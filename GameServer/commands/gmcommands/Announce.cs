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
using System.Collections.Generic;
using DOL.GS.PacketHandler;
using DOL.Language;

namespace DOL.GS.Commands
{
    [Cmd(
        "&announce",
        ePrivLevel.GM,
        "GMCommands.Announce.Description",
        "GMCommands.Announce.Usage")]
    public class AnnounceCommandHandler : AbstractCommandHandler, ICommandHandler
    {
        public void OnCommand(GameClient client, string[] args)
        {
            if (args.Length < 3)
            {
                DisplaySyntax(client);
                return;
            }

            string message = string.Join(" ", args, 2, args.Length - 2);
            if (message == string.Empty)
            {
                return;
            }

            switch (args.GetValue(1).ToString().ToLower())
            {
                case "log":
                    {
                        foreach (GameClient clients in WorldMgr.GetAllPlayingClients())
                        {
                            if (clients != null)
                            {
                                clients.Out.SendMessage(LanguageMgr.GetTranslation(clients, "GMCommands.Announce.LogAnnounce", message), eChatType.CT_Important, eChatLoc.CL_SystemWindow);
                            }
                        }

                        break;
                    }

                case "window":
                    {
                        var messages = new List<string>();
                        messages.Add(message);

                        foreach (GameClient clients in WorldMgr.GetAllPlayingClients())
                        {
                            if (clients != null)
                            {
                                clients.Player.Out.SendCustomTextWindow(LanguageMgr.GetTranslation(clients, "GMCommands.Announce.WindowAnnounce", client.Player.Name), messages);
                            }
                        }

                        break;
                    }

                case "send":
                    {
                        foreach (GameClient clients in WorldMgr.GetAllPlayingClients())
                        {
                            if (clients != null)
                            {
                                clients.Out.SendMessage(LanguageMgr.GetTranslation(clients, "GMCommands.Announce.SendAnnounce", message), eChatType.CT_Send, eChatLoc.CL_ChatWindow);
                            }
                        }

                        break;
                    }

                case "center":
                    {
                        foreach (GameClient clients in WorldMgr.GetAllPlayingClients())
                        {
                            if (clients != null)
                            {
                                clients.Out.SendMessage(message, eChatType.CT_ScreenCenter, eChatLoc.CL_SystemWindow);
                            }
                        }

                        break;
                    }

                case "confirm":
                    {
                        foreach (GameClient clients in WorldMgr.GetAllPlayingClients())
                        {
                            if (clients != null)
                            {
                                clients.Out.SendDialogBox(eDialogCode.SimpleWarning, 0, 0, 0, 0, eDialogType.Ok, true, LanguageMgr.GetTranslation(clients, "GMCommands.Announce.ConfirmAnnounce", client.Player.Name, message));
                            }
                        }

                        break;
                    }

                default:
                    {
                        DisplaySyntax(client);
                        return;
                    }
            }
        }
    }
}
