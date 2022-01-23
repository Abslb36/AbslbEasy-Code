using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Localization;

namespace AbslbEasy.Items
{
	public class DiscordKey : ModItem
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Car Key of Discord");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.discordKey);
			Tooltip.SetDefault("Mount to teleport, inflicts 5 seconds of chaos state too\nCannot be used in Chaos State");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.discordKeyTip);
        }

		public override void SetDefaults() {
			item.width = 34;
			item.height = 34;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Lime;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.UseSound = SoundID.Item79;
			item.noMelee = true;
            item.mountType = MountID.Slime;
		}

        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff(BuffID.ChaosState)) return false;
            Vector2 mousePosition = new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y);
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
            return false;
        }

        public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RodofDiscord, 1);
            recipe.AddIngredient(ItemID.SlimySaddle, 1);
            recipe.AddIngredient(ItemID.SoulofFlight, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ModContent.ItemType<DiscordHook>(), 1);
            recipe2.AddIngredient(ItemID.SlimySaddle, 1);
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
            ModRecipe recipe3 = new ModRecipe(mod);
            recipe3.AddIngredient(ModContent.ItemType<DiscordKey>(), 1);
            recipe3.AddIngredient(ItemID.SlimySaddle, 1);
            recipe3.AddTile(TileID.TinkerersWorkbench);
            recipe3.SetResult(ModContent.ItemType<DiscordHook>(), 1);
            recipe3.AddRecipe();
        }
	}
}