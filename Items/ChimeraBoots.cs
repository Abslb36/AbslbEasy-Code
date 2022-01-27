using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace AbslbEasy.Items
{
    [AutoloadEquip(EquipType.Shoes)]
    public class ChimeraBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chimera Boots");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.chimera);
            Tooltip.SetDefault("The wearer can run super fast\nIncreased mobility on ice\nEffects of Frog Leg\n" +
                "Prevents fall damage\nEffect of Rocket Boots\n\"Fusion\"");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.chimeraTip);
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
            player.accRunSpeed = 10f;
            player.moveSpeed += 0.10f;
            player.maxRunSpeed += 1f;
            player.iceSkate = true;
            player.noFallDmg = true;
            player.rocketBoots = 3;
            player.jumpBoost = true;
            player.autoJump = true;
            player.jumpSpeedBoost = player.jumpSpeedBoost + 2.4f;
            player.extraFall = player.extraFall + 15;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostsparkBoots, 1);
            recipe.AddIngredient(ItemID.LuckyHorseshoe, 1);
            recipe.AddIngredient(ItemID.FrogLeg, 1);
            recipe.AddIngredient(ItemID.SwiftnessPotion, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
