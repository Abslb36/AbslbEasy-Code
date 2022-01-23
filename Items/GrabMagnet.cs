
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AbslbEasy.Items
{
    public class GrabMagnet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Item-Grab Magnet");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.grabMagnet);
            Tooltip.SetDefault("Significantly increases item grab range when favorited\nRidiculously when used\n\"It is different from the celestral magnet!\"");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.grabMagnetTip);
        }
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 34;
            item.uniqueStack = true;
            item.rare = ItemRarityID.Orange;
            item.value = Item.sellPrice(0, 0, 25, 0);
            item.useAnimation = 120;
            item.useTime = 120;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.autoReuse = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 10);
            recipe.AddIngredient(ItemID.FallenStar, 3);
            recipe.AddIngredient(ItemID.CelestialMagnet, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.anyIronBar = true;
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.IronBar, 15);
            recipe2.AddIngredient(ItemID.FallenStar, 6);
            recipe2.AddIngredient(ItemID.HeartreachPotion, 5);
            recipe2.AddTile(TileID.Anvils);
			recipe2.anyIronBar = true;
            recipe2.SetResult(this, 1);
            recipe2.AddRecipe();
        }
        public override void UpdateInventory(Player player)
        {
            if (!item.favorited) return;
            player.GetModPlayer<AbslbEasyPlayer>().largeGrab = true;
        }
        public override bool UseItem(Player player)
        {
            player.GetModPlayer<AbslbEasyPlayer>().ridiculeGrab = 120;
            return true;
        }
    }
}
