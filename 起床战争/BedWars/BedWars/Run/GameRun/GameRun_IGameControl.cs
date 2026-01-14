using BedWars.BedWarsData;
using BedWars.Run.StateActions;
using ModTool.Common;
using ModTool.Utils.GetDataEventArgs;
using System;
using Terraria;

namespace BedWars.Run
{
    public partial class GameRun : IGameControl
    {
        void IGameControl.LoadTry(Action<string> print)
        {
            try
            {
                if (IsLoaded)
                {
                    print?.Invoke("已加载");
                    return;
                }
                if (Main.dedServ == false)
                {
                    print?.Invoke("不是服务端");
                    return;
                }

                print?.Invoke($"地图目录:{ThisMod.DirMapData}");
                print?.Invoke("加载地图数据");
                MapData temp = DataFileHelp.ReadData(ThisMod.DirMapData);

                Data = temp;

                IsLoaded = true;

                print?.Invoke("加载完成");
            }
            catch (Exception ex)
            {
                print?.Invoke($"加载失败:{ex.Message}");
            }
        }

        void IGameControl.InitTry(Action<string> print)
        {
            try
            {
                if (IsInited)
                {
                    print?.Invoke("已初始化");
                    return;
                }
                if (IsLoaded == false)
                {
                    print?.Invoke("未加载");
                    return;
                }

                print?.Invoke("检查数据");
                DataCheck.Repair(Data);
                DataCheck.CheckMapData(Data);
                DataInventory.inventory[DataInventory.inventory.Count - 1].Copy(null);//最后一个是鼠标物品,清空它

                Team = new GameTeam(Data);

                States = new IStateAction[]
                {
                    null,
                    new SMapInit(this),
                    new SReadyGame(this),
                    new SGameing(this),
                    new SGameEnd(this),
                };

                Init();

                ModTool.ServerHelp.Utils.ServerSideCharacter(true);

                IsInited = true;

                print?.Invoke("初始化完成");
            }
            catch (Exception ex)
            {
                print?.Invoke($"初始化失败:{ex.Message}");
            }
        }

        void IGameControl.Update(uint gametime)
        {
            if (CantRun()) return;

            if (HasUpdateState != null)
            {
                Action foo = HasUpdateState;
                HasUpdateState = null;

                foo();
            }
            
            NowState?.Update(gametime);
        }

        void IGameControl.OnPlayJoinGame(Player player)
        {
            if (CantRun()) return;

            NowState?.OnPlayJoinGame(player);
        }

        void IGameControl.OnPlayLeftGame(Player player)
        {
            if (CantRun()) return;

            NowState?.OnPlayLeftGame(player);
        }

        void IGameControl.OnPlayLogined(Player player)
        {
            if (CantRun()) return;

            NowState?.OnPlayLogin(player);
        }

        public void OnPlayJoinGameTryAutoLoginPo(Player player)
        {
            if (CantRun()) return;

            NowState?.OnPlayJoinGameTryAutoLoginPo(player);
        }

        bool IGameControl.PlayCanActionTile(ClassTileEventArgs e)
        {
            if (CantRun()) return true;

            return NowState?.PlayCanActionTile(e) ?? false;
        }

        bool IGameControl.PlayCanAction(GetDataEventArgs e)
        {
            if (CantRun()) return true;

            return NowState?.PlayCanAction(e) ?? false;
        }

        public void OnGetDataPr(Player player, int messageType)
        {
            if (CantRun()) return;

            NowState?.OnGetDataPr(player, messageType);
        }

        public void OnGetDataPo(Player player, int messageType)
        {
            if (CantRun()) return;

            NowState?.OnGetDataPo(player, messageType);
        }

        public bool ModifyShop(ModifyShop.ItemData[] items, NPC npc, Player player)
        {
            if (CantRun()) return false;

            return NowState?.ModifyShop(items, npc, player) ?? false;
        }

        /// <summary>
        /// 不可以运行. 未初始化, 状态类型为<see cref="StateNone"/>
        /// </summary>
        private bool CantRun()
        {
            if (IsInited == false) return true;
            if (NowStateType == StateNone) return true;
            return false;
        }
    }
}
