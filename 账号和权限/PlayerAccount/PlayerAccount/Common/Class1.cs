using System.Collections.Generic;

namespace PlayerAccount.Common
{
    internal class Class1 : ModTool.AdditionalData.PlayerAdditionalData<Dictionary<string, string>>
    {
        public override Dictionary<string, string> ConverterThrow(int index)
        {
            return base.ConverterThrow(index);
        }
    }
}
