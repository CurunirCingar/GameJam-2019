using UnityEngine.Networking;

namespace Network
{
    public class NetworkManager : NetworkBehaviour
    {
        private static NetworkManager instance;
        
        private void Awake()
        {
            if(instance)
                Destroy(this);
            else
                instance = this;
        }
        
        
        
    }
}