using Terraria;

namespace PlayerTag
{
    public static partial class TagHelp
    {
        /// <summary>
        /// 获取标签数据, 不存在返回<paramref name="def"/>
        /// </summary>
        public static TagData GetTagData(string name, TagData def = null)
        {
            if (TagSetting.datas == null) return def;

            foreach (TagData data in TagSetting.datas)
            {
                if (data == null) continue;
                if (data.active == false) continue;
                if (data.Name != name) continue;

                return data;
            }

            return def;
        }

        /// <summary>
        /// 获取标签数据, 不存在返回<paramref name="def"/>
        /// </summary>
        public static TagData GetTagData(this Player player, TagData def = null)
        {
            return TagSetting.GetTagData(player, def);
        }

        public static bool AddName(string name)
        {
            if (GetTagData(name) != null) return false;

            TagSetting.datas?.Add(TagData.NewGroup(name));
            return true;
        }

        public static bool DelName(string name)
        {
            TagData data = GetTagData(name);
            if (data == null) return false;

            bool ok = TagSetting.datas.Remove(data);

            if (ok) data.active = false;

            return ok;
        }
    }
}
