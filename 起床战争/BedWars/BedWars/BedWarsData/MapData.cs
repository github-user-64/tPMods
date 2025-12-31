using System.Collections.Generic;

namespace BedWars.BedWarsData
{
    public class MapData
    {
        public MapInfoData Info = null;
        public List<SpawItemData> SpawItems = null;
        public List<TeamData> Teams = null;
        public List<CanTileData> CanTileDatas = null;

        public void Check()
        {
            Info.Check(this);
            SpawItems.ForEach(i => i.Check(this));
            Teams.ForEach(i => i.Check(this));
            CanTileDatas.ForEach(i => i.Check(this));
        }
    }
}
