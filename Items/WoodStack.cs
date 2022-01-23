using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.Utilities;

namespace AbslbEasy.Items
{
    class WoodStack : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stack of Woods");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.woodStack);
            Tooltip.SetDefault("Right click to extract timbers");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.woodStackTip);
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 30;
            item.maxStack = 99;
            item.rare = ItemRarityID.White;
            item.value = Item.sellPrice(0, 0, 20, 0);
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            int[] woodTypes = { ItemID.Wood, ItemID.Ebonwood, ItemID.Shadewood, ItemID.PalmWood, ItemID.RichMahogany, ItemID.BorealWood, ItemID.DynastyWood, ItemID.Pearlwood, ItemID.SpookyWood };
            int num1, num2;
            UnifiedRandom random = new UnifiedRandom();
            Item.NewItem(player.position, woodTypes[0], 25);
            if (NPC.downedPlantBoss)
            {
                num1 = random.Next(0, 9);
                num2 = random.Next(1, 25);
                player.QuickSpawnItem(woodTypes[num1], num2);
                num1 = random.Next(0, 9);
                num2 = 25 - num2;
                player.QuickSpawnItem(woodTypes[num1], num2);
            }
            else
            {
                num1 = random.Next(0, 7);
                num2 = random.Next(1, 25);
                player.QuickSpawnItem(woodTypes[num1], num2);
                num1 = random.Next(0, 7);
                num2 = 25 - num2;
                player.QuickSpawnItem(woodTypes[num1], num2);
            }
            base.RightClick(player);
        }
        
    }
}
