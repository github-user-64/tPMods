using CommandHelp;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BedWars
{
    internal class CommandReset : CommandMethod
    {
        private readonly Action<string> print = null;

        public CommandReset(Action<string> print = null) : base("起床重置")
        {
            this.print = print;
        }

        public override object OnRuning(ref int index, List<CommandObject> commandList, object[] args)
        {
            ModTool.ServerHelp.ToPlayerPrint.PrintToPlayAll("起床战争: 强制重置", Color.Yellow);

            string text = RunRest();
            print?.Invoke($"起床战争: {text}");

            return null;
        }

        private static string RunRest()
        {
            if (BedWars.Run.GameRun.instance == null) return "为null";
            if (BedWars.Run.GameRun.instance.IsInited == false) return "未初始化";

            BedWars.Run.GameRun.instance.SetState(BedWars.Run.GameRun.StateMapInit);

            return "已尝试重置到初始化状态";
        }
    }
}
