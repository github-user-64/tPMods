using CommandHelp;
using System.Collections.Generic;
using tContentPatch;

namespace BedWars
{
    internal class ModCommand : Mod
    {
        public override List<CommandObject> GetCommands()
        {
            List<CommandObject> cos = new List<CommandObject>();

            cos.Add(new CommandReset(ContentPatch.PrintTry));

            return cos;
        }
    }
}
