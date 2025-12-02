using System.Linq;

namespace PlayerTag
{
    public static partial class TagHelp
    {
        public static TagData GetTagData(string name)
        {
            return TagSetting.datas.FirstOrDefault(i => i.Name == name);
        }

        public static bool AddName(string name)
        {
            if (GetTagData(name) != null) return false;

            TagSetting.datas.Add(TagData.NewGroup(name));
            return true;
        }

        public static bool DelName(string name)
        {
            TagData data = GetTagData(name);
            if (data == null) return false;

            return TagSetting.datas.Remove(data);
        }
    }
}
