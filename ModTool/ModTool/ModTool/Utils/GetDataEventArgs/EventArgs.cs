using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

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
    /// 同步物品
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

    #region 方块类
    /// <summary>
    /// 自定义, 方块类, 修改图格数据这一类的
    /// </summary>
    public abstract class ClassTileEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int x;
        /// <summary/>
        public int y;
    }

    /// <summary>
    /// 操作方块
    /// </summary>
    public class TileManipulationEventArgs : ClassTileEventArgs
    {
        /// <summary/>
        public byte manipulationType;
        /// <summary/>
        public short tileType;
        /// <summary/>
        public int placeStyle;
    }

    /// <summary>
    /// 放置对象
    /// </summary>
    public class PlaceObjectEventArgs : ClassTileEventArgs
    {
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
    public class TileEntityPlacementEventArgs : ClassTileEventArgs
    {
        /// <summary/>
        public short type;
    }

    /// <summary>
    /// 发送多图格方块的数据?
    /// </summary>
    public class SendTileSquareEventArgs : ClassTileEventArgs
    {
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
    public class ChestUpdatesEventArgs : ClassTileEventArgs
    {
        /// <summary/>
        public byte updateType;
        /// <summary/>
        public int style;
        /// <summary/>
        public int id;
    }

    /// <summary>
    /// 点击开关
    /// </summary>
    public class HitSwitchEventArgs : ClassTileEventArgs
    {

    }

    /// <summary>
    /// 自定义的, 放置物品
    /// </summary>
    public abstract class ItemTryPlacingEventArgs : ClassTileEventArgs
    {
        /// <summary/>
        public int netid;
        /// <summary/>
        public int prefix;
        /// <summary/>
        public int stack;
    }

    /// <summary>
    /// 物品框放置物品
    /// </summary>
    public class ItemFrameTryPlacingEventArgs : ItemTryPlacingEventArgs
    {

    }

    /// <summary>
    /// 武器架放置物品
    /// </summary>
    public class WeaponsRackTryPlacingEventArgs : ItemTryPlacingEventArgs
    {

    }

    /// <summary>
    /// 食物盘子放置物品
    /// </summary>
    public class FoodPlatterTryPlacingEventArgs : ItemTryPlacingEventArgs
    {

    }

    /// <summary>
    /// 液体更新
    /// </summary>
    public class LiquidUpdateEventArgs : ClassTileEventArgs
    {
        /// <summary/>
        public byte liquid;
        /// <summary/>
        public byte liquidType;
    }

    /// <summary>
    /// 自定义的, 油漆
    /// </summary>
    public abstract class PaintEventArgs : ClassTileEventArgs
    {
        /// <summary/>
        public byte color;
        /// <summary>
        /// 0放置1去除?
        /// </summary>
        public byte coat;
    }

    /// <summary>
    /// 油漆方块
    /// </summary>
    public class PaintTileEventArgs : PaintEventArgs
    {

    }

    /// <summary>
    /// 油漆墙
    /// </summary>
    public class PaintWallEventArgs : PaintEventArgs
    {

    }

    /// <summary>
    /// 编辑告示牌
    /// </summary>
    public class EditSignEventArgs : PaintEventArgs
    {
        /// <summary/>
        public int signIndex;
        /// <summary/>
        public string text;
        /// <summary>
        /// 应该是用来让发送编辑的客户端处理关闭编辑界面之类的东西
        /// </summary>
        public int whoAmI;
        /// <summary>
        /// 不知道是啥, 但和<see cref="whoAmI"/>有关
        /// </summary>
        public BitsByte bitsByte;
    }

    /// <summary>
    /// 上锁开锁
    /// </summary>
    public class LockAndUnlockEventArgs : PaintEventArgs
    {
        /// <summary>
        /// 1:开箱子,2:开门,3:锁箱子
        /// </summary>
        public int type;
    }
    #endregion

    /// <summary>
    /// 请求打开箱子
    /// </summary>
    public class RequestChestOpenEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int x;
        /// <summary/>
        public int y;
    }

    /// <summary>
    /// 快速堆叠到箱子
    /// </summary>
    public class QuickStackChestsEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int slot;
    }

    /// <summary>
    /// 玩家buffs
    /// </summary>
    public class PlayerBuffsEventArgs : GetDataEventArgs
    {

    }

    /// <summary>
    /// 抓住动物
    /// </summary>
    public class BugCatchingEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int npcIndex;
    }

    /// <summary>
    /// 释放动物
    /// </summary>
    public class BugReleasingEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int x;
        /// <summary/>
        public int y;
        /// <summary/>
        public int type;
        /// <summary/>
        public byte style;
    }

    /// <summary>
    /// 生成boss, 使用许可证, 开始事件
    /// </summary>
    public class SpawnBossUseLicenseStartEventEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int type;
    }

    /// <summary>
    /// 请求服务器传送
    /// </summary>
    public class RequestTeleportationByServerEventArgs : GetDataEventArgs
    {
        /// <summary>
        /// 0:随机,1:魔法海螺,2:恶魔海螺,3:贝壳电话世界重生点
        /// </summary>
        public byte type;
    }

    /// <summary>
    /// 传送实体, 好乱实在看不懂
    /// </summary>
    public class TeleportEntityEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public BitsByte bitsByte;
        /// <summary/>
        public Vector2 vector;
        /// <summary/>
        public int style;
        /// <summary>
        /// 0:传玩家,1:传npc,2:传队伍
        /// </summary>
        public int type;
        /// <summary/>
        public int extraInfo;
    }
    /// <summary>
    /// 伤害npc
    /// </summary>
    public class DamageNPCEventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int npcIndex;
        /// <summary/>
        public int damage;
        /// <summary/>
        public float knokBack;
        /// <summary/>
        public int hitDirection;
        /// <summary/>
        public byte crit;
    }
    /// <summary>
    /// 伤害玩家
    /// </summary>
    public class PlayerHurtV2EventArgs : GetDataEventArgs
    {
        /// <summary/>
        public int playerHurt;
        /// <summary/>
        public PlayerDeathReason playerDeathReason;
        /// <summary/>
        public int damage;
        /// <summary/>
        public int hitDirection;
        /// <summary/>
        public bool crit;
        /// <summary/>
        public bool pvp;
        /// <summary/>
        public int cooldownCounter;
    }
}
