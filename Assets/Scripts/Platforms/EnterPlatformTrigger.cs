using System;
using UnityEngine;

namespace Core.Platforms
{
    public class EnterPlatformTrigger : MonoBehaviour
    {
        public event Action<PlayerController> PlayerEntered;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == Constants.PlayerLayerId)
            {
                var playerView = other.GetComponent<PlayerView>();

                PlayerEntered?.Invoke(playerView.Controller);
            }
        }
    }
}