using System.IO;

namespace ModTool.PatchGame.PNetMessage_SendData
{
    internal class PShopOverride
    {
        /// <summary>
        /// 妈的修了半天结果是原版这里的参数转化有问题 >:(
        /// <para/>被<see href="red"/>摆了一道
        /// <para/><see cref="ReLogic"/>: 一想到有人用这个发数据结果发现数据有问题我就想笑
        /// </summary>
        public static void Foo(BinaryWriter writer, int number, float number2, float number3, float number4, int number5, int number6)
        {
            writer.Write((byte)number);
            writer.Write((short)number2);
            writer.Write((short)(number3 < 0 ? 0f : number3));//原本传进去的是float
            writer.Write((byte)number4);
            writer.Write(number5);
            writer.Write((byte)number6);
        }
    }
}
