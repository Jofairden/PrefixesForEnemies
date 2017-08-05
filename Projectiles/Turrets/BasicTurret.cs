using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Turrets
{
    public abstract class BasicTurret : ModProjectile
    {
        protected bool hasTarget;
        protected NPC target;
        protected int range;
        protected int fireRate;
        protected float shootSpeed;
        protected int ammoType;
        protected int secondaryType = 0;
        protected int secondaryRate;
        protected float secondarySpeed;
        protected int secondaryDam;
        protected bool hasStand = false;
        protected int buffID;

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            Main.projPet[projectile.type] = true;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft = 3600;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        protected void basicAI()
        {
            //update time left, required for minion cap
            update();
            if (target==null)
            {
                target = getTarget(range);
            }
            //gets a new target if the old one is dead or out of sight
            else if (!target.active || !Collision.CanHitLine(projectile.position, projectile.width, projectile.height, target.position, target.width, target.height) || projectile.Distance(target.position)>range)
            {
                target = getTarget(range);
            }
            //has target
            if (target != null)
            {
                Vector2 toTarget = target.Center - projectile.position;
                toTarget += new Vector2((toTarget.X / shootSpeed) * target.velocity.X, (toTarget.Y / shootSpeed) * target.velocity.Y);//attempt to lead target
                projectile.rotation = toTarget.ToRotation();
                projectile.ai[0]++;//shot timer
                if(projectile.ai[0] >= fireRate)
                {
                    shoot(toTarget);
                    projectile.ai[0] = 0;
                }
                if(secondaryType != 0)
                {
                    projectile.localAI[1]++;
                    if (projectile.localAI[1] >= secondaryRate)
                    {
                        shootSecondary(toTarget);
                        projectile.localAI[1] = 0;
                    }
                }
            }
            //does not have target, idle rotation. ai[1] stores the angle of turret to contact point, set on creation
            else
            {
                if (projectile.rotation >= projectile.ai[1] + (6.28f-.785f) - 3.14f)
                {
                    projectile.localAI[0] = -1;
                }
                else if (projectile.rotation <= projectile.ai[1] + .785f - 3.14f)
                {
                    projectile.localAI[0] = 1;
                }
                projectile.rotation += .012f * projectile.localAI[0];
            }
            //flip sprite based on rotation, not currently working
            float adjustedRot = (projectile.rotation - projectile.ai[1]) % (2 * (float)Math.PI);
            if (adjustedRot > Math.PI)
            {
                projectile.spriteDirection = -1;
            }
            else
            {
                projectile.spriteDirection = 1;
            }
        }
        protected void createStand()
        {
            Vector2 adjustment = new Vector2(-(float)Math.Cos(projectile.ai[1]), -(float)Math.Sin(projectile.ai[1])) * 20;
            projectile.position -= adjustment;
            projectile.rotation = -projectile.ai[1];
            int p = Projectile.NewProjectile(projectile.Center.X + 2*adjustment.X, projectile.Center.Y + 2*adjustment.Y, 0, 0, mod.ProjectileType("TurretStand"), 0, 0, projectile.owner, projectile.whoAmI);
            Main.projectile[p].timeLeft = projectile.timeLeft;
            Main.projectile[p].rotation = projectile.ai[1] + 1.57f;
        }
        protected NPC getTarget(int range)
        {
            float lowestD = range;
            NPC closest = null;
            for (int i = 0; i < 100; i++)
            {
                NPC npc = Main.npc[i];
                float distance = (float)Math.Sqrt((npc.Center.X - projectile.Center.X) * (npc.Center.X - projectile.Center.X) + (npc.Center.Y - projectile.Center.Y) * (npc.Center.Y - projectile.Center.Y));
                if (lowestD > distance && npc.CanBeChasedBy() && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
                {
                    closest = npc;
                    lowestD = distance;
                }
            }
            return closest;
        }
        protected abstract void shoot(Vector2 toTarget);//to be overridden in subclasses
        protected abstract void shootSecondary(Vector2 toTarget);
        protected abstract void update();
        protected int consumeAmmo(out int type)
        {
            Item item = new Item();
            bool flag = false;
            bool canShoot = false;
            int damage = projectile.damage;
            Player player = Main.player[projectile.owner];
            for (int i = 57; i >= 54; i--)//bottom up for ammo
            {
                if (player.inventory[i].ammo == ammoType && player.inventory[i].stack > 0)
                {
                    item = player.inventory[i];
                    canShoot = true;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                for (int j = 0; j < 54; j++)
                {
                    if (player.inventory[j].ammo == ammoType && player.inventory[j].stack > 0)
                    {
                        item = player.inventory[j];
                        canShoot = true;
                        break;
                    }
                }
            }
            if (!canShoot)
            {
                type = -1;
                return -1;
            }
            type = item.shoot;
            damage = (int)((damage + item.damage) * player.rangedDamage);
            if (item.consumable)
            {
                item.stack--;
                if (item.stack <= 0)
                {
                    item.active = false;
                    item.type = 0;
                }
            }
            return damage;
        }
    }
}
