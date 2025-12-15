namespace ModTool.ServerHelp
{
    /// <summary>
    /// 杂项
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// 启用服务端角色
        /// </summary>
        public static void ServerSideCharacter(bool enable)
        {
            PatchGame.PatchNetMessage_SendData.PatchWorldData.ServerSideCharacter = enable;
        }
    }
}
