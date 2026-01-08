using System;
using System.Linq;
using System.Reflection;
using tContentPatch;
using tContentPatch.ModLoad;

namespace PlayerAccount
{
    internal class ThisMod : Mod
    {
        public static ModObject mo { get; private set; } = null;

        public override void Load()
        {
            mo = ContentPatch.GetModObjects()?.FirstOrDefault(i => i.assembly == Assembly.GetExecutingAssembly());
            if (mo == null) throw new Exception($"{nameof(PlayerAccount)}:找不到模组对象");

            //

            Account.AccountHelp.Init();
        }
    }
}
