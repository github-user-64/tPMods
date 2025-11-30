using CommandHelp;
using Microsoft.Xna.Framework;
using PlayerAccount.Account;
using PlayerGroup;
using PlayerGroup.CMDUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria;

namespace PlayerAccount.Common
{
    /// <summary>
    /// 玩家指令
    /// </summary>
    public static class PlayCommand
    {
        /// <summary>
        /// 玩家指令
        /// </summary>
        /// <param name="player"></param>
        /// <param name="aleve">管理员等级, 不是管理员为<see langword="null"/></param>
        /// <param name="oleve">普通玩家等级, 不是普通玩家为<see langword="null"/></param>
        /// <param name="bLeve">封禁等级, 不是封禁为<see langword="null"/></param>
        /// <param name="print"></param>
        /// <returns></returns>
        public delegate List<CommandObject> PlayCMD(Player player, int? aleve, int? oleve, int? bLeve, Action<string> print);
        /// <summary>
        /// 玩家指令
        /// </summary>
        public static List<PlayCMD> PlayCMDList { get; internal set; } = new List<PlayCMD>();
        
        static PlayCommand()
        {
            PlayCMDList.Add(GetA0);
            PlayCMDList.Add(GetAll);
        }

        /// <summary>
        /// 运行指令
        /// </summary>
        /// <param name="text"></param>
        /// <param name="player"></param>
        internal static void InputCMD(string text, Player player)
        {
            if (text == null) return;
            if (player == null) return;
            if (player.name == null) return;

            Action<string> print = s => PrintTo.PrintToPlay(player.whoAmI, s, Color.Yellow);

            if (text.Length < 1)
            {
                print("输入?获取指令信息");
                return;
            }

            string exText = tContentPatch.Command.Utils.CommandRun(text, GetPlayCMD(player, print));
            if (exText != null) print(exText);
        }

        /// <summary>
        /// 获取玩家可用指令
        /// </summary>
        /// <param name="player"></param>
        /// <param name="print"></param>
        /// <returns></returns>
        private static List<CommandObject> GetPlayCMD(Player player, Action<string> print)
        {
            int? aLevel = player.GetPlayAdministratorLevel();
            int? oLevel = player.GetPlayOrdinaryLevel();
            int? bLevel = player.GetPlayBanLevel();

            List<CommandObject> cos = new List<CommandObject>();
            cos.Add(new CommandHelpList(cos, print: print));

            PlayCMDList.RemoveAll(i => i == null);
            foreach (PlayCMD i in PlayCMDList)
            {
                try
                {
                    List<CommandObject> list = i?.Invoke(player, aLevel, oLevel, bLevel, print);
                    if (list == null) continue;
                    cos.AddRange(list);
                }
                catch
                {
                    Debug.WriteLine($"{nameof(PlayCommand)}:获取指令异常, 跳过该指令");
                }
            }

            return cos;
        }

        /// <summary>
        /// 获取0级管理员可用指令
        /// </summary>
        private static List<CommandObject> GetA0(Player player, int? aLevel, int? olevel, int? bLeve, Action<string> print)
        {
            if (aLevel == null) return null;//不是管理员
            if (aLevel != 0) return null;//等级不是0
            if (bLeve != null) return null;//被封禁

            return Command.GetCMD(print);
        }

        /// <summary>
        /// 获取所有玩家可用指令
        /// </summary>
        private static List<CommandObject> GetAll(Player player, int? aLevel, int? olevel, int? bLeve, Action<string> print)
        {
            List<CommandObject> list = new List<CommandObject>();

            CommandMethod playing = new CommandMethod("playing");
            playing.Runing += _ => FunctionCommand.playing(print);

            list.Add(playing);

            return list;
        }
    }
}
