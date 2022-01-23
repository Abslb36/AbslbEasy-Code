using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.Localization;

namespace AbslbEasy.Items
{
    public class ChasePotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaser Potion");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.chasePotion);
            Tooltip.SetDefault("Increases life healing value of your potions by 50%");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.chasePotionTip);
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 32;
            item.accessory = true;
            item.value = Item.sellPrice(gold: 3);
            item.rare = ItemRarityID.Lime;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AbslbEasyPlayer>().potionChase = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HealingPotion, 15);
            recipe.AddIngredient(ItemID.CrystalShard, 15);
            recipe.AddIngredient(ItemID.Ichor, 15);
            recipe.AddIngredient(ItemID.GlowingMushroom, 15);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.HealingPotion, 15);
            recipe2.AddIngredient(ItemID.CrystalShard, 15);
            recipe2.AddIngredient(ItemID.CursedFlame, 15);
            recipe2.AddIngredient(ItemID.GlowingMushroom, 15);
            recipe2.AddTile(TileID.CrystalBall);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
}
