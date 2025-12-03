namespace ModTool.EntityTag
{
    /// <summary>
    /// 实体
    /// </summary>
    public static class Entitys
    {
        /// <summary>玩家</summary>
        public static PlayerTag player { get; private set; } = null;
        /// <summary>射弹</summary>
        public static ProjectileTag projectile { get; private set; } = null;
        /// <summary>物品</summary>
        public static ItemTag item { get; private set; } = null;
        /// <summary>NPC</summary>
        public static NPCTag npc { get; private set; } = null;

        internal static void Init()
        {
            player = new PlayerTag();
            projectile = new ProjectileTag();
            item = new ItemTag();
            npc = new NPCTag();
        }
    }
}
