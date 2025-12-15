using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;

namespace ModTool.ServerHelp
{
    public static partial class PlayerCanAction
    {
        /// <summary/>
        public delegate bool NewProjectileEvent(Player player, int type, int damage);
        /// <summary>
        /// 能否生成射弹, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static readonly List<NewProjectileEvent> OnCanNewProjectile = new List<NewProjectileEvent>();
        /// <summary>
        /// 能否切换pvp, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static readonly List<Func<Player, bool, bool>> OnCanTogglePVP = new List<Func<Player, bool, bool>>();
        /// <summary>
        /// 能否切换队伍, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static readonly List<Func<Player, int, bool>> OnCanToggleTeam = new List<Func<Player, int, bool>>();
        /// <summary/>
        public class ControlsEventArgs : EventArgs
        {
            /// <summary/>
            public bool controlUp;
            /// <summary/>
            public bool controlDown;
            /// <summary/>
            public bool controlLeft;
            /// <summary/>
            public bool controlRight;
            /// <summary/>
            public bool controlJump;
            /// <summary/>
            public bool controlUseItem;
            /// <summary/>
            public Vector2 position;
        }
        /// <summary>
        /// 能否控制和移动位置, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static readonly List<Func<Player, ControlsEventArgs, bool>> OnCanControls = new List<Func<Player, ControlsEventArgs, bool>>();
    }
}
