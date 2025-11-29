using PlayerGroup.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using tContentPatch.Utils;

namespace PlayerGroup
{
    /// <summary>
    /// 工具
    /// </summary>
    public static partial class GroupHelp
    {
        /// <summary>
        /// 保存分组数据
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static void SaveData(GroupData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            Directory.CreateDirectory(INoThisIsWhat.DirGroup);//目录不存在就创建

            string save = GetSaveName(data);
            if (save == null) throw new Exception("无法获取可保存的文件名");

            data.Save(Path.Combine(INoThisIsWhat.DirGroup, save));
        }

        /// <summary>
        /// 保存分组数据
        /// </summary>
        /// <param name="datas"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SaveData(List<GroupData> datas)
        {
            if (datas == null) throw new ArgumentNullException(nameof(datas));

            if (Directory.Exists(INoThisIsWhat.DirTempGroupSaveAll))
                Utils.Utils.DirectoryFileDelete(INoThisIsWhat.DirTempGroupSaveAll);//清空临时文件夹下的文件
            else
                Directory.CreateDirectory(INoThisIsWhat.DirTempGroupSaveAll);//目录不存在就创建

            //

            int count = 0;
            foreach (GroupData a in datas)
            {
                string file = $"{a.Name}.json";

                if (IsValidFileName(file) == false)//如果文件名不合法
                {
                    file = $"分组{count++}.json";
                }

                a.Save(Path.Combine(INoThisIsWhat.DirTempGroupSaveAll, file));//保存到临时文件夹
            }

            //

            Utils.Utils.DirectoryFileDelete(INoThisIsWhat.DirGroup);//清空分组文件夹下的文件

            Utils.Utils.DirectoryFileCopyTo(INoThisIsWhat.DirTempGroupSaveAll, INoThisIsWhat.DirGroup);//复制到分组文件夹
        }

        /// <summary>
        /// 更新分组数据, 更新成功为<see langword="null"/>
        /// </summary>
        /// <returns></returns>
        public static string UpdateData()
        {
            try
            {
                GroupSetting.datas = Read();
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsValidFileName(string fileName)
        {
            if (fileName == null || fileName.Length < 1) return false;

            //检查文件名是否包含无效字符
            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                return false; //文件名不合法
            }
            return true; //文件名合法
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetSaveName(GroupData data)
        {
            string save = $"{data?.Name}.json";

            if (IsValidFileName(save)) return save;

            List<string> files = Directory.GetFiles(INoThisIsWhat.DirGroup)//获取所有文件路径
                .Where(i => Path.GetExtension(i) == ".json")//筛选后缀是.json的文件
                .ToList();
            files = files.ConvertAll(i => Path.GetFileNameWithoutExtension(i));//只保留文件名

            //查找分组名相同的文件
            foreach (string file in files)
            {
                try
                {
                    save = $"{file}.json";
                    string path = Path.Combine(INoThisIsWhat.DirGroup, save);

                    if (File.Exists(path) == false) continue;

                    GroupData gd = MyJson1.Get2<GroupData>(path);//读取数据
                    if (gd?.Name == data.Name) return save;
                }
                catch { }
            }

            //找到没占用的文件名
            for (int i = 0; i < 999; ++i)
            {
                save = $"分组{i}";

                if (files.Contains(save) == false) return $"{save}.json";
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<GroupData> Read()//读取分组数据
        {
            string exMsg = "读取分组数据";

            try
            {
                string[] files = Directory.GetFiles(INoThisIsWhat.DirGroup);//获取目录下所有文件
                files = files.Where(i => Path.GetExtension(i) == ".json").ToArray();//筛选后缀是.json的文件

                List<GroupData> datas = new List<GroupData>();

                foreach (string f in files)
                {
                    exMsg = $"读取分组数据文件:[{Path.GetFileName(f)}]";

                    GroupData data = MyJson1.Get2<GroupData>(f);//读取数据
                    if (data == null) continue;
                    if (data.Name == null) continue;
                    if (data.PlaysName == null) data.PlaysName = new List<string>();
                    if (data.Tag == null) data.Tag = new Dictionary<string, string>();

                    GroupData old = datas.FirstOrDefault(i => i.Name == data.Name);//名称相同的分组

                    if (old == null)//如果分组不存在
                    {
                        datas.Add(data);
                    }
                    else
                    {
                        old.PlaysName.AddRange(data.PlaysName);
                    }
                }

                return datas;
            }
            catch (Exception ex)
            {
                throw new Exception($"读取分组数据失败:状态:[{exMsg}]", ex);
            }
        }
    }
}
