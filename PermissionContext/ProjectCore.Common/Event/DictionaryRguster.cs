using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Common.Event
{
    public class DictionaryRguster
    {
        public static void DictionaryRgustered<TKey, TValue>(TKey key, TValue value, Dictionary<TKey, List<TValue>> dictionarys)
        {
            if (dictionarys.TryGetValue(key, out List<TValue> handlers))
            {
                if (!handlers.Contains(value))
                {
                    dictionarys[key].Add(value);
                }
                else
                {
                    dictionarys[key] = new List<TValue> { value };
                }
            }
            else
            {
                dictionarys.TryAdd(key, new List<TValue> { value });
            }
        }
    }
}
