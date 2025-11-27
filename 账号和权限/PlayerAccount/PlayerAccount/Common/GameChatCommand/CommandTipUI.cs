using tContentPatch;
using tContentPatch.Content.UI;
using Terraria.UI;

namespace PlayerAccount.Common.GameChatCommand
{
    internal class CommandTipUI : ModSetting
    {
        public override string Name => "设置";
        public override string Title => "账号和权限: 设置";

        public override UIElement GetUI()
        {
            UIScrollViewer2 sv = new UIScrollViewer2();
            sv.Width.Precent = 1;
            sv.Height.Precent = 1;

            sv.AddChild(new UI.UIItemSwitchBind(CommandTip.Enable, null, "显示指令提示") { MouseText = "此设置不会保存" });

            return sv;
        }
    }
}
