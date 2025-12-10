using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using tContentPatch;
using tContentPatch.ModLoad;
using tContentPatch.Utils;

namespace ModTool.Utils
{
    /// <summary>
    /// 有备份的<see cref="ModSetting"/>
    /// </summary>
    public abstract class ModSettingBackup : ModSetting
    {
        /// <summary>
        /// 备份目录, 在模组目录下, 不设置无法备份
        /// </summary>
        public virtual string BackupDir => null;
        /// <summary>
        /// 备份文件名, 保存到<see cref="BackupDir"/>, 不设置无法备份
        /// </summary>
        public virtual string BackupFileName => null;
        /// <summary>
        /// 保存文件是否缩进
        /// </summary>
        public virtual bool SaveFileIndented => true;
        /// <summary>
        /// 最大备份文件数量
        /// </summary>
        public virtual int BackupMaxSaveCount => 32;

        /// <summary>
        /// 保存, 不用设置<see cref="ModSetting.NeedSave"/>
        /// </summary>
        public override void Save()
        {
            Assembly assembly = GetType().Assembly;
            ModObject mo = ContentPatch.GetModObjects()?.FirstOrDefault(i => i.assembly == assembly);

            ModFile.SaveFileTry(FilePath, delegate (string file)
            {
                MyJson1.Save(GetSaveData(), file, SaveFileIndented);
                return true;
            }, mo);

            NeedSave = false;
        }

        /// <summary>
        /// 备份, 成功返回<see langword="true"/>
        /// <para/>在备份目录下按"(0到<see cref="BackupMaxSaveCount"/>)<see cref="BackupFileName"/>"格式保存
        /// <para/>若全被占用则覆盖修改时间最早的文件
        /// </summary>
        public virtual bool BackupSave()
        {
            if (BackupDir == null) return false;
            if (BackupFileName == null) return false;

            Assembly assembly = GetType().Assembly;
            ModObject mo = ContentPatch.GetModObjects()?.FirstOrDefault(i => i.assembly == assembly);

            string dir = GetBackupPath();//保存位置
            if (dir == null) return false;

            Directory.CreateDirectory(dir);//目录不存在就创建

            //

            List<string> names = new DirectoryInfo(dir)
                .GetFiles()
                .OrderByDescending(i => i.LastWriteTime)
                .ToList()
                .ConvertAll(i => i.Name);

            string save = null;

            //找到没占用的文件名
            for (int i = 0; i < BackupMaxSaveCount; ++i)
            {
                string n = $"{i}{BackupFileName}";

                if (names.Contains(n)) continue;

                save = n;
                break;
            }

            if (save == null)
            {
                if (names.Count < 1) save = $"0{BackupFileName}";
                else save = names.Last();
            }

            save = Path.Combine(dir, save);

            try
            {
                MyJson1.Save(GetSaveData(), save, SaveFileIndented);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取备份, 失败返回<see langword="null"/>
        /// <para/>在备份目录下从最新的文件开始读取, 读取到的值为<see langword="null"/>则跳过
        /// </summary>
        public virtual object BackupRead()
        {
            if (BackupDir == null) return null;
            if (BackupFileName == null) return null;

            string dir = GetBackupPath();
            if (dir == null) return null;

            if (Directory.Exists(dir) == false) return null;

            //

            IOrderedEnumerable<FileInfo> files = new DirectoryInfo(dir)
                .GetFiles()
                .OrderByDescending(i => i.LastWriteTime);

            foreach (FileInfo f in files)
            {
                try
                {
                    object data = MyJson1.Get2(f.FullName, DataType);
                    if (data == null) continue;
                    return data;
                }
                catch { }
            }

            return null;
        }

        /// <summary>
        /// 获取备份目录, 失败返回<see langword="null"/>
        /// </summary>
        public string GetBackupPath()
        {
            if (BackupDir == null) return null;

            Assembly assembly = GetType().Assembly;
            ModObject mo = ContentPatch.GetModObjects()?.FirstOrDefault(i => i.assembly == assembly);

            string modPath = mo?.modPath;
            if (modPath == null) return null;

            return Path.Combine(modPath, BackupDir);
        }
    }
}
