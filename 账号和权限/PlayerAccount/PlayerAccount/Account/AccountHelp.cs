using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using tContentPatch;

namespace PlayerAccount.Account
{
    /// <summary>
    /// 工具
    /// </summary>
    public static class AccountHelp
    {
        internal static List<AccountData> datas { get; private set; } = null;

        internal static void Init()
        {
            UpdateData();
            if (datas == null) datas = new List<AccountData>();
        }

        /// <summary>
        /// 更新账号数据
        /// </summary>
        /// <returns></returns>
        public static bool UpdateData()
        {
            List<AccountData> data = ReadData();
            if (data == null) return false;
            datas = data;
            return true;
        }

        /// <summary>
        /// 读取账号数据
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static List<AccountData> ReadData(string file)
        {
            if (File.Exists(file) == false) throw new FileNotFoundException("文件不存在", file);

            List<AccountData> list = tContentPatch.Utils.MyJson1.Get2<List<AccountData>>(file);

            CheckData(list);

            return list;
        }

        /// <summary>
        /// 读取账号数据, 读取失败从备份中读取, 读取失败返回<see langword="null"/>
        /// </summary>
        /// <returns></returns>
        public static List<AccountData> ReadData()
        {
            try
            {
                return ReadData(Path.Combine(ThisMod.Dir, $"{ThisMod.FileNameAccountData}.txt"));
            }
            catch (Exception ex)
            {
                ContentPatch.PrintTry($"{nameof(AccountHelp)}:读取账号数据失败,尝试读取备份:{ex.Message}");
            }

            if (Directory.Exists(ThisMod.DirBackup) == false)
            {
                ContentPatch.PrintTry($"{nameof(AccountHelp)}:备份目录不存在:[{ThisMod.DirBackup}]");
                return null;
            }

            DirectoryInfo dir = new DirectoryInfo(ThisMod.DirBackup);
            IOrderedEnumerable<FileInfo> files = dir.GetFiles()
                .Where(i => i.Extension == ".txt")
                .OrderByDescending(i => i.LastWriteTime);

            foreach (FileInfo f in files)
            {
                try
                {
                    List<AccountData> list = ReadData(f.FullName);
                    ContentPatch.PrintTry($"{nameof(AccountHelp)}:已读取备份文件:[{f.Name}]");
                    return list;
                }
                catch { }
            }

            return null;
        }

        /// <summary>
        /// 清空异常数据
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void CheckData(List<AccountData> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            data.RemoveAll(i => i == null || i.name == null || i.password == null);
        }

        /// <summary>
        /// 保存账号数据
        /// </summary>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static void SaveData()
        {
            //这目录就是模组文件夹, 如果这个目录不存在说明模组文件夹也不在
            if (Directory.Exists(ThisMod.Dir) == false) throw new DirectoryNotFoundException($"目录不存在:[{ThisMod.Dir}]");

            string file = Path.Combine(ThisMod.Dir, $"{ThisMod.FileNameAccountData}.txt");

            PlayerGroup.Utils.Utils.Save(datas, file);
        }

        /// <summary>
        /// 备份账号数据
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void BackupData()
        {
            Directory.CreateDirectory(ThisMod.DirBackup);//目录不存在就创建

            List<string> files = Directory.GetFiles(ThisMod.DirBackup)//获取所有文件路径
                .Where(i => Path.GetExtension(i) == ".txt")//筛选后缀
                .ToList();
            files = files.ConvertAll(i => Path.GetFileNameWithoutExtension(i));//只保留文件名

            string save = null;

            //找到没占用的文件名
            for (int i = 0; ; ++i)
            {
                string name = $"{ThisMod.FileNameAccountData}{i}";

                if (files.Contains(name)) continue;

                save = name;
                break;
            }

            //何意味
            if (save == null) throw new Exception("未知异常");

            save = Path.Combine(ThisMod.DirBackup, $"{save}.txt");

            PlayerGroup.Utils.Utils.Save(datas, save);
        }
    }
}
