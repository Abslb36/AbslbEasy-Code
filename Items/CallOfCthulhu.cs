
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AbslbEasy.Items
{
    public class CallOfCthulhu : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Call of Cthulhu");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.coc);
            Tooltip.SetDefault("Significantly increases item grab range, provides various visual effects, and increases mining speed when favorited\nRidiculously increases grab range when used\n" +
                "\"This is not a summon for the Moon Lord!\"");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.cocTip);
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
            recipe.AddIngredient(ModContent.ItemType<SeenEye>());
            recipe.AddIngredient(ModContent.ItemType<GrabMagnet>());
            recipe.AddIngredient(ItemID.DemoniteBar, 3);
            recipe.AddTile(TileID.Bookcases);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ModContent.ItemType<SeenEye>());
            recipe2.AddIngredient(ModContent.ItemType<GrabMagnet>());
            recipe2.AddIngredient(ItemID.CrimtaneBar, 3);
            recipe2.AddTile(TileID.Bookcases);
            recipe2.SetResult(this, 1);
            recipe2.AddRecipe();
        }
        public override void UpdateInventory(Player player)
        {
            if (!item.favorited) return;
            player.GetModPlayer<AbslbEasyPlayer>().largeGrab = true;
            player.findTreasure = true;
            player.nightVision = true;
            player.dangerSense = true;
            player.detectCreature = true;
            player.sonarPotion = true;
            Lighting.AddLight((int)(player.position.X + (double)(player.width / 2)) / 16, (int)(player.position.Y + (double)(player.height / 2)) / 16, 0.8f, 0.95f, 1f);
            player.pickSpeed = player.pickSpeed - 0.25f;
        }
        public override bool UseItem(Player player)
        {
            player.GetModPlayer<AbslbEasyPlayer>().ridiculeGrab = 120;
            return true;
        }
    }
}
