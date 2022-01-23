using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.Localization;

namespace AbslbEasy.Items
{
    public class DefenceLocket : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Locket of Defence");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.defenceLocket);
            Tooltip.SetDefault("Locks your defence to be no less than the sum of defence stats of your equips");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.defenceLocketTip);
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 32;
            item.accessory = true;
            item.value = Item.sellPrice(gold: 2, silver: 50);
            item.rare = ItemRarityID.LightPurple;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AbslbEasyPlayer>().defenceLock = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldBar, 5);
            recipe.AddIngredient(ItemID.Obsidian, 10);
            recipe.AddIngredient(ItemID.GoldenKey, 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.anyIronBar = true;
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.PlatinumBar, 5);
            recipe2.AddIngredient(ItemID.Obsidian, 10);
            recipe2.AddIngredient(ItemID.GoldenKey, 1);
            recipe2.AddIngredient(ItemID.HellstoneBar, 5);
            recipe2.AddTile(TileID.HeavyWorkBench);
            recipe2.anyIronBar = true;
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
}
