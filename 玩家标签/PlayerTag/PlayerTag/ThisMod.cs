using System;
using System.IO;
using System.Linq;
using System.Reflection;
using tContentPatch;
using tContentPatch.ModLoad;

namespace PlayerTag
{
    internal class ThisMod : Mod
    {
        public static ModObject mo { get; private set; } = null;
        public static string DirTag { get; private set; } = null;
        public static string DirTagTemp { get; private set; } = null;
        public static string FileTag { get; private set; } = null;
        public static string FileTagTemp { get; private set; } = null;

        public override void Load()
        {
            ModObject mo = ContentPatch.GetModObjects()?.FirstOrDefault(i => i.assembly == Assembly.GetExecutingAssembly());
            
            ThisMod.mo = mo;

            if (mo == null) throw new Exception($"{nameof(ThisMod)}:找不到模组对象");

            //

            DirTag = Path.Combine(ThisMod.mo.modPath, "Tag");
            DirTagTemp = Path.Combine(DirTag, "Temp");
            FileTag = Path.Combine(DirTag, "Tag.txt");
            FileTagTemp = Path.Combine(DirTagTemp, "Tag.txt");

            Directory.CreateDirectory(DirTag);//目录不存在就创建
        }
    }
}
