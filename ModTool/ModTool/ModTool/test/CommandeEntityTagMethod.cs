using CommandHelp;
using System;
using System.Collections.Generic;
using Terraria;

namespace ModTool.test
{
    /// <summary>
    /// 实体标签操作
    /// </summary>
    public class CommandeEntityTagMethod : CommandMethod
    {
        /// <summary/>
        public Player player = null;
        /// <summary/>
        public Action<string> print = null;

        /// <summary/>
        public CommandeEntityTagMethod(string text, Player player, Action<string> print) : base(text, 3)
        {
            this.player = player;
            this.print = print;

            SubCommand.Add(new CommandPrintList(SubCommand, print: print));

            CommandeEnum at = new CommandeEnumEntity(player, print, false);
            at.SubCommand.Add(new CommandPrintList(at.SubCommand, "标签", print));
            SubCommand.Add(at);

            CommandString tag = new CommandString() { TipText = "标签" };
            tag.SubCommand.Add(new CommandPrintList(tag.SubCommand, "值", print));
            at.SubCommand.Add(tag);

            CommandString val = new CommandString(true) { TipText = "值" };
            tag.SubCommand.Add(val);
        }

        /// <inheritdoc/>
        public override object OnRuning(ref int index, List<CommandObject> commandList, object[] args)
        {
            Entity entity = (Entity)args[0];
            string tag = (string)args[1];
            string val = (string)args[2];

            OnTagMethod(entity, tag, val);

            return null;
        }

        /// <summary/>
        public virtual void OnTagMethod(Entity entity, string tag, string val)
        {

        }
    }
}
