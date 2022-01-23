using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.Graphics.Shaders;
using Terraria.DataStructures;

namespace AbslbEasy
{
    public class AbslbEasyPlayer : ModPlayer
    {
        // Here we declare the frostBurnSummon variable which will represent whether this player has the effect or not.
        public bool frostBurnSummon;
        public int minionCrits;
        public bool defenceLock;
        public bool potionChase;
        public bool largeGrab;
        public int ridiculeGrab;
        private int lastLife;

        // ResetEffects is used to reset effects back to their default value. Terraria resets all effects every frame back to defaults so we will follow this design. (You might think to set a variable when an item is equipped and unassign the value when the item in unequipped, but Terraria is not designed that way.)
        public override void ResetEffects()
        {
            frostBurnSummon = false;
            minionCrits = 0;
            defenceLock = false;
            potionChase = false;
            largeGrab = false;
            ridiculeGrab = ridiculeGrab <= 1 ? 0 : ridiculeGrab - 1;
            lastLife = player.statLife;
        }

        // Here we use a "hook" to actually let our frostBurnSummon status take effect. This hook is called anytime a player owned projectile hits an enemy. 
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if ((proj.minion || ProjectileID.Sets.MinionShot[proj.type]) && frostBurnSummon && !proj.noEnchantments)
            {
                target.AddBuff(BuffID.Frostburn, 60 * Main.rand.Next(5, 15), false);
            }
            
        }

        public override void PostUpdateEquips()
        {
            if (defenceLock)
            {
                int defLockVal = 0;
                int i;
                for (i = 0; i <= 7 + player.extraAccessorySlots; i++)
                {
                    if (player.armor[i].type != ItemID.None)
                        defLockVal += player.armor[i].defense;
                }
                if (player.statDefense < defLockVal)
                    player.statDefense = defLockVal;
            }
        }
        
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (defenceLock)
            {
                int defLockVal = 0;
                int i;
                for (i = 0; i <= 7 + player.extraAccessorySlots; i++)
                {
                    if(player.armor[i].type != ItemID.None)
                        defLockVal += player.armor[i].defense;
                }
                if (player.statDefense < defLockVal)
                    player.statDefense = defLockVal;
                float factor = Main.expertMode ? 0.75f : 0.50f;
                float flux = (Main.expertMode ? 3f: 2.5f);
                float theory = npc.damage * flux - factor * player.statDefense;
                if (damage > theory)
                    damage = (int) theory;
            }
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (defenceLock)
            {
                int defLockVal = 0;
                int i;
                for (i = 0; i <= 7 + player.extraAccessorySlots; i++)
                {
                    if (player.armor[i].type != ItemID.None)
                        defLockVal += player.armor[i].defense;
                }
                if (player.statDefense < defLockVal)
                    player.statDefense = defLockVal;
                float factor = Main.expertMode ? 0.75f : 0.50f;
                float flux = (Main.expertMode ? 4f : 2.5f);
                float theory = proj.damage * flux - factor * player.statDefense;
                if (damage > theory)
                    damage = (int)theory;
            }
        }
        
        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            base.GetHealLife(item, quickHeal, ref healValue);
            if (potionChase) healValue += healValue >= 0 ? (int)(healValue / 2) : -(int)(healValue / 2);
        }
        
    }

}
