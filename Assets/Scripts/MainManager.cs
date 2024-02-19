using Core.Configs;
using Core.Platforms;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Start Point of app.Control main game circle.
    /// </summary>
    public class MainManager : MonoBehaviour
    {
        [SerializeField] private PrefabsCfg _prefabConfig;
        [SerializeField] private Transform _playerSpawn;
        [SerializeField] private Transform _platformSpawn;
        [SerializeField] private Transform _cameraTransform;

        private PlayerController _player;
        private InputController _inputController;
        private UserInput _userInput;
        private CameraController _cameraController;
        private PlatformManager _platformManager;

        private void Awake()
        {
            Init();
            _platformManager.Start();
            _player.StartMove();
        }

        private void Init()
        {
            _userInput = new UserInput();
            _userInput.Player.Enable();

            _player = new PlayerController(_playerSpawn, _prefabConfig.PlayerPrefab);
            _inputController = new InputController(_userInput, _player);
            _cameraController = new CameraController(_cameraTransform, _player.TransformView);
            _platformManager = new PlatformManager(_platformSpawn, _prefabConfig.PlatformPrefabs, _prefabConfig.ItemPrefabs);

            _player.PlayerDied += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _player.ResetPosition();
            _cameraController.ResetPosition();
            _platformManager.Reset();
            _player.StartMove();
        }

        private void Update()
        { 
            _cameraController.Move();
        }
    }
}

// I am know di container's plugins, but intentionally did not use in this project.