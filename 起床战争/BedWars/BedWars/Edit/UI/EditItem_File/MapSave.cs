using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using Terraria;

namespace BedWars.Edit.UI.EditItem_File
{
    internal class MapSave : ModTool.Common.UI.UIItemTextButton
    {
        private bool tasking = false;

        public MapSave(string btnText, string text) : base(btnText, null, text)
        {
            OnClick += () =>
            {
                if (tasking)
                {
                    Main.NewText("加载中");
                    return;
                }
                tasking = true;

                _ = Task.Run(() =>
                {
                    try
                    {
                        string ex = EditData.instance.SaveData();

                        Main.NewText(ex ?? "保存地图成功");
                    }
                    catch { }
                })
                .ContinueWith(_ => tasking = false);
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            string path = EditData.instance.DirMapData;
            if (path == null) return;
            if (path.Length > 40) path = $"{path.Substring(0, 20)}...{path.Substring(path.Length - 20, 20)}";

            MouseText = path;
        }
    }
}
