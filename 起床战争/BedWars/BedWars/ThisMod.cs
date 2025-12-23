using BedWars.Common;
using System.Linq;
using System.Reflection;
using tContentPatch;
using tContentPatch.ModLoad;

namespace BedWars
{
    internal class ThisMod : Mod
    {
        public static ModObject mo { get; private set; } = null;
        public static string Dir { get; private set; } = null;

        public override void Load()
        {
            ModObject mo = ContentPatch.GetModObjects()?.FirstOrDefault(i => i.assembly == Assembly.GetExecutingAssembly());

            ThisMod.mo = mo;

            //if (mo == null) throw new Exception($"{nameof(BedWars)}:找不到模组对象");

            //

            Dir = mo.modPath;

            //

            SpawItem.Init();
        }
    }
}
