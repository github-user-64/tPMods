using Microsoft.Xna.Framework;
using ModTool.Utils;
using PlayerAccount.Account;
using System;
using System.Collections.Generic;
using Terraria;

namespace PlayerAccount.Common
{
    /// <summary>
    /// 修改玩家聊天
    /// </summary>
    internal class SetChat
    {
        public class ChatData
        {
            public string tag = null;
            public string text = null;
            public Color color = Color.White;
        }

        public class ChatConfig : DataABackup<List<ChatData>>
        {
            public override bool HasUI => false;
            public override string FilePath => "修改带标签账号聊天.txt";
            internal static ChatConfig instance = null;

            public override void CheckData(List<ChatData> data)
            {
                if (data == null) throw new ArgumentNullException(nameof(data));
                data.RemoveAll(i => i == null || i.tag == null);
            }

            public override void Load(object v)
            {
                instance = this;

                if (v is List<ChatData> data)
                {
                    CheckData(data);

                    datas = data;
                }
                else
                {
                    datas = new List<ChatData>
                    {
                        new ChatData() { tag = "禁言", text = null },
                        new ChatData() { tag = AccountTag.AdminLevel, text = "<{0}>: {1}", color = new Color(0f, 1f, 1f) },
                    };
                    Save();
                }
            }
        }

        public static bool foo(Player player, string text, Color color, int excludedPlayer)
        {
            Dictionary<string, string> acc = AccountHelp.GetAccount(player);

            if (acc == null)
            {
                string msg = $"(未登录){player.name}: {text}";

                ModTool.ServerHelp.PrintTo.PrintToPlayAll(msg, color, excludedPlayer);
                return false;
            }

            List<ChatData> list = ChatConfig.instance.datas;

            foreach (ChatData data in list)
            {
                if (acc.HasKey(data.tag) == false) continue;
                if (data.text == null) return false;//为null直接说不了话

                try
                {
                    string msg = string.Format(data.text, player.name, text);

                    ModTool.ServerHelp.PrintTo.PrintToPlayAll(msg, data.color, excludedPlayer);
                }
                catch { }

                return false;
            }

            return true;
        }
    }
}
