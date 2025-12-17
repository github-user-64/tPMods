using Microsoft.Xna.Framework;
using Terraria;

namespace ModTool.Utils.GetDataEventArgs
{
    /// <summary/>
    public class GetDataEventArgs
    {
        /// <summary/>
        public Player player;
    }

    /// <summary>
    /// 同步射弹
    /// </summary>
    public class SyncProjectileEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int identity;
        /// <summary/>
        public int owner;
        /// <summary/>
        public int type;
        /// <summary/>
        public int damage;
    }

    /// <summary>
    /// 切换pvp
    /// </summary>
    public class TogglePVPEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public bool hostile;
    }

    /// <summary>
    /// 切换队伍
    /// </summary>
    public class ToggleTeamEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int team;
    }

    /// <summary>
    /// 控制和移位
    /// </summary>
    public class ControlsEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public bool controlUp;
        /// <summary/>
        public bool controlDown;
        /// <summary/>
        public bool controlLeft;
        /// <summary/>
        public bool controlRight;
        /// <summary/>
        public bool controlJump;
        /// <summary/>
        public bool controlUseItem;
        /// <summary/>
        public Vector2 position;
    }

    /// <summary>
    /// 操作方块
    /// </summary>
    public class TileManipulationEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public byte manipulationType;
        /// <summary/>
        public int x;
        /// <summary/>
        public int y;
        /// <summary/>
        public short tileType;
        /// <summary/>
        public int placeStyle;
    }

    /// <summary>
    /// 放置对象
    /// </summary>
    public class PlaceObjectEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int x;
        /// <summary/>
        public int y;
        /// <summary/>
        public short type;
        /// <summary/>
        public int style;
        /// <summary/>
        public int alternate;
        /// <summary/>
        public int random;
        /// <summary/>
        public int direction;
    }

    /// <summary>
    /// 放置实体方块
    /// </summary>
    public class TileEntityPlacementEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int x;
        /// <summary/>
        public int y;
        /// <summary/>
        public short type;
    }

    /// <summary>
    /// 发送多图格方块的数据?
    /// </summary>
    public class SendTileSquareEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int x;
        /// <summary/>
        public int y;
        /// <summary/>
        public ushort sizeX;
        /// <summary/>
        public ushort sizeY;
        /// <summary/>
        public byte changeType;
    }

    /// <summary>
    /// 箱子放置破坏
    /// </summary>
    public class ChestUpdatesEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public byte updateType;
        /// <summary/>
        public int x;
        /// <summary/>
        public int y;
        /// <summary/>
        public int style;
        /// <summary/>
        public int id;
    }

    /// <summary>
    /// 点击开关
    /// </summary>
    public class HitSwitchEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int x;
        /// <summary/>
        public int y;
    }

    /// <summary>
    /// 物品框放置物品
    /// </summary>
    public class ItemFrameTryPlacingEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public short x;
        /// <summary/>
        public int y;
        /// <summary/>
        public int netid;
        /// <summary/>
        public int prefix;
        /// <summary/>
        public int stack;
    }

    /// <summary>
    /// 武器架放置物品
    /// </summary>
    public class WeaponsRackTryPlacingEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public short x;
        /// <summary/>
        public int y;
        /// <summary/>
        public int netid;
        /// <summary/>
        public int prefix;
        /// <summary/>
        public int stack;
    }

    /// <summary>
    /// 
    /// </summary>
    public class SyncItemEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int index;
        /// <summary/>
        public Vector2 position;
        /// <summary/>
        public Vector2 velocity;
        /// <summary/>
        public int stack;
        /// <summary/>
        public int prefix;
        /// <summary>应该是防止丢出物品立即捡起</summary>
        public int ownIgnore;
        /// <summary/>
        public int netid;
    }
}
