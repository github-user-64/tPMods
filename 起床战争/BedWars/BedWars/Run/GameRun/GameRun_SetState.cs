using System;
using Terraria;

namespace BedWars.Run
{
    public partial class GameRun
    {
        protected void EndState()
        {
            try
            {
                NowState?.OnEnd();
                NowState = null;
            }
            catch (Exception ex)
            {
                string s = $"起床战争:退出状态{NowStateType}时出现异常:{ex.Message}";
                tContentPatch.Utils.Log.Add(s);
                tContentPatch.ContentPatch.PrintTry(s);
            }
        }

        protected void StartState(int key, object arg = null)
        {
            if (States?.IndexInRange(key) != true) return;

            try
            {
                NowStateType = key;
                NowState = States[key];
                NowState?.OnStart(arg);
            }
            catch (Exception ex)
            {
                string s = $"起床战争:进入状态{NowStateType}时出现异常:{ex.Message}";
                tContentPatch.Utils.Log.Add(s);
                tContentPatch.ContentPatch.PrintTry(s);
            }
        }

        public void SetState(int key, object arg = null)
        {
            EndState();

            StartState(key, arg);
        }

        /// <summary>
        /// 下次更新时才会修改状态
        /// </summary>
        public void SetStateUpdate(int key, object arg = null)
        {
            HasUpdateState = () =>
            {
                SetState(key, arg);
            };
        }
    }
}
