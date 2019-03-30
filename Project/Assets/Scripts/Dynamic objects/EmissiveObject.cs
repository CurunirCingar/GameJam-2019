using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dynamic_objects
{
    public class EmissiveObject : MonoBehaviour
    {
        private Color savedColor;
        private GameObject emisObject;

        public void Init(GameObject gameObject)
        {
            emisObject = gameObject;
            emisObject.GetComponent<Renderer>().sharedMaterial.EnableKeyword("_EMISSION");
        }

        public void SetEmission(bool state, Color newColor)
        {
            if (state)
            {
                savedColor = emisObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");
                emisObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", newColor);
            } else
            {
                emisObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", savedColor);
            }
        }
    }
}
