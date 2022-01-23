using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.Localization;

namespace AbslbEasy.Items
{
    [AutoloadEquip(EquipType.Neck)]
    public class AlchemistWard : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warding of Alchemist");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.alchemistWard);
            Tooltip.SetDefault("Increases life regeneration speed\nReduces potion sickness time\nIncreases life healing value of your potions by 50%\nLocks your defence to be no less than the sum of defence stats of your equips");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.alchemistWardTip);
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 32;
            item.accessory = true;
            item.lifeRegen = 2;
            item.value = Item.sellPrice(gold: 5, silver: 50);
            item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pStone = true;
            player.GetModPlayer<AbslbEasyPlayer>().defenceLock = true;
            player.GetModPlayer<AbslbEasyPlayer>().potionChase = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CharmofMyths, 1);
            recipe.AddIngredient(ModContent.ItemType<ChasePotion>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DefenceLocket>(), 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
