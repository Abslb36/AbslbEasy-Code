using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;

namespace AbslbEasy.Items
{
	public class DiscordHook : ModItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hook of Discord");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.discordHook);
            Tooltip.SetDefault("Teleports on launch\nInflicts chaos state of 5 seconds\nCannot be used under chaos state");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.discordHookTip);
        }

		public override void SetDefaults() {
			/*
				this.noUseGraphic = true;
				this.damage = 0;
				this.knockBack = 7f;
				this.useStyle = 5;
				this.name = "Amethyst Hook";
				this.shootSpeed = 10f;
				this.shoot = 230;
				this.width = 18;
				this.height = 28;
				this.useSound = 1;
				this.useAnimation = 20;
				this.useTime = 20;
				this.rare = 1;
				this.noMelee = true;
				this.value = 20000;
			*/
			// Instead of copying these values, we can clone and modify the ones we want to copy
			item.CloneDefaults(ItemID.AmethystHook);
			item.shootSpeed = 0f;
            item.rare = ItemRarityID.Lime;
            item.value = Item.sellPrice(0, 10, 0, 0);
			item.shoot = ModContent.ProjectileType<DiscordHookProjectile>();
            item.width = 26;
            item.height = 34;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RodofDiscord, 1);
            recipe.AddIngredient(ItemID.SoulofFlight, 10);
            recipe.AddIngredient(ItemID.GrapplingHook, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.TeleportationPotion, 5);
            recipe2.AddIngredient(ItemID.CrystalShard, 10);
            recipe2.AddIngredient(ItemID.UnicornHorn, 1);
            recipe2.AddIngredient(ItemID.SoulofFlight, 10);
            recipe2.AddIngredient(ItemID.GrapplingHook, 1);
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
    
    
	public class DiscordHookProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("");
		}

		public override void SetDefaults() {
            //	this.netImportant = true;
            //	this.name = "Gem Hook";
            //	this.width = 18;
            //	this.height = 18;
            //	this.aiStyle = 7;
            //	this.friendly = true;
            //	this.penetrate = -1;
            //	this.tileCollide = false;
            //	this.timeLeft *= 10;
			projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
            projectile.width = 24;
		}

		// Use this hook for hooks that can have multiple hooks mid-flight: Dual Hook, Web Slinger, Fish Hook, Static Hook, Lunar Hook
		public override bool? CanUseGrapple(Player player) {
            return player.active && !player.chaosState;
        }

		// Return true if it is like: Hook, CandyCaneHook, BatHook, GemHooks
		public override bool? SingleGrappleHook(Player player)
		{
			return true;
		}

		// Use this to kill oldest hook. For hooks that kill the oldest when shot, not when the newest latches on: Like SkeletronHand
		// You can also change the projectile like: Dual Hook, Lunar Hook
		public override void UseGrapple(Player player, ref int type)
		{
            Vector2 mousePosition = new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y);
            /**
            Vector2 p1 = new Vector2(mousePosition.X - player.width/2, mousePosition.Y - player.height/2);
            Vector2 p2 = new Vector2(mousePosition.X + player.width/2, mousePosition.Y + player.height/2);
            if ( Collision.CanHitLine(p1,1,1,p2,1,1))
            {
                player.Teleport(mousePosition);
                player.AddBuff(BuffID.ChaosState, 300);
                player.wingTime = player.wingTimeMax;
                player.jumpAgainBlizzard = true;
                player.jumpAgainCloud = true;
                player.jumpAgainFart = true;
                player.jumpAgainSail = true;
                player.jumpAgainSandstorm = true;
                player.rocketTime = player.rocketTimeMax;
                //player.UnityTeleport(mousePosition);
            }
            */
            Vector2 vector2;
            vector2.X = ((float)Main.mouseX + Main.screenPosition.X);
            vector2.Y = (double)player.gravDir != 1.0 ? (float)(Main.screenPosition.Y + (double)Main.screenHeight - (double)Main.mouseY) : (float)((double)Main.mouseY + Main.screenPosition.Y - (double)player.height);
            if (vector2.X > 50.0 && vector2.X < (double)(Main.maxTilesX * 16 - 50) && (vector2.Y > 50.0 && vector2.Y < (double)(Main.maxTilesY * 16 - 50)))
            {
                int index1 = (int)(vector2.X / 16.0);
                int index2 = (int)(vector2.Y / 16.0);
                if (((int)Main.tile[index1, index2].wall != 87 || (double)index2 <= Main.worldSurface || NPC.downedPlantBoss) && !Collision.SolidCollision(vector2, player.width, player.height))
                {
                    player.Teleport(vector2, 1, 0);
                    NetMessage.SendData(MessageID.Teleport, -1, -1, (NetworkText)null, 0, (float)player.whoAmI, (float)vector2.X, (float)vector2.Y, 1, 0, 0);
                    if (player.chaosState)
                    {
                        player.statLife = player.statLife - player.statLifeMax2 / 7;
                        PlayerDeathReason damageSource = PlayerDeathReason.ByOther(13);
                        if (Main.rand.Next(2) == 0)
                            damageSource = PlayerDeathReason.ByOther(player.Male ? 14 : 15);
                        if (player.statLife <= 0)
                            player.KillMe(damageSource, 1.0, 0, false);
                        player.lifeRegenCount = 0;
                        player.lifeRegenTime = 0;
                    }
                    player.AddBuff(BuffID.ChaosState, 300, true);
                    player.wingTime = player.wingTimeMax;
                    player.jumpAgainBlizzard = true;
                    player.jumpAgainCloud = true;
                    player.jumpAgainFart = true;
                    player.jumpAgainSail = true;
                    player.jumpAgainSandstorm = true;
                    player.rocketTime = player.rocketTimeMax;
                }
                else
                {
                    Color c = new Color(1f, 0.2f, 0.9f);
                    Dust.NewDust(player.position, player.width, player.height, DustID.Teleporter, 0f, 0f, 150, c, 1.1f);
                    Dust.NewDust(mousePosition, player.width, player.height, DustID.Teleporter, 0f, 0f, 150, c, 1.1f);
                }
            }
        }

        // Amethyst Hook is 300, Static Hook is 600
        public override float GrappleRange() {
			return 0f;
		}

		public override void NumGrappleHooks(Player player, ref int numHooks) {
			numHooks = 1;
		}

		// default is 11, Lunar is 24
		public override void GrappleRetreatSpeed(Player player, ref float speed) {
			speed = 0f;
		}

		public override void GrapplePullSpeed(Player player, ref float speed) {
            projectile.timeLeft=0;
			speed = 0f;
		}

		public override void GrappleTargetPoint(Player player, ref float grappleX, ref float grappleY) {
            //Vector2 dirToPlayer = projectile.DirectionTo(player.Center);
            //float hangDist = 50f;
            //grappleX += dirToPlayer.X * hangDist;
            //grappleY += dirToPlayer.Y * hangDist;
            grappleX = (float)Main.MouseWorld.X;
            grappleY = (float)Main.MouseWorld.Y;
		}

        //public override void AI()
        //{
        //    return;
        //}
    }
    
	// Animated hook example
	// Multiple, 
	// only 1 connected, spawn mult
	// Light the path
	// Gem Hooks: 1 spawn only
	// Thorn: 4 spawns, 3 connected
	// Dual: 2/1 
	// Lunar: 5/4 -- Cycle hooks, more than 1 at once
	// AntiGravity -- Push player to position
	// Static -- move player with keys, don't pull to wall
	// Christmas -- light ends
	// Web slinger -- 9/8, can shoot more than 1 at once
	// Bat hook -- Fast reeling

}


