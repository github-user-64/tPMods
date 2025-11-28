using System;
using tContentPatch;
using tContentPatch.Content.UI;
using Terraria.UI;

namespace ChatBarCMD.Common.GameChatCommand
{
    internal class Setting : ModSetting
    {
        public class Data
        {
            public bool Enable = true;
            public string Head = "/";
            public bool EnableTip = true;
        }

        public override string Name => "设置";
        public override string Title => "聊天栏指令: 设置";
        public override Type DataType => typeof(Data);
        public override string FilePath => "set.txt";
        internal static Setting instance = null;

        public override void Load(object v)
        {
            if (instance == null)
            {
                GameChatCommand.Enable.OnValUpdate += _ => OnDataUpdate();
                GameChatCommand.CMDHead.OnValUpdate += _ => OnDataUpdate();
                CommandTip.Enable.OnValUpdate += _ => OnDataUpdate();
            }
            instance = this;

            if (v is Data data)
            {
                UpdateData(data);
            }
            else
            {
                SetDefault();
                Save();
            }
        }

        public override void SetDefault()
        {
            UpdateData(new Data());
            NeedSave = true;
        }

        public override object GetSaveData()
        {
            return new Data()
            {
                Enable = GameChatCommand.Enable.val,
                Head = GameChatCommand.CMDHead.val,
                EnableTip = CommandTip.Enable.val,
            };
        }

        public static void UpdateData(Data data)
        {
            if (data == null) return;

            GameChatCommand.Enable.val = data.Enable;
            GameChatCommand.CMDHead.val = data.Head;
            CommandTip.Enable.val = data.EnableTip;
        }

        public static void SaveData()
        {
            if (instance == null) return;
            instance.NeedSave = true;
            instance.Save();
        }

        public static void OnDataUpdate()
        {
            if (instance == null) return;
            instance.NeedSave = true;
        }

        public override UIElement GetUI()
        {
            UIScrollViewer2 sv = new UIScrollViewer2();
            sv.Width.Precent = 1;
            sv.Height.Precent = 1;

            sv.AddChild(new UI.UIItemSwitchBind(GameChatCommand.Enable, null, "启用"));
            sv.AddChild(new UI.UIItemTextBoxBind<string>(GameChatCommand.CMDHead, s => s, null, "指令头"));
            sv.AddChild(new UI.UIItemSwitchBind(CommandTip.Enable, null, "显示指令提示"));

            return sv;
        }
    }
}
