using tContentPatch.Content.UI;

namespace BedWars.Common.UI
{
    /// <summary>
    /// <see cref="UIWrapPanel"/>, 自动设置高度
    /// </summary>
    public class UIWrapPanel2 : UIWrapPanel
    {
        public override void Recalculate()
        {
            base.Recalculate();

            this.UpdateContainer_Height();
        }
    }
}
