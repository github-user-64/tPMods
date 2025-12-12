using ModTool.Utils;
using System;

namespace PlayerAccount.Account
{
    internal abstract class DataABackup<T> : ModSettingBackup
    {
        public override bool HasUI => false;
        public override Type DataType => typeof(T);
        public T datas { get; protected set; } = default;

        public override object GetSaveData() => datas;

        /// <summary>
        /// 更新数据, 失败返回<see langword="false"/>
        /// </summary>
        public bool UpdateData()
        {
            if (Read() is T data == false) return false;
            CheckData(data);

            datas = data;

            return true;
        }

        /// <summary>
        /// 清空异常数据
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public abstract void CheckData(T data);

        public void NeedSaveData()
        {
            NeedSave = true;
        }
    }
}
