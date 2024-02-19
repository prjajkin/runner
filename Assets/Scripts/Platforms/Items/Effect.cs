using System;

namespace Core.Platforms.Items
{
    /// <summary>
    /// Effect for PlayerView with time
    /// </summary>
    public class Effect
    {
        public float Time;

        public Action<PlayerView> OnStart;
        public Action<PlayerView> OnEnd;

        public Effect(float time, Action<PlayerView> onStart, Action<PlayerView> onEnd)
        {
            Time = time;
            OnStart = onStart;
            OnEnd = onEnd;
        }
    }
}