using PlayerInput;
using UnityEngine;

namespace Abilities
{
    public class Ability : MonoBehaviour
    {
        private Player player;
        
        public Player Player
        {
            get { return player; }
        }

        public virtual void AttachToPlayer(Player player)
        {
            this.player = player;
            player.Abilities.Add(this);
            transform.parent = player.transform;
        }
    }
}