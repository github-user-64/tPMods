using System.Collections.Generic;
using Terraria;

namespace PlayerAccount.Account
{
    internal class PlayerAccount : ModTool.AdditionalData.PlayerAdditionalData<Dictionary<string, string>>
    {
        //不允许设置值
        public override bool SetData(int index, Dictionary<string, string> val) => false;
        //不允许设置值
        protected override void ClearData() { }
        public override bool UpdateDataItem(int index, bool clearOld = false) => false;

        public bool SetAccount(Player player, Dictionary<string, string> acc)
        {
            CheckPlayer(player, out int index);
            if (index == -1) return false;

            if (IndexInRange(index) == false) return false;

            data[index] = acc;
            hasData[index] = true;

            return true;
        }
    }
}
