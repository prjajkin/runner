using Core.Platforms.Items;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    /// <summary>
    /// Create PLayerView(MonoBeh) and Controls it.
    /// </summary>
    public class PlayerController : IDisposable
    {
        private readonly Transform _playerSpawn; 
        private readonly PlayerView _playerView;
        private bool _disposedValue;

        public Transform TransformView => _playerView.transform;
        public event Action PlayerDied;
    
        public PlayerController(Transform playerSpawn, PlayerView playerViewPrefab)
        {
            _playerSpawn = playerSpawn; 

            _playerView = Object.Instantiate<PlayerView>(playerViewPrefab, _playerSpawn.position, Quaternion.identity);
            _playerView.Init(this);
            _playerView.PlayerDied += OnPlayerDied;
        }

        public void AddEffect(Effect effect)
        {
            _playerView.AddEffect(effect);
        }

        public void Kill()
        {
            _playerView.Stop();
            PlayerDied?.Invoke();
        }

        private void OnPlayerDied()
        {
            PlayerDied?.Invoke();
        }

        public void ResetPosition()
        {
            _playerView.transform.position = _playerSpawn.position;
        }
         
        public void StartMove() => _playerView.StartMove(); 
        public void StartLeft() => _playerView.StartLeft();
        public void EndLeft() => _playerView.EndLeft();
        public void StartRight() => _playerView.StartRight();
        public void EndRight() => _playerView.EndRight();
        public void Jump() => _playerView.Jump();


        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    //  dispose managed state (managed objects)
                }

                if (_playerView != null)
                {
                    _playerView.PlayerDied -= OnPlayerDied;
                    Object.Destroy(_playerView.gameObject);
                }
                _disposedValue = true;
            }
        }

        ~PlayerController()
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
