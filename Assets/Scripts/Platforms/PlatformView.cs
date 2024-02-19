using System;
using System.Collections.Generic;
using Core.Platforms.Items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Platforms
{
    /// <summary>
    /// Platform view (MonoBeh).
    /// </summary>
    public class PlatformView : MonoBehaviour
    {
        [SerializeField] private float _lenght;
        [SerializeField] private List<Transform> _itemsSpawns;
        [SerializeField] private ExitPlatformTrigger _exitTrigger;
        [SerializeField] private EnterPlatformTrigger _enterTrigger;

        private List<AItem> _items;
        public float Lenght => _lenght;

        public event Action PlayerEntered;
        public event Action<PlatformView> PlayerExited;


        public void Init(List<AItem> itemPrefabs)
        {
            foreach (var spawn in _itemsSpawns)
            {
                var prefab = itemPrefabs[Random.Range(0, itemPrefabs.Count)];
                Instantiate(prefab, spawn.position, Quaternion.identity, transform);
            }
            _exitTrigger.PlayerExited += OnPlayerExited;
            _enterTrigger.PlayerEntered += OnPlayerEntered;
        }

        private void OnPlayerEntered(PlayerController obj)
        {
            PlayerEntered?.Invoke();
        }

        private void OnPlayerExited(PlayerController player)
        {
            PlayerExited?.Invoke(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerEntered?.Invoke();
        }

        private void OnDestroy()
        {
            _exitTrigger.PlayerExited -= OnPlayerExited;
            _enterTrigger.PlayerEntered -= OnPlayerEntered;
        }
    }
}
