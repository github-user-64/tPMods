namespace ExtenContent.Extens
{
    /// <summary/>
    public interface ILoadable
    {
        /// <summary/>
        void Load();
        /// <summary/>
        void Unload();
        /// <summary/>
        bool CanLoad();
    }
}
