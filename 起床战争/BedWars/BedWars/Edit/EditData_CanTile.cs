using BedWars.BedWarsData;
using Microsoft.Xna.Framework;

namespace BedWars.Edit
{
    public partial class EditData
    {
        /// <summary>
        /// 两个参数都是世界位置
        /// </summary>
        public string AddCanTile(Point pos, Point sizePos)
        {
            if (Data == null) return "地图数据为null";

            pos.X -= DataInfo.pos.X;
            pos.Y -= DataInfo.pos.Y;
            sizePos.X -= DataInfo.pos.X;
            sizePos.Y -= DataInfo.pos.Y;

            bool inmapP = Data.InMapRelative(pos);
            bool inmapSP = Data.InMapRelative(sizePos);

            if (inmapP == false)
            {
                if (inmapSP == false) return null;

                if (pos.X < 0) pos.X = 0;
                if (pos.Y < 0) pos.Y = 0;
                if (pos.X > DataInfo.size.X - 1) pos.X = DataInfo.size.X - 1;
                if (pos.Y > DataInfo.size.Y - 1) pos.Y = DataInfo.size.Y - 1;
            }
            if (inmapSP == false)
            {
                if (sizePos.X < 0) sizePos.X = 0;
                if (sizePos.Y < 0) sizePos.Y = 0;
                if (sizePos.X > DataInfo.size.X - 1) sizePos.X = DataInfo.size.X - 1;
                if (sizePos.Y > DataInfo.size.Y - 1) sizePos.Y = DataInfo.size.Y - 1;
            }

            
            return null;
        }

        public string DelCanTile(Point pos, Point sizePos)
        {
            if (Data == null) return "地图数据为null";

            pos.X -= DataInfo.pos.X;
            pos.Y -= DataInfo.pos.Y;
            sizePos.X -= DataInfo.pos.X;
            sizePos.Y -= DataInfo.pos.Y;

            return null;
        }
    }
}
