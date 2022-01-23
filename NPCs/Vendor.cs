using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AbslbEasy.NPCs
{
	// [AutoloadHead] and npc.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
	[AutoloadHead]
	public class Vendor : ModNPC
	{
		public override string Texture => "AbslbEasy/NPCs/Vendor";

		public override string[] AltTextures => new[] { "AbslbEasy/NPCs/Vendor" };

		public override bool Autoload(ref string name) {
			name = Language.ActiveCulture!=GameCulture.Chinese ? "Vendor" : "小贩";
			return mod.Properties.Autoload;
		}

		public override void SetStaticDefaults() {
			// DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
			// DisplayName.SetDefault("Example Person");
			Main.npcFrameCount[npc.type] = 25;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 4;
			NPCID.Sets.DangerDetectRange[npc.type] = 700;
			NPCID.Sets.AttackType[npc.type] = 0;
			NPCID.Sets.AttackTime[npc.type] = 90;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
			NPCID.Sets.HatOffsetY[npc.type] = 4;
		}

		public override void SetDefaults() {
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 18;
			npc.height = 40;
			npc.aiStyle = 7;
			npc.damage = 10;
			npc.defense = 15;
			npc.lifeMax = 250;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.5f;
			animationType = NPCID.Guide;
		}

		public override void HitEffect(int hitDirection, double damage) {
			int num = npc.life > 0 ? 1 : 5;
			for (int k = 0; k < num; k++) {
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.Cobalt);
			}
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money) {
            return (numTownNPCs >= 1);
		}

		public override string TownNPCName() {
            if(Language.ActiveCulture != GameCulture.Chinese)
			    switch (WorldGen.genRand.Next(6)) {
    				case 0:
					    return "Query";
				    case 1:
    					return "Acknowlegdement";
				    case 2:
    					return "Synthesis";
                    case 3:
                        return "Inflation";
                    case 4:
                        return "Undo";
                    case 5:
                        return "Syncronization";
				    default:
    					return "Query";
			    }
            else
                switch (WorldGen.genRand.Next(6))
                {
                    case 0:
                        return "请求";
                    case 1:
                        return "确认";
                    case 2:
                        return "合成";
                    case 3:
                        return "膨胀";
                    case 4:
                        return "撤销";
                    case 5:
                        return "同步";
                    default:
                        return "请求";
                }
        }

		public override string GetChat() {
			switch (Main.rand.Next(4)) {
				case 0:
					return Language.ActiveCulture != GameCulture.Chinese ? "Who Wouldn't?" : "谁不会呢？";
				case 1:
					return Language.ActiveCulture != GameCulture.Chinese ? "Bravo, Bravo!" : "太棒了，太棒了！";
				case 2:
				    return Language.ActiveCulture != GameCulture.Chinese ? "Why would vanilla Slime Crowns cost SOOOO much precious pre-boss materials?" : "原版的史莱姆王冠消耗的材料太多了吧？";
				default:
					return Language.ActiveCulture != GameCulture.Chinese ? "Abslb is a cool dude." : "作者NB。";
			}
		}

		public override void SetChatButtons(ref string button, ref string button2) {
			button = Language.GetTextValue("LegacyInterface.28");
        }

		public override void OnChatButtonClicked(bool firstButton, ref bool shop) {
			if (firstButton) {
				shop = true;
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot) {
			shop.item[nextSlot].SetDefaults(ItemID.SlimeCrown);
            shop.item[nextSlot].value = 30000;
			nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.GoblinBattleStandard);
            shop.item[nextSlot].value = 50000;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.WoodStack>());
            shop.item[nextSlot].value = 2000;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.HerbBag);
            shop.item[nextSlot].value = 15000;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.JungleSpores);
            shop.item[nextSlot].value = 10000;
            nextSlot++;
            if (NPC.downedBoss3)
            {
                shop.item[nextSlot].SetDefaults(ItemID.LifeCrystal);
                shop.item[nextSlot].value = 100000;
                nextSlot++;
            }
            if (Main.hardMode) {
                shop.item[nextSlot].SetDefaults(ItemID.PirateMap);
                shop.item[nextSlot].value = 50000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SnowGlobe);
                shop.item[nextSlot].value = 50000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SoulofLight);
                shop.item[nextSlot].value = 10000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SoulofNight);
                shop.item[nextSlot].value = 10000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.CursedFlame);
                shop.item[nextSlot].value = 10000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Ichor);
                shop.item[nextSlot].value = 10000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.CrystalShard);
                shop.item[nextSlot].value = 10000;
                nextSlot++;
            }
            if (NPC.downedPlantBoss) {
                shop.item[nextSlot].SetDefaults(ItemID.TruffleWorm);
                shop.item[nextSlot].value = 150000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.NaughtyPresent);
                shop.item[nextSlot].value = 100000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.PumpkinMoonMedallion);
                shop.item[nextSlot].value = 100000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.LihzahrdPowerCell);
                shop.item[nextSlot].value = 150000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.LifeFruit);
                shop.item[nextSlot].value = 100000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Ectoplasm);
                shop.item[nextSlot].value = 50000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.CelestialSigil);
                shop.item[nextSlot].value = 250000;
                nextSlot++;
            }
            if (NPC.downedMoonlord)
            {
                shop.item[nextSlot].SetDefaults(ItemID.LunarOre);
                shop.item[nextSlot].value = 150000;
                nextSlot++;
            }
        }

		public override void NPCLoot() {
			Item.NewItem(npc.getRect(), ItemID.SlimeCrown);
		}

		// Make this Town NPC teleport to the Queen statue when triggered.
		public override bool CanGoToStatue(bool toKingStatue) {
            return true;
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback) {
			damage = 20;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {
			cooldown = 30;
			randExtraCooldown = 30;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {
            projType = ProjectileID.StardustCellMinionShot;
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset) {
			multiplier = 12f;
			randomOffset = 2f;
		}
	}
}
