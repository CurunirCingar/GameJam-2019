using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class NetworkController : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Networking.NetworkManager manager;
        [SerializeField] private UnityEngine.Networking.NetworkDiscovery discovery;
        [SerializeField] private Button joinButton;
        public void Start()
        {
            discovery.Initialize();
            discovery.StartAsClient();
        }
        
        public void StartAsHost()
        {
            manager.StartHost();
            
            discovery.StopBroadcast();
            discovery.Initialize();
            discovery.StartAsServer();
        }

        public void StartAsClient()
        {
            manager.StartClient();
            discovery.StopBroadcast();
        }

        private void Update()
        {
            if (discovery.broadcastsReceived.Count > 0)
            {
                foreach (var broadcastResult in discovery.broadcastsReceived)
                {
                    var server = broadcastResult.Key;
                    var r = broadcastResult.Value;
                    
                    Debug.Log(server);

                    manager.networkAddress = server;
                    joinButton.interactable = true;

                    break;
                }
                
                discovery.broadcastsReceived.Clear();
            }
        }
    }
}