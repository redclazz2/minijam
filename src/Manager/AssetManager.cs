using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace minijam.Manager
{
    public static class AssetManager
    {
        private static ContentManager contentManager;
        private static Dictionary<string, object> assetCache = [];

        public static void Initialize(ContentManager _contentManager)
        {
            contentManager = _contentManager;
        }

        public static T Load<T>(string key)
        {
            if (assetCache.TryGetValue(key, out var asset))
                return (T)asset;

            var loaded = contentManager.Load<T>(key);
            assetCache[key] = loaded;
            return loaded;
        }
    }
}