//using CommandHelp;
//using ModTool.Command;
//using System;
//using System.Collections.Generic;
//using tContentPatch;
//using Terraria;

//namespace test2.MT
//{
//    internal class 测试指令2 : Mod
//    {
//        public override void Loaded()
//        {
//            ChatBarCMD.Common.GameChatCommand.NetMode01.CMD.Add((i, p) =>
//            {
//                return GetCMD(Main.LocalPlayer, p);
//            });
//        }

//        public static List<CommandObject> GetCMD(Player player, Action<string> print)
//        {
//            List<CommandObject> list = new List<CommandObject>();

//            #region 输出
//            CommandMethod p = new CommandMethod("输出", 1);
//            p.SubCommand.Add(new CommandString2());
//            p.Runing += args =>
//            {
//                print($"{args[0]}");
//            };

//            list.Add(p);
//            #endregion

//            #region
//            CommandMethod gameMode = new CommandMethod("gameMode", 1);
//            gameMode.SubCommand.Add(new CommandeEnum(false, "0", "1", "2"));
//            gameMode.Runing += args =>
//            {
//                switch ($"{args[0]}")
//                {
//                    case "0": print("游戏模式生存"); break;
//                    case "1": print("游戏模式创造"); break;
//                    case "2": print("游戏模式旁观"); break;
//                }
//            };

//            list.Add(gameMode);
//            #endregion

//            return list;
//        }
//    }
//}
