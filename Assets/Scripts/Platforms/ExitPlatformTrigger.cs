using System;
using UnityEngine;

namespace Core.Platforms
{
    public class ExitPlatformTrigger : MonoBehaviour
    {
        public event Action<PlayerController> PlayerExited;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == Constants.PlayerLayerId)
            {
                var playerView = other.GetComponent<PlayerView>();

                PlayerExited?.Invoke(playerView.Controller);
            }
        }
    }
}