using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack.Redis;


namespace Whale.Api.Providers
{
    public class CacheProvider : ICacheProvider
    {
        private IConfigProvider _config;
        private RedisManagerPool _manager;

        public IDictionary<String, T> GetAll<T>()
        {
            using (var client = _manager.GetClient())
            {
                
                try
                {
                    var keys = client.GetAllKeys();
                    if (keys.Count <= 0)
                    {
                        return new Dictionary<string, T>();
                    }
                    return client.GetAll<T>(keys);

                }
                catch (RedisResponseException exception)
                {
                    Console.Write(exception);
                    return null;
                }
            }
        }

        public T Get<T>(string key)
        {
            using (var client = _manager.GetClient())
            {
                return client.Get<T>(key);
            }
        }

        public void Set<T>(string key, T value)
        {
            using (var client = _manager.GetClient())
            {
                client.Set<T>(key, value);
            }
        }

        public void Clear<T>()
        {
            using (var client = _manager.GetClient())
            {
                var keys = client.GetAllKeys();
                client.RemoveAll(keys);
            }
        }

        public CacheProvider()
        {
            _config = new ConfigProvider();
            _manager = new RedisManagerPool(_config.RedisHostname);
        }
    }

    public interface ICacheProvider
    {
        IDictionary<String, T> GetAll<T>();
        T Get<T>(String key);
        void Set<T>(String key, T value);
        void Clear<T>();
    }
}
