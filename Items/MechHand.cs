
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AbslbEasy.Items
{
    public class MechHand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mechanical Hand");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.mechHand);
            Tooltip.SetDefault("Enables auto-swing and increases mining speed when favorited\n\"It is different from the mechanical glove!\"");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.mechHandTip);
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.uniqueStack = true;
            item.rare = ItemRarityID.Blue;
            item.value = Item.sellPrice(0, 0, 5, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 7);
            recipe.AddIngredient(ItemID.GrayPressurePlate, 1);
            recipe.AddIngredient(ItemID.DartTrap, 1);
            recipe.AddIngredient(ItemID.FeralClaws, 1);
            recipe.anyIronBar = true;
            recipe.anyPressurePlate = true;
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.IronBar, 7);
            recipe2.AddIngredient(ItemID.GrayPressurePlate, 1);
            recipe2.AddIngredient(ItemID.DartTrap, 1);
            recipe2.AddIngredient(ItemID.JungleSpores, 5);
            recipe2.AddIngredient(ItemID.Silk, 7);
            recipe2.AddIngredient(ItemID.Vine, 1);
            recipe2.anyIronBar = true;
            recipe2.anyPressurePlate = true;
            recipe2.AddTile(TileID.Anvils);
            recipe2.AddTile(TileID.Loom);
            recipe2.SetResult(this, 1);
            recipe2.AddRecipe();
        }
        public override void UpdateInventory(Player player)
        {
            if (!item.favorited) return;
            player.pickSpeed = player.pickSpeed - 0.25f;
            if (!player.HeldItem.autoReuse && 
                !player.HeldItem.channel &&
                player.HeldItem.fishingPole == 0)
            {
                if (player.itemAnimation == 0)
                {
                    player.releaseUseItem = true;
                }
            }
        }
    }
}
