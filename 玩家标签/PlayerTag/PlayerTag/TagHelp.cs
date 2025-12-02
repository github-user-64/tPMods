using PlayerTag.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using tContentPatch.Utils;

namespace PlayerTag
{
    /// <summary>
    /// 工具
    /// </summary>
    public static partial class TagHelp
    {
        /// <summary>
        /// 保存标签数据
        /// </summary>
        public static void SaveData()
        {
            Directory.CreateDirectory(ThisMod.DirTag);//目录不存在就创建
            Directory.CreateDirectory(ThisMod.DirTagTemp);//目录不存在就创建

            SaveData(TagSetting.datas, ThisMod.FileTag, ThisMod.FileTagTemp);
        }

        /// <summary>
        /// 保存标签数据
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="file">保存的文件位置</param>
        /// <param name="temp">临时保存的文件位置</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static void SaveData(List<TagData> datas, string file, string temp)
        {
            if (datas == null) throw new ArgumentNullException(nameof(datas));
            if (file == null) throw new ArgumentNullException(nameof(file));
            if (temp == null) throw new ArgumentNullException(nameof(temp));

            string saveDir = Path.GetDirectoryName(file);
            string tempDir = Path.GetDirectoryName(temp);

            if (Directory.Exists(saveDir) == false) throw new DirectoryNotFoundException($"目录不存在[{saveDir}]");
            if (Directory.Exists(tempDir) == false) throw new DirectoryNotFoundException($"目录不存在[{tempDir}]");

            //

            datas = datas.ToList();//复制
            CheckTagData(datas);//检查删除active为false的

            //保存到临时文件夹
            datas.Save(temp);

            //复制到文件夹
            FileInfo fi = new FileInfo(temp);
            fi.CopyTo(file, true);
        }

        /// <summary>
        /// 更新标签数据, 更新成功为<see langword="null"/>
        /// </summary>
        public static string UpdateData()
        {
            try
            {
                List<TagData> data = Read(ThisMod.FileTag);
                TagSetting.UpdateData(data);

                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }

        /// <summary>
        /// 读取标签数据
        /// </summary>
        public static List<TagData> Read(string file)
        {
            return MyJson1.Get2<List<TagData>>(file);//读取数据
        }

        /// <summary>
        /// 检查和恢复数据并删除<see cref="TagData.active"/>为<see langword="false"/>项
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static void CheckTagData(this List<TagData> datas)
        {
            TagSetting.CheckTagData(datas);
        }
    }
}
