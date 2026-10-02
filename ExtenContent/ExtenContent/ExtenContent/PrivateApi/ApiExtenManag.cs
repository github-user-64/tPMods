using ExtenContent.Extens;
using tContentPatch.ModLoad;

namespace ExtenContent.PrivateApi
{
    /// <summary/>
    public class ApiExtenManag
    {
        internal ApiExtenManag() { }

        /// <summary/>
        public void Initialize(ModObject mo)
        {
            ExtenManag.Initialize(mo);
        }

        /// <summary/>
        public void Load()
        {
            ExtenManag.Load();
        }

        /// <summary/>
        public void Unload()
        {
            ExtenManag.Unload();
        }
    }
}
