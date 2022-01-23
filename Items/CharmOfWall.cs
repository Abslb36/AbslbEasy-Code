using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.Localization;

namespace AbslbEasy.Items
{
    [AutoloadEquip(EquipType.Neck)]
    public class CharmOfWall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charm of the Ultimate Wall");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.charmWall);
            Tooltip.SetDefault("Increases life regeneration speed\nReduces potion sickness time\nIncreases life healing value of your potions by 50%" +
                "\nLocks your defence to be no less than the sum of defence stats of your equips" +
                "\nProvides immunty to many debuffs, fire blocks and knockback\nProvides longer invincible time after hurt\nProvides extra damage reduction below half health");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.charmWallTip);
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 40;
            item.accessory = true;
            item.lifeRegen = 2;
            item.defense = 4;
            item.value = Item.sellPrice(gold: 40);
            item.rare = ItemRarityID.Purple;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pStone = true;
            player.GetModPlayer<AbslbEasyPlayer>().defenceLock = true;
            player.GetModPlayer<AbslbEasyPlayer>().potionChase = true;
            if (player.statLife <= (double)player.statLifeMax2 * 0.5)
                player.AddBuff(62, 5, true);
            player.longInvince = true;
            player.buffImmune[46] = true;
            player.noKnockback = true;
            player.fireWalk = true;
            player.buffImmune[33] = true;
            player.buffImmune[36] = true;
            player.buffImmune[30] = true;
            player.buffImmune[20] = true;
            player.buffImmune[32] = true;
            player.buffImmune[31] = true;
            player.buffImmune[35] = true;
            player.buffImmune[23] = true;
            player.buffImmune[22] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Stoned] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrozenTurtleShell, 1);
            recipe.AddIngredient(ItemID.CrossNecklace, 1);
            recipe.AddIngredient(ItemID.AnkhShield, 1);
            recipe.AddIngredient(ModContent.ItemType<AlchemistWard>(), 1);
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
