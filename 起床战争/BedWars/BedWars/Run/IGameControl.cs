using ModTool.Utils.GetDataEventArgs;
using System;
using Terraria;

namespace BedWars.Run
{
    public interface IGameControl
    {
        void LoadTry(Action<string> print = null);

        void InitTry(Action<string> print = null);

        void OnPlayJoinGame(Player player);

        void OnPlayLeftGame(Player player);

        void OnPlayLogined(Player player);

        void OnPlayJoinGameTryAutoLoginPo(Player player);

        void Update(uint gametime);

        bool PlayCanActionTile(ClassTileEventArgs e);

        bool PlayCanAction(GetDataEventArgs e);
    }
}
