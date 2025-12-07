using ModTool.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using tContentPatch;

namespace PlayerAccount.Account
{
    internal class AccountData
    {
        internal static List<Dictionary<string, string>> datas { get; private set; } = null;

        internal static void Init()
        {
            if (UpdateData()) return;

            ContentPatch.PrintTry($"{nameof(PlayerAccount)}:读取账号数据失败,所有数据将会清空,在备份中可能有以前数据");

            datas = new List<Dictionary<string, string>>();
        }

        /// <summary>
        /// 更新账号数据, 失败返回<see langword="false"/>
        /// </summary>
        internal static bool UpdateData()
        {
            try
            {
                if (Directory.Exists(ThisMod.Dir) == false) return false;
                Directory.CreateDirectory(ThisMod.DirBackup);

                List<Dictionary<string, string>> datas = ReadData();
                if (datas == null) return false;

                AccountData.datas = datas;

                return true;
            }
            catch { }

            return false;
        }

        /// <summary>
        /// 读取账号数据, 读取失败从备份中读取, 读取失败返回<see langword="null"/>
        /// </summary>
        private static List<Dictionary<string, string>> ReadData()
        {
            try
            {
                return AccountFileHelp.ReadData(Path.Combine(ThisMod.Dir, $"{ThisMod.FileNameAccountData}.txt"));
            }
            catch (Exception ex)
            {
                ContentPatch.PrintTry($"{nameof(PlayerAccount)}:读取账号数据失败,尝试读取备份:{ex.Message}");
            }

            if (Directory.Exists(ThisMod.DirBackup) == false)
            {
                ContentPatch.PrintTry($"{nameof(PlayerAccount)}:备份目录不存在:[{ThisMod.DirBackup}]");
                return null;
            }

            try
            {
                DirectoryInfo dir = new DirectoryInfo(ThisMod.DirBackup);
                IOrderedEnumerable<FileInfo> files = dir.GetFiles()
                    .Where(i => i.Extension == ".txt")
                    .OrderByDescending(i => i.LastWriteTime);

                foreach (FileInfo f in files)
                {
                    try
                    {
                        ContentPatch.PrintTry($"{nameof(PlayerAccount)}:尝试读取备份文件:[{f.Name}]");
                        List<Dictionary<string, string>> datas = AccountFileHelp.ReadData(f.FullName);
                        ContentPatch.PrintTry($"{nameof(PlayerAccount)}:已读取备份文件:[{f.Name}]");
                        return datas;
                    }
                    catch { }
                }
            }
            catch { }

            return null;
        }

        /// <summary>
        /// 备份账号数据
        /// </summary>
        /// <exception cref="Exception"></exception>
        internal static void BackupData()
        {
            if (Directory.Exists(ThisMod.Dir) == false) throw new Exception("模组目录不存在");
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

            AccountFileHelp.SaveData(datas, save, true);
        }

        /// <summary>
        /// 备份并保存数据
        /// </summary>
        /// <exception cref="Exception"></exception>
        internal static void SaveData()
        {
            if (Directory.Exists(ThisMod.Dir) == false) throw new Exception("模组目录不存在");

            if (datas == null) datas = new List<Dictionary<string, string>>();
            CheckData(datas);

            try
            {
                BackupData();
            }
            catch (Exception ex)
            {
                throw new Exception($"备份数据失败:{ex.Message}", ex);
            }

            AccountFileHelp.SaveData(datas, Path.Combine(ThisMod.Dir, ThisMod.FileAccountData), true);
        }

        /// <summary>
        /// 清空异常数据
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        internal static void CheckData(List<Dictionary<string, string>> datas)
        {
            if (datas == null) throw new ArgumentNullException(nameof(datas));

            datas.RemoveAll(i =>
            i == null ||
            i.GetVal(AccountTag.Name, null) == null);
        }
    }
}
