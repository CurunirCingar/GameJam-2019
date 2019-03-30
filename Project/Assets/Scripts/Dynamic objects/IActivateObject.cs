using PlayerInput;

namespace Dynamic_objects
{
    public interface IActivateObject
    {
        void Activate(Player player, bool activeState);
    }
}