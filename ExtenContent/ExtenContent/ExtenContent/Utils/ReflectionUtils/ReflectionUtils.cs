using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExtenContent.Utils
{
    internal static partial class ReflectionUtils
    {
        public class R
        {
            protected readonly Type type;
            public R(Type type)
            {
                this.type = type;
            }

            protected readonly Dictionary<string, MethodInfo> methods = new Dictionary<string, MethodInfo>();
            protected readonly Dictionary<string, FieldInfo> fields = new Dictionary<string, FieldInfo>();

            public T Method<T>(string name, object instance = null, params object[] args)
            {
                if (methods.TryGetValue(name, out MethodInfo mi) != true)
                {
                    BindingFlags bf = BindingFlags.NonPublic | (instance == null ? BindingFlags.Static : BindingFlags.Instance);

                    mi = type.GetMethod(name, bf);
                    methods[name] = mi ?? throw new Exception($"{nameof(R)}:找不到{name}");
                }

                return (T)mi.Invoke(instance, args);
            }

            public T Field<T>(string name, object instance)
            {
                if (fields.TryGetValue(name, out FieldInfo fi) != true)
                {
                    BindingFlags bf = BindingFlags.NonPublic | (instance == null ? BindingFlags.Static : BindingFlags.Instance);

                    fi = type.GetField(name, bf);
                    fields[name] = fi ?? throw new Exception($"{nameof(R)}:找不到{name}");
                }

                return (T)fi.GetValue(instance);
            }

            public void Field(string name, object instance, object val)
            {
                if (fields.TryGetValue(name, out FieldInfo fi) != true)
                {
                    BindingFlags bf = BindingFlags.NonPublic | (instance == null ? BindingFlags.Static : BindingFlags.Instance);

                    fi = type.GetField(name, bf);
                    fields[name] = fi ?? throw new Exception($"{nameof(R)}:找不到{name}");
                }

                fi.SetValue(instance, val);
            }
        }
    }
}
