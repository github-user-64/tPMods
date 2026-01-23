using CommandHelp;
using PlayerAccount.Account;
using System;
using tContentPatch;
using Terraria;

namespace PlayerAccount.Common.FunctionCommand.Acc
{
    /// <summary/>
    public class Acc : CommandObject
    {
        /// <summary>
        /// 账号操作, 服务端
        /// </summary>
        public Acc(Action<string> print = null) : this(null, print){ }

        /// <summary>
        /// 账号操作, <paramref name="player"/>为空视为服务端
        /// </summary>
        public Acc(Player player = null, Action<string> print = null) : base(CommandText.Acc)
        {
            SubCommand.Add(new CommandPrintList(SubCommand, null, print));

            //保存
            CommandMethod accSave = new CommandMethod(CommandText.AccSave);
            accSave.Runing += _ =>
            {
                string msg = AccountFileHelp.BackupSaveData();
                ContentPatch.PrintTry(msg ?? "保存数据成功");
            };
            SubCommand.Add(accSave);

            //更新
            CommandMethod accUpdate = new CommandMethod(CommandText.AccUpdate);
            accUpdate.Runing += _ =>
            {
                ContentPatch.PrintTry(AccountFileHelp.UpdateData() ?? "更新数据成功");
            };
            SubCommand.Add(accUpdate);

            SubCommand.Add(new AccInfo(print));

            SubCommand.Add(new AccDel(print));

            SubCommand.Add(new AccTags(print));

            SubCommand.Add(new AccTagd(print));

            SubCommand.Add(new AccSetMan(print));

            SubCommand.Add(new AccSetAdmin(player, print));
        }
    }
}
