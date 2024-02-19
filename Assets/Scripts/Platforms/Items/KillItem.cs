using System;

namespace Core.Platforms.Items
{
    /// <summary>
    /// Item which Kill player.
    /// </summary>
    public class KillItem : AItem
    {
        #region Overrides of AItem

        public override void Apply(PlayerController player)
        { 
            player.Kill();
        }
          
        #endregion
    }
}