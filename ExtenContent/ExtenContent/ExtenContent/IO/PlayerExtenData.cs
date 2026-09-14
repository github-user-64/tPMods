using System.Collections.Generic;
using System.IO;
using tContentPatch.Utils;

namespace ExtenContent.IO
{
    /// <summary>
    /// 玩家扩展数据
    /// </summary>
    public class PlayerExtenData : Dictionary<string, object>
    {
        /// <summary>
        /// 玩家扩展数据后缀名
        /// </summary>
        public const string ExtensionName = "plre";

        /// <summary>
        /// 保存玩家扩展数据(没有扩展名的文件名, 保存的数据)
        /// </summary>
        /// <param name="fileName">没有扩展名的文件名</param>
        /// <param name="data">保存的数据</param>
        /// <returns>保存成功返回<see langword="true"/></returns>
        public static bool Save(string fileName, PlayerExtenData data)
        {
            return ModFile.SaveFileTry(Path.Combine("Players", $"{fileName}.{ExtensionName}"), path =>
            {
                MyJson1.Save(data, path, true);

                return true;
            });
        }

        /// <summary>
        /// 加载玩家扩展数据(没有扩展名的文件名)
        /// </summary>
        /// <param name="fileName">没有扩展名的文件名</param>
        /// <returns>失败返回<see langword="null"/></returns>
        public static PlayerExtenData Load(string fileName)
        {
            object data = null;

            ModFile.ReadFileTry(Path.Combine("Players", $"{fileName}.{ExtensionName}"), path =>
            {
                data = MyJson1.Get2(path, typeof(PlayerExtenData));

                return true;
            });

            return data as PlayerExtenData;
        }
    }
}
