using PlayerInput;

namespace Dynamic_objects
{
    public interface IActivateObject
    {
        float LastBadActionTime { get; }
        Player LastPlayer { get; }

        /// <summary>
        /// Активировать ловушку
        /// </summary>
        /// <param name="player"></param>
        /// <param name="activeState"></param>
        void Activate(Player player, bool activeState);
    }
}