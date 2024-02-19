using System;
using System.Collections.Generic;
using Core.Platforms.Items;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Core.Platforms
{
    /// <summary>
    /// Class for Creation and controlling platforms.
    /// </summary>
    public class PlatformManager: IDisposable
    {
        private const int StartPlatformCount = 4;
        private readonly Transform _platformSpawn;
        private List<PlatformView> _platforms = new List<PlatformView>();
        private List<PlatformView> _platformPrefabs;
        private List<AItem> _itemPrefabs;
        private bool _disposedValue;

        public PlatformManager(Transform platformSpawn, List<PlatformView> platformPrefabs,
            List<AItem> itemPrefabs )
        {
            _platformPrefabs = platformPrefabs;
            _platformSpawn = platformSpawn;
            _itemPrefabs = itemPrefabs; 
        }

        public void Start()
        {
            for (int i = 0; i < StartPlatformCount; i++)
            {
                CreateNextPlatform();
            }
        }
     

        public void Reset()
        {
            Clear();
            Start();
        }

        public void Clear()
        {
            for (var i = _platforms.Count - 1; i >= 0; i--)
            {
                var platform = _platforms[i];
                DestroyPlatform(platform);
            }

            _platforms = new List<PlatformView>();
        }

        private void CreateNextPlatform()
        {
            var spawnPoint = _platformSpawn.position;
            if (_platforms.Count > 0)
            {
                var last = _platforms[^1];
                spawnPoint = last.transform.position + Vector3.forward*last.Lenght;
            }

            var prefab = _platformPrefabs[Random.Range(0, _platformPrefabs.Count)];
        
            var platform = Object.Instantiate(prefab, spawnPoint, Quaternion.identity, _platformSpawn);
            platform.Init(_itemPrefabs);
            platform.PlayerEntered += OnPlayerPlatformEntered;
            platform.PlayerExited += DestroyPlatform;
            _platforms.Add(platform);
        }

        private void OnPlayerPlatformEntered()
        {
            CreateNextPlatform();
        }

        private void DestroyPlatform(PlatformView platformView)
        {
            platformView.PlayerExited -= DestroyPlatform;
            platformView.PlayerEntered -= OnPlayerPlatformEntered;
            if (platformView!=null && platformView.gameObject != null)
            {
                Object.Destroy(platformView.gameObject);
            }
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                
                }

                Clear();
                _disposedValue = true;
            }
        }

        ~PlatformManager()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }
}