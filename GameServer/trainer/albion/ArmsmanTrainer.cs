/*
 * DAWN OF LIGHT - The first free open source DAoC server emulator
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 *
 */
using DOL.GS.PacketHandler;

namespace DOL.GS.Trainer
{
    /// <summary>
    /// Armsman Trainer
    /// </summary>
    [NPCGuildScript("Armsman Trainer", eRealm.Albion)] // this attribute instructs DOL to use this script for all "Fighter Trainer" NPC's in Albion (multiple guilds are possible for one script)
    public class ArmsmanTrainer : GameTrainer
    {
        public override eCharacterClass TrainedClass => eCharacterClass.Armsman;

        // Item Template Ids
        private const string WeaponSlash = "slash_sword_item";
        private const string WeaponCrush = "crush_sword_item";
        private const string WeaponThrust = "thrust_sword_item";
        private const string WeaponPole = "pike_polearm_item";

        /// <summary>
        /// Interact with trainer
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player))
            {
                return false;
            }

            // check if class matches.
            if (player.CharacterClass.ID == (int)TrainedClass)
            {
                OfferTraining(player);
            }
            else
            {
                // perhaps player can be promoted
                if (CanPromotePlayer(player))
                {
                    player.Out.SendMessage(Name + " says, \"Do you desire to [join the Defenders of Albion] and defend our realm as an Armsman?\"",eChatType.CT_Say,eChatLoc.CL_PopupWindow);
                    if (!player.IsLevelRespecUsed)
                    {
                        OfferRespecialize(player);
                    }
                }
                else
                {
                    CheckChampionTraining(player);
                }
            }

            return true;
        }

        /// <summary>
        /// Talk to trainer
        /// </summary>
        /// <param name="source"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public override bool WhisperReceive(GameLiving source, string text)
        {
            if (!base.WhisperReceive(source, text))
            {
                return false;
            }

            if (!(source is GamePlayer player))
            {
                return false;
            }

            if (CanPromotePlayer(player))
            {
                switch (text)
                {
                    case "join the Defenders of Albion":
                        player.Out.SendMessage(Name + " says, \"Very well. Choose a weapon, and you shall become one of us. Which would you have, [slashing], [crushing], [thrusting] or [polearms]?\"",eChatType.CT_Say,eChatLoc.CL_PopupWindow);
                        break;
                    case "slashing":
                        PromotePlayer(player, (int)eCharacterClass.Armsman, "Here is your Sword of the Initiate. Welcome to the Defenders of Albion.", null);
                        player.ReceiveItem(this,WeaponSlash);
                        break;
                    case "crushing":
                        PromotePlayer(player, (int)eCharacterClass.Armsman, "Here is your Mace of the Initiate. Welcome to the Defenders of Albion.", null);
                        player.ReceiveItem(this,WeaponCrush);

                        break;
                    case "thrusting":
                        PromotePlayer(player, (int)eCharacterClass.Armsman, "Here is your Rapier of the Initiate. Welcome to the Defenders of Albion.", null);
                        player.ReceiveItem(this,WeaponThrust);
                        break;
                    case "polearms":
                        PromotePlayer(player, (int)eCharacterClass.Armsman, "Here is your Pike of the Initiate. Welcome to the Defenders of Albion.", null);
                        player.ReceiveItem(this,WeaponPole);
                        break;
                }
            }

            return true;
        }
    }
}
