using System;
using System.IO;
using System.Linq;
using System.Reflection;
using tContentPatch;
using tContentPatch.ModLoad;

namespace PlayerGroup
{
    internal class INoThisIsWhat : Mod
    {
        public static ModObject mo { get; private set; } = null;
        public static string DirGroup { get; private set; } = null;
        public static string DirTempGroupSaveAll { get; private set; } = null;

        public override void Load()
        {
            ModObject mo = ContentPatch.GetModObjects()?.FirstOrDefault(i => i.assembly == Assembly.GetExecutingAssembly());
            
            INoThisIsWhat.mo = mo;

            if (mo == null) throw new Exception($"{nameof(PlayerGroup)}:找不到模组对象");

            //

            DirGroup = Path.Combine(INoThisIsWhat.mo.modPath, "Group");//分组目录
            DirTempGroupSaveAll = Path.Combine(DirGroup, "Temp");

            Directory.CreateDirectory(DirGroup);//目录不存在就创建
        }
    }
}
