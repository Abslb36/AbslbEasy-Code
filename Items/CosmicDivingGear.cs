using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace AbslbEasy.Items
{
    [AutoloadEquip(EquipType.Face)]
    public class CosmicDivingGear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Diving Gear");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.gravity);
            Tooltip.SetDefault("Enables swimming, underwater breathing, and liquid walking\nProvides light under water, extra mobility on ice, and lava immunty\n" +
                "Move freely in liquids\nProvides immunty to chills, freezing, burn, and cursed inferno effects\n\"Gravity Suit\"");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.gravityTip);
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.accessory = true; // Makes this item an accessory.
            item.rare = ItemRarityID.Lime;
            item.value = Item.sellPrice(gold: 3);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.waterWalk = true;
            player.waterWalk2 = true;
            player.lavaImmune = true;
            player.fireWalk = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.arcticDivingGear = true;
            player.accFlipper = true;
            player.accDivingHelm = true;
            player.iceSkate = true;
            if (player.wet)
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.8f, 0.5f, 0.999f);
            player.breath = player.breathMax + 1;
            //player.gills = true;
            player.ignoreWater = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ArcticDivingGear, 1);
            recipe.AddIngredient(ItemID.LavaWaders, 1);
            recipe.AddIngredient(ItemID.GillsPotion, 5);
            recipe.AddIngredient(ItemID.ObsidianSkinPotion, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
