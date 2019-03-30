using System;
using Dynamic_objects;
using UnityEngine;
using UnityEngine.Networking;

namespace PlayerInput
{
    public class InteractableObject : NetworkBehaviour
    {
        [SerializeField] private GameObject activationObjectParent;
        private IActivateObject activateObject;
        
        public void Interact(GameObject player, bool activated)
        {
            RpcInteract(player, activated);
        }

        [ClientRpc]
        private void RpcInteract(GameObject playerObject, bool activate)
        {
            if (activateObject == null)
                activateObject = activationObjectParent.GetComponent<IActivateObject>();
            if (activateObject == null)
                throw new NullReferenceException("Connected GameObject doesnt have activateable object!");
            
            Player player = null;
            if (playerObject != null)
                player = playerObject.GetComponent<Player>();
            
            activateObject.Activate(player, activate);
        }
    }
}