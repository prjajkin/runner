using System;

namespace Core.Platforms.Items
{
    /// <summary>
    /// Item which make time slowly.
    /// </summary>
    public class SlowItem : AItem
    {
        #region Overrides of AItem

        public override void Apply(PlayerController player)
        {
            var fastEffect = new Effect(10, OnStart, OnEnd);
            player.AddEffect(fastEffect);
        }

        private void OnStart(PlayerView player)
        {
            player.Speed = MathF.Max(Constants.MinSpeed, player.Speed * 0.8f);
        }
        private void OnEnd(PlayerView player)
        {
            player.Speed *= 1.25f;
        }

        #endregion
    }
}