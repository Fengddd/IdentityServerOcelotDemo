using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace ProjectCore.Common.RedisHelper
{
   public class RedisHelper
    {
        public RedisHelper()
        {
            AddRegisterEvent();
        }

        #region String 可以设置过期时间
        private static readonly IDatabase RedisBase = RedisManager.Instance().GetDatabase();
      
        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public static bool SetStringKey(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            return RedisBase.StringSet(key, value, expiry);
        }

        /// <summary>
        /// 异步保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public static async Task<bool> SetStringKeyAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            return await RedisBase.StringSetAsync(key, value, expiry);
        }

        /// <summary>
        /// 保存多个key value
        /// </summary>
        /// <param name="arr">key</param>
        /// <returns></returns>
        public static bool SetStringKey(KeyValuePair<RedisKey, RedisValue>[] arr)
        {
            return RedisBase.StringSet(arr);
        }

        /// <summary>
        /// 异步保存多个key value
        /// </summary>
        /// <param name="arr">key</param>
        /// <returns></returns>
        public static async Task<bool> SetStringKeyAsync(KeyValuePair<RedisKey, RedisValue>[] arr)
        {
            return await RedisBase.StringSetAsync(arr);
        }

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static bool SetStringKey<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            string json = JsonConvert.SerializeObject(obj);
            return RedisBase.StringSet(key, json, expiry);
        }

        /// <summary>
        /// 异步保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task<bool> SetStringKeyAsync<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            string json = JsonConvert.SerializeObject(obj);
            return await RedisBase.StringSetAsync(key, json, expiry);
        }

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>

        public static RedisValue GetStringKey(string key)
        {
            return RedisBase.StringGet(key);
        }

        /// <summary>
        /// 异步获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>

        public static async Task<RedisValue> GetStringKeyAsync(string key)
        {
            return await RedisBase.StringGetAsync(key);
        }

        /// <summary>
        /// 获取多个Key
        /// </summary>
        /// <param name="listKey">Redis Key集合</param>
        /// <returns></returns>
        public static RedisValue[] GetStringKey(List<RedisKey> listKey)
        {
            return RedisBase.StringGet(listKey.ToArray());
        }

        /// <summary>
        /// 异步获取多个Key
        /// </summary>
        /// <param name="listKey">Redis Key集合</param>
        /// <returns></returns>
        public static async Task<RedisValue[]> GetStringKeyAsync(List<RedisKey> listKey)
        {
            return await RedisBase.StringGetAsync(listKey.ToArray());
        }

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetStringKey<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(RedisBase.StringGet(key));
        }

        /// <summary>
        /// 异步获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<T> GetStringKeyAsync<T>(string key)
        {
            var str =await RedisBase.StringGetAsync(key);
            return JsonConvert.DeserializeObject<T>(str);
        }

        #endregion

        #region Hash

        /// <summary>
        /// 保存一个集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="list">数据集合</param>
        /// <param name="getModelId"></param>
        public static void HashSet<T>(string key, List<T> list, Func<T, string> getModelId)
        {
            List<HashEntry> listHashEntry = new List<HashEntry>();
            foreach (var item in list)
            {
                string json = JsonConvert.SerializeObject(item);
                listHashEntry.Add(new HashEntry(getModelId(item), json));
            }
            RedisBase.HashSet(key, listHashEntry.ToArray());
        }

        /// <summary>
        /// 异步保存一个集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="list">数据集合</param>
        /// <param name="getModelId"></param>
        public static async Task HashSetAsync<T>(string key, List<T> list, Func<T, string> getModelId)
        {
            List<HashEntry> listHashEntry = new List<HashEntry>();
            foreach (var item in list)
            {
                string json = JsonConvert.SerializeObject(item);
                listHashEntry.Add(new HashEntry(getModelId(item), json));
            }
           await RedisBase.HashSetAsync(key, listHashEntry.ToArray());
        }

        /// <summary>
        /// 获取Hash中的单个key的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="hasFildValue">RedisValue</param>
        /// <returns></returns>
        public static T GetHashKey<T>(string key, string hasFildValue)
        {
            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(hasFildValue))
            {
                RedisValue value = RedisBase.HashGet(key, hasFildValue);
                if (!value.IsNullOrEmpty)
                {
                    return JsonConvert.DeserializeObject<T>(value);
                }
            }
            return default(T);
        }

        /// <summary>
        /// 异步获取Hash中的单个key的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="hasFildValue">RedisValue</param>
        /// <returns></returns>
        public static async Task<T> GetHashKeyAsync<T>(string key, string hasFildValue)
        {
            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(hasFildValue))
            {
                RedisValue value =await RedisBase.HashGetAsync(key, hasFildValue);
                if (!value.IsNullOrEmpty)
                {
                    return JsonConvert.DeserializeObject<T>(value);
                }
            }
            return default(T);
        }

        /// <summary>
        /// 获取hash中的多个key的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="listhashFields">RedisValue value</param>
        /// <returns></returns>
        public static List<T> GetHashKey<T>(string key, List<RedisValue> listhashFields)
        {
            List<T> result = new List<T>();
            if (!string.IsNullOrWhiteSpace(key) && listhashFields.Count > 0)
            {
                RedisValue[] value = RedisBase.HashGet(key, listhashFields.ToArray());
                foreach (var item in value)
                {
                    if (!item.IsNullOrEmpty)
                    {
                        result.Add(JsonConvert.DeserializeObject<T>(item));
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 异步获取hash中的多个key的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="listhashFields">RedisValue value</param>
        /// <returns></returns>
        public static async Task<List<T>> GetHashKeyAsync<T>(string key, List<RedisValue> listhashFields)
        {
            List<T> result = new List<T>();
            if (!string.IsNullOrWhiteSpace(key) && listhashFields.Count > 0)
            {
                RedisValue[] value =await RedisBase.HashGetAsync(key, listhashFields.ToArray());
                foreach (var item in value)
                {
                    if (!item.IsNullOrEmpty)
                    {
                        result.Add(JsonConvert.DeserializeObject<T>(item));
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取所有Redis key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T> GetHashAll<T>(string key)
        {
            List<T> result = new List<T>();
            RedisValue[] arr = RedisBase.HashKeys(key);
            foreach (var item in arr)
            {
                if (!item.IsNullOrEmpty)
                {
                    result.Add(JsonConvert.DeserializeObject<T>(item));
                }
            }
            return result;
        }

        /// <summary>
        /// 异步获取所有Redis key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<List<T>> GetHashAllAsync<T>(string key)
        {
            List<T> result = new List<T>();
            RedisValue[] arr =await RedisBase.HashKeysAsync(key);
            foreach (var item in arr)
            {
                if (!item.IsNullOrEmpty)
                {
                    result.Add(JsonConvert.DeserializeObject<T>(item));
                }
            }
            return result;
        }

        /// <summary>
        /// 获取Hashkey所有的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T> HashGetAll<T>(string key)
        {
            List<T> result = new List<T>();
            HashEntry[] arr = RedisBase.HashGetAll(key);
            foreach (var item in arr)
            {
                if (!item.Value.IsNullOrEmpty)
                {
                    result.Add(JsonConvert.DeserializeObject<T>(item.Value));
                }
            }
            return result;
        }

        /// <summary>
        /// 异步获取Hashkey所有的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<List<T>> HashGetAllAsync<T>(string key)
        {
            List<T> result = new List<T>();
            HashEntry[] arr =await RedisBase.HashGetAllAsync(key);
            foreach (var item in arr)
            {
                if (!item.Value.IsNullOrEmpty)
                {
                    result.Add(JsonConvert.DeserializeObject<T>(item.Value));
                }
            }
            return result;
        }

        /// <summary>
        /// 异步删除hasekey
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteHashAsync(RedisKey key, RedisValue hashField)
        {
            return await RedisBase.HashDeleteAsync(key, hashField);
        }

        /// <summary>
        /// 删除hasekey
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public static bool DeleteHash(RedisKey key, RedisValue hashField)
        {
            return RedisBase.HashDelete(key, hashField);
        }

        #endregion

        #region key

        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>是否删除成功</returns>
        public static bool KeyDelete(string key)
        {
            return RedisBase.KeyDelete(key);
        }

        /// <summary>
        /// 异步删除单个key
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>是否删除成功</returns>
        public static async Task<bool> KeyDeleteAsync(string key)
        {
            return await RedisBase.KeyDeleteAsync(key);
        }

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys">rediskey</param>
        /// <returns>成功删除的个数</returns>
        public static long KeyDeleteRange(RedisKey[] keys)
        {
            return RedisBase.KeyDelete(keys);
        }

        /// <summary>
        /// 异步删除多个key
        /// </summary>
        /// <param name="keys">rediskey</param>
        /// <returns>成功删除的个数</returns>
        public static async Task<long> KeyDeleteRangeAsync(RedisKey[] keys)
        {          
            return await RedisBase.KeyDeleteAsync(keys);
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns></returns>
        public static bool KeyExists(string key)
        {
            return RedisBase.KeyExists(key);
        }

        /// <summary>
        /// 异步判断key是否存在
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns></returns>
        public static async Task<bool> KeyExistsAsync(string key)
        {
            return await RedisBase.KeyExistsAsync(key);
        }

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key">就的redis key</param>
        /// <param name="newKey">新的redis key</param>
        /// <returns></returns>
        public static bool KeyRename(string key, string newKey)
        {
            return RedisBase.KeyRename(key, newKey);
        }

        /// <summary>
        /// 异步重新命名key
        /// </summary>
        /// <param name="key">就的redis key</param>
        /// <param name="newKey">新的redis key</param>
        /// <returns></returns>
        public static async Task<bool> KeyRenameAsync(string key, string newKey)
        {
            return await RedisBase.KeyRenameAsync(key, newKey);
        }
        
        #endregion


        /// <summary>
        /// 追加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void StringAppend(string key, string value)
        {
            ////追加值，返回追加后长度
            RedisBase.StringAppend(key, value);
            
        }

        /// <summary>
        /// 异步追加值String
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void StringAppendAsync(string key, string value)
        {
            ////追加值，返回追加后长度
            RedisBase.StringAppendAsync(key, value);
        }

        #region 注册事件

        /// <summary>
        /// 添加注册事件
        /// </summary>
        private static void AddRegisterEvent()
        {
            RedisManager.Instance().ConnectionRestored += ConnMultiplexer_ConnectionRestored;
            RedisManager.Instance().ConnectionFailed += ConnMultiplexer_ConnectionFailed;
            RedisManager.Instance().ErrorMessage += ConnMultiplexer_ErrorMessage;
            RedisManager.Instance().ConfigurationChanged += ConnMultiplexer_ConfigurationChanged;
            RedisManager.Instance().HashSlotMoved += ConnMultiplexer_HashSlotMoved;
            RedisManager.Instance().InternalError += ConnMultiplexer_InternalError;
            RedisManager.Instance().ConfigurationChangedBroadcast += ConnMultiplexer_ConfigurationChangedBroadcast;
        }

        /// <summary>
        /// 重新配置广播时（通常意味着主从同步更改）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChangedBroadcast)}: {e.EndPoint}");
        }

        /// <summary>
        /// 发生内部错误时（主要用于调试）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_InternalError(object sender, InternalErrorEventArgs e)
        {
            throw new RedisException("Redis内部错误"+e.Exception.Message);
        }

        /// <summary>
        /// 更改集群时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_HashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            Console.WriteLine(
                $"{nameof(ConnMultiplexer_HashSlotMoved)}: {nameof(e.OldEndPoint)}-{e.OldEndPoint} To {nameof(e.NewEndPoint)}-{e.NewEndPoint}, ");
        }

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChanged)}: {e.EndPoint}");
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ErrorMessage(object sender, RedisErrorEventArgs e)
        {
            throw new RedisException("Redis发生错误" + e.Message);
        }

        /// <summary>
        /// 物理连接失败时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            throw new RedisException("Redis物理连接失败时"+e.Exception.Message);
        }

        /// <summary>
        /// 建立物理连接时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConnectionRestored)}: {e.EndPoint}");
        }

        #endregion 注册事件

    }
}
