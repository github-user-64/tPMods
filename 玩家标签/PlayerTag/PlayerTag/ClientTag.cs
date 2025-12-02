using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace PlayerTag
{
    internal class ClientTag : ServerHelp.PlayerAdditional<TagData>
    {
        private List<TagData> tagDatas = null;

        public void UpdateData(List<TagData> data)
        {
            tagDatas = data;
            UpdateData();
        }

        public override TagData ConverterThrow(int ply)
        {
            Player player = CheckPlayer(ply);
            if (player?.name == null) throw new Exception($"{nameof(PlayerTag)}:玩家数据异常");

            TagData data = tagDatas?.FirstOrDefault(i => i.Name == player.name);
            if (data == null) throw new Exception($"{nameof(PlayerTag)}:标签数据不存在");
            if (data.active == false) throw new Exception($"{nameof(PlayerTag)}:标签已删除");

            return data;
        }

        //不允许修改值
        public override bool SetData(Player player, TagData val) => false;

        //不用检查active是否为false, 在转化数据的时候如果为false就不会设置到数据里
        //public override TagData GetData(Player player, TagData def = null)
        //{
        //    TagData data = base.GetData(player, def);
        //    if (data?.active != true) return def;
        //    return data;
        //}
    }
}
