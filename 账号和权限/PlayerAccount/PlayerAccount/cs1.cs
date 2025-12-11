//using Microsoft.Xna.Framework;
//using PlayerAccount.Account;
//using System;
//using System.Collections.Generic;
//using tContentPatch;

//namespace PlayerAccount
//{
//    internal class cs1 : PatchMain
//    {
//        private int co = 0;

//        public override void UpdatePrefix(GameTime gameTime)
//        {
//            if (DataAcc.instance.datas == null) return;
//            if (DataBanIP.instance.datas == null) return;
//            if (DataBanName.instance.datas == null) return;
//            if (DataBanUUID.instance.datas == null) return;

//            if (co++ % 30 != 0) return;

//            Console.Clear();

//            foreach (Dictionary<string, string> acc in DataAcc.instance.datas)
//            {
//                Console.WriteLine();
//                foreach (KeyValuePair<string, string> i in acc)
//                {
//                    Console.WriteLine($"{{{i.Key},{i.Value}}}");
//                }
//            }

//            foreach ((string IP, string Port) i in DataBanIP.instance.datas)
//            {
//                Console.WriteLine($"{i.IP}:{i.Port}");
//            }

//            foreach (string i in DataBanName.instance.datas)
//            {
//                Console.WriteLine(i);
//            }

//            foreach (string i in DataBanUUID.instance.datas)
//            {
//                Console.WriteLine(i);
//            }
//        }
//    }
//}
