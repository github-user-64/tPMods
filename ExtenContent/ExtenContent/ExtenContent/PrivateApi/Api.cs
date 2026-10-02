namespace ExtenContent.PrivateApi
{
    /// <summary/>
    public class Api
    {
        private Api() { }

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
    }
}
