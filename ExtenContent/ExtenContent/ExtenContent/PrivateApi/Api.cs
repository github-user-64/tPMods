using System;

namespace ExtenContent.PrivateApi
{
    /// <summary/>
    public class Api
    {
        private Api() { }

        /// <summary/>
        public void Log(string s) => ThisMod.Log(s);
        /// <summary/>
        public void Log(out DateTime time, string s = null) => ThisMod.Log(out time, s);
        /// <summary/>
        public void Log(string s, DateTime time) => ThisMod.Log(s, time);
        /// <summary/>
        public ApiExtenManag ExtenManag = new ApiExtenManag();
        /// <summary/>
        public ApiPatchPlayerLoader PatchPlayer = new ApiPatchPlayerLoader();
        /// <summary/>
        public ApiEquipLoad Equip = new ApiEquipLoad();
        /// <summary/>
        public ApiItemLoad Item = new ApiItemLoad();
        /// <summary/>
        public ApiProjectileLoad Projectile = new ApiProjectileLoad();
        /// <summary/>
        public ApiBuffLoader Buff = new ApiBuffLoader();
    }
}
