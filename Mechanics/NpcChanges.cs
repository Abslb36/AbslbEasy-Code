using System;
using Terraria;
using Terraria.ModLoader;

namespace AbslbEasy.Mechanics
{
	class NpcChanges : GlobalNPC
	{
		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (!crit) {
				bool fromsummon = false;
				int BaseCrits = 0;
				if (projectile.minion) fromsummon = true;
				else {
					for (int i = 0; i < 1000; i++) {
						Projectile CheckProjectile = Main.projectile[i];
						if (CheckProjectile.active && (CheckProjectile.minion || CheckProjectile.sentry)) {
							fromsummon = true;
							foreach(Item item in Main.player[projectile.owner].inventory) {
								if (item.shoot == CheckProjectile.type) {
									BaseCrits = item.crit;
									break;
								}
							}
							break;
						}
					}
				}
				if (!fromsummon) return;
                BaseCrits += Main.player[projectile.owner].GetModPlayer<AbslbEasyPlayer>().minionCrits;
                BaseCrits += Math.Min(Math.Min(Main.player[projectile.owner].meleeCrit, Main.player[projectile.owner].rangedCrit), Math.Min(Main.player[projectile.owner].magicCrit, Main.player[projectile.owner].thrownCrit));
                int SummonCritChance = BaseCrits;
				if (SummonCritChance < 4) SummonCritChance = 4;
				if ( Main.rand.Next(100) < SummonCritChance ) {
					crit = true;
					damage = (int)((damage/2)*3);
				    knockback *= 1.4f;
					return;
				}
			}
		}
	}
}
