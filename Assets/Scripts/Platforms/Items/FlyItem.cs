namespace Core.Platforms.Items
{

    /// <summary>
    /// Item for player flying.
    /// </summary>
    public class FlyItem : AItem
    {
        #region Overrides of AItem


        public override void Apply(PlayerController player)
        {
            var fastEffect = new Effect(10, OnStart, OnEnd);
            player.AddEffect(fastEffect);
        }

        private void OnStart(PlayerView player)
        {
            player.SetGravity(false);
        }
        private void OnEnd(PlayerView player)
        {
            player.SetGravity(true);
        }


        #endregion
    }
}