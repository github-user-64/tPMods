using CommandHelp;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;

namespace ModTool.test
{
    /// <summary>
    /// 枚举实体
    /// </summary>
    public class CommandeEnumEntity : CommandeEnum
    {
        /// <inheritdoc/>
        public override string Text => "<@nn, @ni, @np, @s>//最近的npc, 最近的物品, 最近的射弹, 自己";
        /// <inheritdoc/>
        public override string[] TipTexts => new string[]
        {
            "距离执行者最近的npc",
            "距离执行者最近的物品",
            "距离执行者最近的射弹",
            "执行者",
        };
        /// <summary/>
        public Player player = null;
        /// <summary/>
        public Action<string> print = null;

        /// <summary/>
        public CommandeEnumEntity(Player player, Action<string> print, bool IsVariable) :
            base(IsVariable, "@nn", "@ni", "@np", "@s")
        {
            this.player = player;
            this.print = print;
        }

        /// <inheritdoc/>
        public override object Run(ref int index, List<CommandObject> commandList)
        {
            int type = (int)base.Run(ref index, commandList);

            Entity entity = null;

            if (type == 0)
            {
                entity = GetEntity(Main.npc);
                if (entity == null) print?.Invoke("找不到npc");
            }
            else if (type == 1)
            {
                entity = GetEntity(Main.item);
                if (entity == null) print?.Invoke("找不到物品");
            }
            else if (type == 2)
            {
                entity = GetEntity(Main.projectile);
                if (entity == null) print?.Invoke("找不到射弹");
            }
            else if (type == 3)
            {
                entity = player;
            }
            else
            {
                print?.Invoke($"未知类型:[{type}]");
            }

            return entity;
        }

        /// <summary>
        /// 获取距离玩家最近实体
        /// </summary>
        public T GetEntity<T>(T[] list, Func<T, bool> fun = null) where T : Entity
        {
            Vector2 pos = player.Center;
            T entity = null;
            float oldD = 0;

            foreach (T i in list)
            {
                if (i.active == false) continue;
                if (fun != null && fun(i) == false) continue;

                Vector2 p = i.Center;
                float d = pos.Distance(p);

                if (entity == null)
                {
                    entity = i;
                    oldD = d;
                }
                else if (d < oldD)
                {
                    entity = i;
                    oldD = d;
                }
            }

            return entity;
        }
    }
}
