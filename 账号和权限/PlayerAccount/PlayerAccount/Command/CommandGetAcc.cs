using CommandHelp;
using ModTool.Command;
using ModTool.Utils;
using PlayerAccount.Account;
using System;
using System.Collections.Generic;
using Terraria;

namespace PlayerAccount.Command
{
    /// <summary>
    /// 将完整账号名 或 匹配账号名 或 索引 转为账号, 不存在则报错
    /// </summary>
    public class CommandGetAcc : CommandValue<Dictionary<string, string>>
    {
        /// <inheritdoc/>
        public override string TipText { get; set; } = "\"完整账号名\"或匹配账号名或索引";
        /// <inheritdoc/>
        public override string TypeName => "\"完整账号名\"或匹配账号名或索引";
        /// <inheritdoc/>
        public override string Text => "\"完整账号名\"或匹配账号名或索引";

        /// <summary>
        /// 将完整账号名 或 匹配账号名 或 索引 转为账号, 不存在则报错
        /// </summary>
        public CommandGetAcc(bool isVariable = false) : base(isVariable) { }

        /// <inheritdoc/>
        protected override Dictionary<string, string> ArgConvertThrow(string arg)
        {
            bool isString = false;

            try
            {
                arg = CommandString.StringToString(arg);
                isString = true;
            }
            catch { }

            if (isString)//是完整字符串
            {
                Dictionary<string, string> acc = AccountHelp.GetNameAccount(arg);
                return acc ?? throw new Exception($"没有该账号:{arg}");
            }

            if (int.TryParse(arg, out int index))//是数字
            {
                if (DataAcc.instance.datas.IndexInRange(index) != true) throw new Exception($"[{index}]超出范围");

                return DataAcc.instance.datas[index];
            }

            //匹配名称
            List<(int, Dictionary<string, string>)> list = MatchAccName(arg);
            if (list.Count < 1) throw new Exception($"没有匹配的名称:{arg}");

            if (list.Count > 1)
            {
                string exMsg = "有多个匹配的名称:";
                foreach (var i in list) exMsg += $"\n索引:{i.Item1},名称:{i.Item2.GetVal(AccountTag.Name)}";
                throw new Exception(exMsg);
            }

            return list[0].Item2;
        }

        /// <inheritdoc/>
        protected override Dictionary<string, string> GetDefault() => null;

        /// <inheritdoc/>
        public override (string cmdParse, string cmd) ParseFormat(string command)
        {
            (string cmdParse, string cmd) = CommandString.ValParseFormat(false, command);

            if (cmdParse == null) return base.ParseFormat(command);//如果不是字符串格式就使用默认解析

            return (cmdParse, cmd);
        }

        /// <summary>
        /// 返回名字中包含<paramref name="match"/>的账号列表
        /// </summary>
        public static List<(int, Dictionary<string, string>)> MatchAccName(string match)
        {
            List<(int, Dictionary<string, string>)> list = new List<(int, Dictionary<string, string>)>();

            if (match == null) return list;

            for (int i = 0; i < DataAcc.instance.datas.Count; ++i)
            {
                Dictionary<string, string> acc = DataAcc.instance.datas[i];

                string name = acc.GetVal(AccountTag.Name, null);
                if (name == null)
                {
                    list.Add((i, acc));
                    continue;
                }

                if (name.Contains(match)) list.Add((i, acc));
            }

            return list;
        }
    }
}
