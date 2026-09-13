using Microsoft.Xna.Framework;
using System.Collections.Generic;
using tContentPatch;
using Terraria;
using Terraria.UI;

namespace ExtenContent
{
    internal class ModifyInterfaceLayers : PatchMain
    {
        public UIState state = null;
        private UserInterface ui = null;

        public override void SetupDrawInterfaceLayersPostfix(List<GameInterfaceLayer> gameInterfaceLayers)
        {
            int index = gameInterfaceLayers.FindIndex(i => i.Name == "Vanilla: Inventory");
            if (index == -1) return;

            state = new UIState();
            ui = new UserInterface();

            gameInterfaceLayers.Insert(index, new LegacyGameInterfaceLayer(
                "StaticTile.ExtenContent: Inventory Prefix",
                () =>
                {
                    ui.Draw(Main.spriteBatch, Main.gameTimeCache);//绘制ui
                    return true;
                },
                InterfaceScaleType.Game));

            state.Append(new DeBug.DeBug.DrawHeldItem());
        }

        public override void UpdateUIStatesPostfix(GameTime gameTime)
        {
            if (ui == null) return;

            if (Main.gameMenu || Main.mapFullscreen)//在游戏外或打开大地图时
            {
                ui.SetState(null);//禁用ui
                return;
            }

            ui.SetState(state);//启用ui
            ui.Update(gameTime);//更新ui
        }
    }
}
