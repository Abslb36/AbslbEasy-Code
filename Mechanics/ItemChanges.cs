using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AbslbEasy.Mechanics
{
	class ItemChanges : GlobalItem
	{
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (item.summon) {
				int count = 0;
				bool founddmg = false;
				foreach (TooltipLine tooltip in tooltips) {
					count++;
					if(tooltip.Name == "Damage") {
						founddmg = true;
						break;
					}
				}
				if (founddmg) {
                    int BaseCrits = Main.player[item.owner].GetModPlayer<AbslbEasyPlayer>().minionCrits;
                    BaseCrits += Math.Min(Math.Min(Main.player[item.owner].meleeCrit, Main.player[item.owner].rangedCrit), Math.Min(Main.player[item.owner].magicCrit, Main.player[item.owner].thrownCrit));
                    int SummonCritChance = item.crit + BaseCrits;
                    if ( SummonCritChance < 4 ) SummonCritChance = 4;
					if ( SummonCritChance > 100 ) SummonCritChance = 100;
					string SummonCritString = SummonCritChance + (Language.ActiveCulture != GameCulture.Chinese ? "% critical strike chance" : "%暴击率");
					tooltips.Insert(count, new TooltipLine(AbslbEasy.Instance, "CritChance", SummonCritString));
				}
			}
		}

        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            base.GrabRange(item, player, ref grabRange);
            if (player.GetModPlayer<AbslbEasyPlayer>().largeGrab) grabRange *= 7;
            if (player.GetModPlayer<AbslbEasyPlayer>().ridiculeGrab > 0) grabRange *= 2000;
        }
        public override bool GrabStyle(Item item, Player player)
        {
            if (player.GetModPlayer<AbslbEasyPlayer>().ridiculeGrab <= 0 && !player.GetModPlayer<AbslbEasyPlayer>().largeGrab) 
                return base.GrabStyle(item, player);
            else
            {
                Vector2 vectorItemToPlayer = player.Center - item.Center;
                Vector2 movement = vectorItemToPlayer.SafeNormalize(default) * 15f;
                item.velocity = movement;
                //item.velocity = Collision.TileCollision(item.position, item.velocity, item.width, item.height);
            }
            return true;
        }
    }
}
