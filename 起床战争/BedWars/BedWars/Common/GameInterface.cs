using System;
using System.Collections.Generic;
using tContentPatch;
using Terraria;
using Terraria.UI;

namespace BedWars.Common
{
    internal class GameInterface : PatchMain
    {
        public static List<Action> OnDraw => new List<Action>();
        public static UIState UI { get; private set; } = null;
        private static UserInterface ui = null;

        public override void SetupDrawInterfaceLayersPostfix(List<GameInterfaceLayer> gameInterfaceLayers)
        {
            if (ui == null)
            {
                ui = new UserInterface();
                UI = new UIState();
                ui.SetState(UI);
            }

            Setup(gameInterfaceLayers, InterfaceScaleType.UI,
                "Vanilla: Inventory",
                "StaticTile.BedWars: Inventory Postfix UI", () =>
                {
                    try
                    {
                        ui.Update(Main.gameTimeCache);
                        ui.Draw(Main.spriteBatch, Main.gameTimeCache);
                    }
                    catch { }

                    return true;
                });

            Setup(gameInterfaceLayers, InterfaceScaleType.Game,
                "Vanilla: Laser Ruler",
                "StaticTile.BedWars: Laser Ruler Postfix Game", () =>
                {
                    foreach (Action i in OnDraw)
                    {
                        try
                        {
                            i?.Invoke();
                        }
                        catch { }
                    }

                    return true;
                });
        }

        public static void Setup(List<GameInterfaceLayer> gameInterfaceLayers, InterfaceScaleType scaleType,
            string name, string nameAdd, GameInterfaceDrawMethod drawMethod)
        {
            int index = gameInterfaceLayers.FindIndex(i => i.Name == name);
            if (index == -1) return;

            gameInterfaceLayers.Insert(index, new LegacyGameInterfaceLayer(nameAdd, drawMethod, scaleType));
        }
    }
}
