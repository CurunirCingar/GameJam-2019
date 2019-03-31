using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dynamic_objects
{
    public class EmissiveObject : MonoBehaviour
    {
        private List<Color> savedColors;
        private List<GameObject> emisObjects;

        public void Init(GameObject gameObject)
        {
            if (gameObject == null)
            {
                gameObject = this.gameObject;
            }

            List<GameObject> list = new List<GameObject> {
                gameObject
            };

            Init(list);

            savedColors = new List<Color> { emisObjects[0].GetComponent<Renderer>().material.GetColor("_EmissionColor") };
        }

        public void Init(List<GameObject> gameObjects)
        {
            if (gameObjects.Count == 0)
            {
                gameObjects.Add(gameObject);
            }

            emisObjects = gameObjects;
            foreach (var emisObject in emisObjects)
            {
                emisObject.GetComponent<Renderer>().sharedMaterial.EnableKeyword("_EMISSION");
            }
        }

        public void SetEmission(bool state, Color newColor)
        {
            foreach (var emisObject in emisObjects)
            {
                if (state)
                {
                    savedColors.Add(emisObject.GetComponent<Renderer>().material.GetColor("_EmissionColor"));
                    emisObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", newColor);
                }
                else
                {
                    savedColors.Remove(newColor);
                    emisObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", savedColors[savedColors.Count-1]);
                }
            }
        }
    }
}
