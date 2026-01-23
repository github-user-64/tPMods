using CommandHelp;
using ModTool.Utils;
using PlayerAccount.Account;
using System;
using System.Collections.Generic;
using tContentPatch;
using Terraria;

namespace BedWars
{
    internal class GameCommand : Mod
    {
        public override void Load()
        {
            PlayerAccount.Common.PlayerCommand.Cmd += GetServerCmd;
        }

        private List<CommandObject> GetServerCmd(Player player, Dictionary<string, string> account, Action<string> print)
        {
            List<CommandObject> cos = new List<CommandObject>();

            if (account.GetVal(AccountTag.AdminLevel, null) != null)
            {
                cos.Add(new CommandReset(print));
            }

            return cos;
        }
    }
}
