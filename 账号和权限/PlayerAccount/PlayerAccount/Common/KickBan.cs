using ModTool.ServerHelp;
using ModTool.Utils;
using PlayerAccount.Account;
using System.Collections.Generic;
using tContentPatch;
using Terraria;
using Terraria.ID;

namespace PlayerAccount.Common
{
    /// <summary>
    /// 踢出被封禁的玩家
    /// </summary>
    internal class KickBan : Mod
    {
        public override void Load()
        {
            ModTool.PatchGame.PMessageBuffer.OnGetDataPo.Add(OnSyncPlayer);
            ModTool.PatchGame.PMessageBuffer.OnGetDataPo.Add(OnUUID);
            ModTool.PatchGame.PNetMessage.OnSyncConnectedPlayerPr += OnSyncConnectedPlayerPr;
            AccountHelp.OnLogined += OnLogined;
        }

        private void OnLogined(Player obj)
        {
            if (obj == null) return;
            Dictionary<string, string> acc = obj.GetAccount();
            if (acc == null) return;

            if (acc.HasKey(AccountTag.Ban) == false) return;

            KickPlay.Kick(obj.whoAmI, $"该账号被封禁:{acc.GetVal(AccountTag.Ban)}");
        }

        private void OnSyncPlayer(MessageBuffer This, int start, int length, int messageType)
        {
            if (messageType != MessageID.SyncPlayer) return;
            if (Main.dedServ == false) return;

            This.reader.ReadByte();
            This.reader.ReadByte();
            This.reader.ReadByte();
            string name = This.reader.ReadString().Trim().Trim();

            if (DataBanName.instance.datas.Contains(name)) KickPlay.Kick(This.whoAmI, "该名称被封禁,请使用其它名称");
        }

        private void OnUUID(MessageBuffer This, int start, int length, int messageType)
        {
            if (messageType != MessageID.Unknown68) return;
            if (Main.dedServ == false) return;

             string uuid = This.reader.ReadString();

            if (DataBanUUID.instance.datas.Contains(uuid)) KickPlay.Kick(This.whoAmI, "该账号被封禁");
        }

        private void OnSyncConnectedPlayerPr(int ply)
        {
            if (Main.dedServ == false) return;
            if (Main.player?.IndexInRange(ply) != true) return;

            if (ServerConfig.data.BanIP == false) return; 

            Player player = Main.player[ply];
            if (player == null) return;

            string ip = player.GetIP();
            string port = player.GetPort().ToString();

            if (DataBanIP.instance.datas.Exists(i =>
            i.IP == port && (i.Port == null || i.Port == port)))
                KickPlay.Kick(ply, "该账号被封禁");
        }
    }
}
