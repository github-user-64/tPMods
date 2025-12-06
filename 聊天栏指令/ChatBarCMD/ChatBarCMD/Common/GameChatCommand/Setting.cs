using ModTool.Common.UI;
using ModTool.Utils;
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
            public string Head = "//";
            public string HeadToServer = "/";

            public bool ServerEnable = true;
            public string ServerHead = "/";

            public bool EnableTip = true;
        }

        public override string Name => "设置";
        public override string Title => "聊天栏指令: 设置";
        public override Type DataType => typeof(Data);
        public override string FilePath => "set.txt";
        private static Setting instance = null;

        public override void Load(object v)
        {
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

            BindNeedSave(NetMode01.Enable);
            BindNeedSave(NetMode01.Head);
            BindNeedSave(NetMode01.HeadToServer);
            BindNeedSave(NetMode2.Enable);
            BindNeedSave(NetMode2.Head);
            BindNeedSave(CommandTip.Enable);
        }

        private void BindNeedSave<T>(GetSetReset<T> gsr)
        {
            gsr.OnValUpdate += v => NeedSave = true;
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
                Enable = NetMode01.Enable.val,
                Head = NetMode01.Head.val,
                HeadToServer = NetMode01.HeadToServer.val,

                ServerEnable = NetMode2.Enable.val,
                ServerHead = NetMode2.Head.val,

                EnableTip = CommandTip.Enable.val,
            };
        }

        public static void UpdateData(Data data)
        {
            if (data == null) return;

            NetMode01.Enable.val = data.Enable;
            NetMode01.Head.val = data.Head;
            NetMode01.HeadToServer.val = data.HeadToServer;

            NetMode2.Enable.val = data.ServerEnable;
            NetMode2.Head.val = data.ServerHead;

            CommandTip.Enable.val = data.EnableTip;
        }

        public override UIElement GetUI()
        {
            UIScrollViewer2 sv = new UIScrollViewer2();
            sv.Width.Precent = 1;
            sv.Height.Precent = 1;

            sv.AddChild(new tContentPatch.Content.UI.ModSet.UIItemTitle(null, "单人和客户端"));
            sv.AddChild(new UIItemSwitch(NetMode01.Enable, null, "启用指令"));
            sv.AddChild(new UIItemTextBox<string>(NetMode01.Head, s => s, null, "指令头"));
            sv.AddChild(new UIItemTextBox<string>(NetMode01.HeadToServer, s => s, null, "指令头,发送到服务端"));
            sv.AddChild(new tContentPatch.Content.UI.ModSet.UIItemTitle(null, "服务端"));
            sv.AddChild(new UIItemSwitch(NetMode2.Enable, null, "启用指令"));
            sv.AddChild(new UIItemTextBox<string>(NetMode2.Head, s => s, null, "指令头"));

            return sv;
        }

        public static void SaveData()
        {
            if (instance == null) return;

            instance.NeedSave = true;
            instance.Save();
        }
    }
}
