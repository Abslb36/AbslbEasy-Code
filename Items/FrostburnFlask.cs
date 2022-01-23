using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AbslbEasy.Items
{
	public class FrostburnFlask : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frostburn Enchantment Flask");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.frostburnFlask);
            Tooltip.SetDefault("Your summon weapons inflict frostburn\n10% Increased minion critical rate");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.frostburnFlaskTip);
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 26;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = ItemRarityID.Orange;
            item.value = Item.buyPrice(gold: 1);
            item.buffType = ModContent.BuffType<FrostburnEnchantBuff>(); //Specify an existing buff to be applied when used.
            item.buffTime = 20 * 60 * 60; //The amount of time the buff declared in item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 2);
            recipe.AddIngredient(ItemID.Shiverthorn, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.ImbuingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }


    public class FrostburnEnchantBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Forstburn Enchantment");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.frostburnEnchantBuff);
            Description.SetDefault("Your summons inflict frostburn\n10% extra minion critical chance");
            Description.AddTranslation(GameCulture.Chinese, ChineseText.frostburnEnchantBuffTip);
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
            Main.lightPet[Type] = false;
            Main.vanityPet[Type] = false;
            this.canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AbslbEasyPlayer>().frostBurnSummon = true;
            player.GetModPlayer<AbslbEasyPlayer>().minionCrits += 10;
        }

    }
}
