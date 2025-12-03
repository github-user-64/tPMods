using System;
using Terraria;

namespace ServerHelp
{
    /// <summary>
    /// 附加数据
    /// </summary>
    public abstract class AdditionalData<T, T2>
    {
        /// <summary>数据</summary>
        protected T[] data = null;
        /// <summary>有值</summary>
        protected bool[] hasData = null;

        /// <summary/>
        public AdditionalData(T2[] array)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));

            data = new T[array.Length];
            hasData = new bool[data.Length];
        }

        /// <summary>
        /// 是否有数据
        /// </summary>
        public virtual bool HasData(int index)
        {
            if (IndexInRange(index) == false) return false;

            return hasData[index];
        }

        /// <summary>
        /// 获取数据, 不存在返回<paramref name="def"/>
        /// </summary>
        public virtual T GetData(int index, T def = default)
        {
            if (HasData(index) == false) return def;

            return data[index];
        }

        /// <summary>
        /// 设置数据, 成功返回<see langword="true"/>
        /// </summary>
        public virtual bool SetData(int index, T val)
        {
            if (IndexInRange(index) == false) return false;

            data[index] = val;
            hasData[index] = true;

            return true;
        }

        /// <summary>
        /// 清除数据
        /// </summary>
        protected virtual void ClearData()
        {
            ForData(i =>
            {
                data[i] = default;
                hasData[i] = false;
            });
        }

        /// <summary>
        /// 清除数据项
        /// </summary>
        protected virtual void ClearDataItem(int index)
        {
            if (IndexInRange(index) == false) return;

            data[index] = default;
            hasData[index] = false;
        }

        /// <summary>
        /// 更新数据, <paramref name="clearOld"/>为<see langword="true"/>会先清除再更新
        /// </summary>
        public virtual void UpdateData(bool clearOld = false)
        {
            ForData(i => UpdateDataItem(i, clearOld));
        }

        /// <summary>
        /// 更新数据项, 成功返回<see langword="true"/>, <paramref name="clearOld"/>为<see langword="true"/>会先清除再更新
        /// </summary>
        public virtual bool UpdateDataItem(int index, bool clearOld = false)
        {
            try
            {
                if (IndexInRange(index) == false) return false;

                if (clearOld) ClearDataItem(index);

                return SetData(index, ConverterThrow(index));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 批量处理数据
        /// </summary>
        public virtual void ForData(Action<int> action)
        {
            if (action == null) return;

            for (int i = 0; i < data.Length; ++i) action(i);
        }

        /// <summary>
        /// <paramref name="index"/>在范围内返回<see langword="true"/>
        /// </summary>
        public virtual bool IndexInRange(int index)
        {
            return data?.IndexInRange(index) == true;
        }

        /// <summary>
        /// 将<paramref name="index"/>转化为<typeparamref name="T"/>
        /// </summary>
        public virtual T ConverterThrow(int index) => throw new Exception("转化失败");
    }
}
