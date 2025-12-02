using System;
using System.Collections.Generic;
using tContentPatch;
using Terraria;

namespace PlayerTag
{
    internal class TagSetting : ModSetting
    {
        public override bool HasUI => false;//不添加ui
        /// <summary>
        /// 标签数据, 建议使用<see cref="TagHelp"/>中的方法修改数据, 修改数据注意和<see cref="ClientTag"/>做同步
        /// </summary>
        public static List<TagData> datas { get; private set; } = null;
        private static ClientTag ct = null;

        public override void Load(object v)
        {
            ct = new ClientTag();
            List<TagData> data = v as List<TagData> ?? new List<TagData>();

            try
            {
                UpdateData(data);
            }
            catch (Exception ex)
            {
                throw new Exception($"更新数据失败:{ex.Message}", ex);
            }
        }

        public override object Read()
        {
            try
            {
                return TagHelp.Read(ThisMod.FileTag);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 更新分组数据
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UpdateData(List<TagData> data)
        {
            data?.ForEach(i =>
            {
                if (i != null) i.active = true;
            });

            CheckTagData(data);

            datas = data;

            ct.UpdateData(datas);
        }

        /// <summary>
        /// 检查和恢复数据并删除<see cref="TagData.active"/>为<see langword="false"/>项
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static void CheckTagData(List<TagData> datas)
        {
            if (datas == null) throw new ArgumentNullException(nameof(datas));

            datas.RemoveAll(i => i == null || i.Name == null || i.active == false);

            foreach (TagData data in datas)
            {
                if (data.Tag == null) data.Tag = new Dictionary<string, string>();
                if (data.TagTemp == null) data.TagTemp = new Dictionary<string, string>();
            }
        }

        /// <summary>
        /// 获取标签数据, 不存在返回<paramref name="def"/>
        /// </summary>
        public static TagData GetTagData(Player player, TagData def = null)
        {
            if (ct == null) return def;

            return ct.GetData(player, def);
        }
    }
}
