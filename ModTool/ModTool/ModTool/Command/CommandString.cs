using CommandHelp;

namespace ModTool.Command
{
    /// <summary>
    /// "字符串"
    /// </summary>
    public class CommandString: CommandValue<string>
    {
        /// <inheritdoc/>
        public override string Text => "<\"string\">";
        /// <inheritdoc/>
        protected override string GetDefault() => null;

        /// <summary/>
        public CommandString(bool isVariable = false) : base(isVariable) { }

        /// <inheritdoc/>
        protected override string ArgConvertThrow(string arg) => StringToString(arg);

        /// <inheritdoc/>
        public override (string cmdParse, string cmd) ParseFormat(string command) => ValParseFormat(IsVariable, command);

        /// <summary>
        /// 解析格式["字符串"]
        /// </summary>
        public static (string cmdParse, string cmd) ValParseFormat(bool IsVariable, string command)
        {
            if (command == null) return (IsVariable ? string.Empty : null, command);

            string cmd = command.TrimStart();

            if (cmd.Length < 1) return (IsVariable ? string.Empty : null, command);
            if (cmd.Length < 2) return (null, command);
            if (cmd[0] != '\"') return (null, command);

            int index1 = 0;
            int index2 = cmd.IndexOf('\"', index1 + 1);

            if (index2 == -1) return (null, command);

            string text = cmd.Substring(index1, index2 + 1);
            cmd = cmd.Remove(index1, index2 + 1);

            return (text, cmd);
        }

        /// <summary>
        /// ["字符串"]=>[字符串]
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        public static string StringToString(string s)
        {
            if (s.Length < 2) throw new System.Exception($"[{s}]字符串格式错误");
            if (s[0] != '\"') throw new System.Exception($"[{s}]字符串格式错误:开头不为\"");
            if (s[s.Length - 1] != '\"') throw new System.Exception($"[{s}]字符串格式错误:结尾不为\"");
            if (s.Length < 3) return string.Empty;
            return s.Substring(1, s.Length - 2);
        }
    }
}
