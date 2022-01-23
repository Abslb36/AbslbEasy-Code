
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AbslbEasy.Items
{
    public class SeenEye : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eye of Seen");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.seenEye);
            Tooltip.SetDefault("Enables various visual informations when favorited in your inventory");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.seenEyeTip);
        }
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 36;
            item.uniqueStack = true;
            item.rare = ItemRarityID.Orange ;
            item.value = Item.sellPrice(0, 1, 0, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpelunkerPotion, 5);
            recipe.AddIngredient(ItemID.NightOwlPotion, 5);
            recipe.AddIngredient(ItemID.TrapsightPotion, 5);
            recipe.AddIngredient(ItemID.HunterPotion, 5);
            recipe.AddIngredient(ItemID.SonarPotion, 5);
            recipe.AddIngredient(ItemID.ShinePotion, 5);
            recipe.AddIngredient(ItemID.Lens, 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
        public override void UpdateInventory(Player player)
        {
            if (!item.favorited) return;
            player.findTreasure = true;
            player.nightVision = true;
            player.dangerSense = true;
            player.detectCreature = true;
            player.sonarPotion = true;
            Lighting.AddLight((int)(player.position.X + (double)(player.width / 2)) / 16, (int)(player.position.Y + (double)(player.height / 2)) / 16, 0.8f, 0.95f, 1f);
            
        }
    }
}
