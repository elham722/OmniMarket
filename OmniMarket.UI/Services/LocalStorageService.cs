using Hanssens.Net;
using Microsoft.AspNetCore.Http;
using OmniMarket.UI.Contracts;

namespace OmniMarket.UI.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly LocalStorage _storage;
        private readonly string _userPrefix;

        public LocalStorageService(IHttpContextAccessor httpContextAccessor)
        {
            var config = new LocalStorageConfiguration()
            {
                AutoLoad = true,
                AutoSave = true,
                Filename = "OmniMarket"
            };
            _storage = new LocalStorage(config);

            // از Session ID به‌عنوان پیشوند برای جدا کردن داده‌های هر کاربر استفاده می‌کنیم
            _userPrefix = httpContextAccessor.HttpContext.Session.Id;
        }

        public void ClearStorage(List<string> keys)
        {
            foreach (var key in keys)
            {
                var userKey = $"{_userPrefix}:{key}";
                _storage.Remove(userKey);
            }
        }

        public bool Exists(string key)
        {
            var userKey = $"{_userPrefix}:{key}";
            return _storage.Exists(userKey);
        }

        public T GetStorageValue<T>(string key)
        {
            var userKey = $"{_userPrefix}:{key}";
            return _storage.Get<T>(userKey);
        }

        public void SetStorageValue<T>(string key, T value)
        {
            var userKey = $"{_userPrefix}:{key}";
            _storage.Store(userKey, value);
            _storage.Persist();
        }
    }
}