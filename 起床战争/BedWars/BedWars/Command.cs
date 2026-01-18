using CommandHelp;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using tContentPatch;

namespace BedWars
{
    internal class Command : Mod
    {
        public override List<CommandObject> GetCommands()
        {
            List<CommandObject> cos = new List<CommandObject>();

            CommandMethod c1 = new CommandMethod("起床重置");
            c1.Runing += _ =>
            {
                ModTool.ServerHelp.ToPlayerPrint.PrintToPlayAll("起床战争: 强制重置", Color.Yellow);

                string text = RunRest();
                ContentPatch.PrintTry($"起床战争: {text}");
            };
            cos.Add(c1);

            return cos;
        }

        private static string RunRest()
        {
            if (Run.GameRun.instance == null) return "为null";
            if (Run.GameRun.instance.IsInited == false) return "未初始化";

            Run.GameRun.instance.SetState(Run.GameRun.StateMapInit);

            return "已尝试重置到初始化状态";
        }
    }
}
