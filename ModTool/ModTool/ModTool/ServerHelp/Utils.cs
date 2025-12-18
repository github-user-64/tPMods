namespace ModTool.ServerHelp
{
    /// <summary>
    /// 杂项
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// 启用服务端角色, 如需启用最好在玩家加入前就启用
        /// </summary>
        public static void ServerSideCharacter(bool enable)
        {
            PatchGame.PatchNetMessage_SendData.PatchWorldData.ServerSideCharacter = enable;
        }
    }
}
