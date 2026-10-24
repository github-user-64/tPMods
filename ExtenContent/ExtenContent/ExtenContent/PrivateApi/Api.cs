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
        public ApiEquipLoader Equip = new ApiEquipLoader();
        /// <summary/>
        public ApiItemLoader Item = new ApiItemLoader();
        /// <summary/>
        public ApiProjectileLoader Projectile = new ApiProjectileLoader();
        /// <summary/>
        public ApiBuffLoader Buff = new ApiBuffLoader();
        /// <summary/>
        public ApiSurfaceBackgroundLoader Back = new ApiSurfaceBackgroundLoader();
        /// <summary/>
        public ApiMenuStyleLoader MenuStyle = new ApiMenuStyleLoader();

        /// <summary/>
        public ApiPatchPlayerLoader PatchPlayer = new ApiPatchPlayerLoader();
        /// <summary/>
        public ApiPatchMainLoader PatchMain = new ApiPatchMainLoader();
    }
}
