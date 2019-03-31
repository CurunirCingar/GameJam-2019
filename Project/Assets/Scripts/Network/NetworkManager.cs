using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
    public class NetworkManager : MonoBehaviour
    {
        private static NetworkManager instance;

        private PlayerManager player1;
        private PlayerManager player2;
        
        public static NetworkManager Manager
        {
            get { return instance; }
        }

        public PlayerManager Player1
        {
            get { return player1; }
        }

        public PlayerManager Player2
        {
            get { return player2; }
        }

        public void AddPlayer(PlayerManager player)
        {
            if (player1)
                player2 = player;
            else
                player1 = player;
        }
        
        private void Awake()
        {
            if(instance)
                Destroy(this);
            else
                instance = this;
        }                     
    }
}