using System.Collections.Generic;
using Terraria.ID;

namespace BedWars.Utils
{
    public static class Utils
    {
        /// <summary>
        /// 获取随机物品id, 排除<paramref name="exclude"/>, 如果不存在返回0
        /// </summary>
        public static int GetRandItemID(List<int> exclude = null)
        {
            int index = ModTool.Utils.Utils.GetRand(1, ItemID.Count);

            for (int i = index; ;)
            {
                if (
                    ItemID.Sets.Deprecated[i] ||//已弃用
                    exclude?.Contains(i) == true//排除
                    )
                {
                    ++i;
                    if (i < ItemID.Count == false) i = 1;//到结尾就从头开始
                    if (i == index) return 0;//如果绕一圈回来了
                    continue;
                }

                return i;
            }
        }
    }
}
