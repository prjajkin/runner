using UnityEngine;

namespace Core
{
    /// <summary>
    /// Common Constants in Project.
    /// </summary>
    public static class Constants
    {
        public static readonly int PlayerLayerId = LayerMask.NameToLayer("Player");
        public const float NormalSpeed = 15;
        public const float MaxSpeed = 35;
        public const float MinSpeed = 5;
    }
}