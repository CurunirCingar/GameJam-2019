using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
    public class GameController : MonoBehaviour
    {
        private static GameController instance;

        public GameObject Barrier;
        public Transform BadSpawnPosition;
        public Transform GoodSpawnPosition;
        
        
        private PlayerManager player1;
        private PlayerManager player2;
        
        public static GameController Manager
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

        public Dynamic_objects.IActivateObject GetNearestActivatableObject(Vector3 killedPlayerPosition)
        {
            Dynamic_objects.IActivateObject answer = null;
            float minDist = float.PositiveInfinity;

            foreach (var activateObject in FindObjectsOfType<Dynamic_objects.EmissiveObject>())
            {
                Vector3 position = (activateObject as Dynamic_objects.EmissiveObject).transform.position;
                if (Vector3.Distance(position, killedPlayerPosition) < minDist)
                {
                    minDist = Vector3.Distance(position, killedPlayerPosition);
                    answer = activateObject as Dynamic_objects.IActivateObject;
                }
            }

            return answer;
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