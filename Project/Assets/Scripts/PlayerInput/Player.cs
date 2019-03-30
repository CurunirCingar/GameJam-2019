using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace PlayerInput
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private Camera camera;
        
        
        [SyncVar(hook = nameof(ApplyColorChange))] private Color playerColor;

        private InteractableObject prevObject = null;
        private List<Material> materials = new List<Material>();
        
        public Color PlayerColor
        {
            get { return playerColor; }
        }

        private void Awake()
        {
            playerColor = Random.ColorHSV();
        }

        private void Start()
        {
            if (isLocalPlayer)
            {
                camera.transform.parent = transform.parent;
            }
            else
            {
                Destroy(GetComponent<BasicBehaviour>());
                Destroy(GetComponent<MoveBehaviour>());
                Destroy(GetComponent<FlyBehaviour>());
                Destroy(GetComponent<AimBehaviourBasic>());
                Destroy(camera.gameObject);
            }
        }

        private void ApplyColorChange(Color newPlayerColor)
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                foreach (var material in renderer.materials)
                {
                    material.color = newPlayerColor;
                }
            }
        }

        private void Update()
        {
            CheckForPlayerInput();
        }

        [ClientCallback]
        private void CheckForPlayerInput()
        {
            if (!isLocalPlayer)
                return;

            if (Input.GetButton("Fire1"))
            {
                RaycastHit hit;
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    var interactedObject = hit.transform.GetComponent<InteractableObject>();
                    if(prevObject && prevObject != interactedObject)
                        CmdSetInteraction(prevObject.gameObject, false);
                    if (interactedObject && interactedObject != prevObject)
                    {
                        prevObject = interactedObject;
                        CmdSetInteraction(interactedObject.gameObject, true);
                    }
                    
                }
                else if (prevObject)
                {
                    CmdSetInteraction(prevObject.gameObject, false);
                    prevObject = null;
                }
            }
        }

        [Command]
        private void CmdSetInteraction(GameObject interactionObject, bool activated)
        {
            var interaction = interactionObject.GetComponent<InteractableObject>();
            interaction.Interact(gameObject, activated);
        }

        [Command]
        public void CmdChangePlayerColor(Color newColor)
        {
            playerColor = newColor;
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Test color change")]
        private void TestColorChange()
        {
            CmdChangePlayerColor(Random.ColorHSV());
        }
        #endif
    }
}