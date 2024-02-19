using UnityEngine;

namespace Core.Platforms.Items
{
    /// <summary>
    /// Base Class for Items on platforms
    /// </summary>
    public abstract class AItem : MonoBehaviour
    {  
        public abstract void Apply(PlayerController player);

        private void OnTriggerEnter(Collider other)
        { 
            if ( other.gameObject.layer == Constants.PlayerLayerId )
            {
                var playerView = other.GetComponent<PlayerView>();

                Apply(playerView.Controller);
                Destroy(gameObject);
            }
        }
    }
}