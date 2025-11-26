using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using tContentPatch;

namespace PlayerGroup.Utils
{
    /// <summary>
    /// 嘿哟喂
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// 保存数据, 有缩进
        /// </summary>
        /// <param name="val"></param>
        /// <param name="FilePath1"></param>
        /// <exception cref="Exception"></exception>
        public static void Save(this object val, string FilePath1)
        {
            try
            {
                string directory = Path.GetDirectoryName(FilePath1);

                if (!Directory.Exists(directory)) throw new Exception($"目录不存在:[{directory}]");

                File.WriteAllText(FilePath1, JsonConvert.SerializeObject(val, Formatting.Indented), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MethodBase mb = MethodBase.GetCurrentMethod();
                throw new Exception($"{mb?.DeclaringType.Name}.{mb?.Name}:{ex.Message}");
            }
        }

        /// <summary>
        /// 清空目录下的所有文件
        /// </summary>
        /// <param name="path"></param>
        public static void DirectoryFileDelete(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo f in di.GetFiles()) f.Delete();
        }

        /// <summary>
        /// 复制目录下所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pathTo"></param>
        public static void DirectoryFileCopyTo(string path, string pathTo)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo f in di.GetFiles()) f.CopyTo(Path.Combine(pathTo, f.Name), true);
        }

        /// <summary>
        /// 何意味
        /// </summary>
        /// <param name="action"></param>
        /// <param name="ok"></param>
        /// <param name="no"></param>
        /// <param name="print"></param>
        public static void ActionState(Action action, string ok, string no, Action<string> print = null)
        {
            try
            {
                action();
            }
            catch
            {
                PrintTry(no, print);
                return;
            }
            PrintTry(ok, print);
        }

        /// <summary>
        /// 输出, 如果<paramref name="print"/>为<see langword="null"/>那么使用<see cref="ContentPatch.PrintTry(string)"/>输出
        /// </summary>
        /// <param name="s"></param>
        /// <param name="print"></param>
        public static void PrintTry(string s, Action<string> print = null)
        {
            try
            {
                (print ?? ContentPatch.PrintTry)(s);
            }
            catch { }
        }
    }
}
