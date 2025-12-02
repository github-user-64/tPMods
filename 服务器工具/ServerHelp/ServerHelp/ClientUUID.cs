using Terraria;
using Terraria.ID;

namespace ServerHelp
{
    internal class ClientUUID : PlayerAdditional<string>
    {
        internal static ClientUUID instance = null;

        internal static void Init()
        {
            instance = new ClientUUID();
        }

        internal static string GetUUID(Player player)
        {
            return instance.GetData(player);
        }

        public ClientUUID()
        {
            PatchGame.PatchMessageBuffer.OnGetDataPo += GetData;
        }

        private void GetData(MessageBuffer This, int start, int length, int messageType)
        {
            if (messageType != MessageID.Unknown68) return;

            int index = This.whoAmI;

            Player p = CheckPlayer(index);
            if (p == null) return;

            data[index] = This.reader.ReadString();
            name[index] = p.name;
        }

        public override void ConnectedPlayer(int ply) { }
    }
}
