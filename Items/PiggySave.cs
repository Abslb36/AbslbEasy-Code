
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace AbslbEasy.Items
{
    public class PiggySave : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("PiggyBank Save");
            DisplayName.AddTranslation(GameCulture.Chinese, ChineseText.piggySave);
            Tooltip.SetDefault("Enables auto-moneysave to your piggy bank when favorited\nRight click on this item to open your piggy bank\n\"It is different from the piggy bank!\"");
            Tooltip.AddTranslation(GameCulture.Chinese, ChineseText.piggySaveTip);
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 20;
            item.uniqueStack = true;
            item.rare = ItemRarityID.Green;
            item.value = Item.sellPrice(0, 2, 0, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PiggyBank, 1);
            recipe.AddIngredient(ItemID.GrayPressurePlate, 1);
            recipe.AddIngredient(ItemID.GoldCoin, 2);
            recipe.AddTile(TileID.Tables);
            recipe.AddTile(TileID.Chairs);
            recipe.anyPressurePlate = true;
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
        public override void UpdateInventory(Player player)
        {
            if (!item.favorited) return;
            int i;
            for (i = 0; i < player.inventory.Length-1; i++)
            {
                if ((player.inventory[i].type == ItemID.PlatinumCoin || player.inventory[i].type == ItemID.GoldCoin
                    || player.inventory[i].type == ItemID.SilverCoin || player.inventory[i].type == ItemID.CopperCoin) && player.inventory[i].stack > 0 && player.inventory[i].active) 
                {
                    int nextCoin;
                    nextCoin = AddToBank(player, player.inventory[i]);
                    if (nextCoin == -1) 
                    {
                        Recipe.FindRecipes();
                        break;
                    }
                    if (nextCoin == 0) player.inventory[i].TurnToAir();
                    else
                    {
                        int type = player.inventory[i].type == ItemID.CopperCoin ? ItemID.SilverCoin :
                                (player.inventory[i].type == ItemID.SilverCoin ? ItemID.GoldCoin : ItemID.PlatinumCoin);
                        player.inventory[i].TurnToAir();
                        player.QuickSpawnItem(type, nextCoin);
                    }
                    Recipe.FindRecipes();
                }
            }
        }
        
        private int AddToBank(Player player, Item item)
        {
            int i;
            if(item.type == ItemID.PlatinumCoin && item.stack == item.maxStack)
            {
                for (i = 39; i >= 0; i--)
                {
                    if (player.bank.item[i].type == ItemID.None)
                    {
                        player.bank.item[i] = item.Clone();
                        return 0;
                    }
                }
                return -1;
            }
            for (i = 39; i >= 0; i--)
            {
                if (player.bank.item[i].type == item.type && player.bank.item[i].stack != 0 && player.bank.item[i].active
                    && (player.bank.item[i].stack != item.maxStack || item.type != ItemID.PlatinumCoin))
                {
                    if (player.bank.item[i].stack + item.stack <= item.maxStack)
                    {
                        player.bank.item[i].stack += item.stack;
                        return 0;
                    }
                    else
                    {
                        int change = item.stack + player.bank.item[i].stack - item.maxStack;
                        player.bank.item[i].stack = change;
                        if (item.type != ItemID.PlatinumCoin)
                        {
                            return 1;
                        }
                        else
                        {
                            return item.maxStack;
                        }
                    }
                }
            }
            for (i = 39; i >= 0; i--)
            {
                if (player.bank.item[i].type == ItemID.None)
                {
                    player.bank.item[i] = item.Clone();
                    return 0;
                }
            }
            return -1;
        }

        public override bool CanRightClick()
        {
            for (int i=0; i < Main.maxProjectiles; i++)
                if (Main.projectile[i].type == ProjectileID.FlyingPiggyBank)
                {
                    Main.projectile[i].Kill();
                }
            int num1 = Projectile.NewProjectile(Main.player[Main.myPlayer].position, Vector2.Zero, ProjectileID.FlyingPiggyBank, 0, 0, Main.myPlayer);
            Main.mouseRightRelease = false;
            if (Main.player[Main.myPlayer].chest == -2)
            {
                Main.PlaySound(SoundID.Item59, -1, -1);
                Main.player[Main.myPlayer].chest = -1;
            }
            else
            {
                Main.player[Main.myPlayer].flyingPigChest = num1;
                Main.player[Main.myPlayer].chest = -2;
                Main.player[Main.myPlayer].chestX = (int)(Main.player[Main.myPlayer].position.X);
                Main.player[Main.myPlayer].chestY = (int)(Main.player[Main.myPlayer].position.Y);
                Main.player[Main.myPlayer].talkNPC = -1;
                Main.npcShop = 0;
                Main.playerInventory = true;
                Main.PlaySound(SoundID.Item59, -1, -1);
            }
            Recipe.FindRecipes();
            return base.CanRightClick();
        }
    }
}
