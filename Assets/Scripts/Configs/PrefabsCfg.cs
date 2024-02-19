using System.Collections.Generic;
using Core.Platforms;
using Core.Platforms.Items;
using UnityEngine;

namespace Core.Configs
{
    /// <summary>
    /// Config for 
    /// </summary>
    [CreateAssetMenu(fileName = "PrefabsCfg", menuName = "Configs/PrefabsCfg", order = 0)]
    public class PrefabsCfg : ScriptableObject
    {
        [Header("Platform Prefabs")]
        [SerializeField] List<PlatformView> _platformPrefabs = new List<PlatformView>();
        public List<PlatformView> PlatformPrefabs =>  _platformPrefabs;

        [Header("Item Prefabs")]
        [SerializeField] List<AItem> _itemPrefabs = new List<AItem>();
        public List<AItem> ItemPrefabs => _itemPrefabs;

        [Header("player Prefab")]
        [SerializeField] PlayerView _playerPrefab ;

        public PlayerView PlayerPrefab => _playerPrefab;


    }
}