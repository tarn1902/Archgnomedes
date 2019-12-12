using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archgnomedes
{
    public class HighlightAction : MonoBehaviour
    {
        [Header("Indicator")]
        public Material highlightMaterial;
        public float rayDistance = 10f;
        Material originalMaterial;
        GameObject lastHighlightedObject;

        // Update is called once per frame
        void Update()
        {
            HighlightCentreCamera();
        }

        //highlights the object to show interactable objects
        private void HighlightObject(GameObject gameObject)
        {
            if (gameObject.tag == "Object")
            {
                ClearHighlight();
                originalMaterial = gameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial;
                gameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial = highlightMaterial;
                lastHighlightedObject = gameObject;
            }
        }

        private void ClearHighlight()
        {
            if (lastHighlightedObject != null)
            {
                lastHighlightedObject.GetComponentInChildren<MeshRenderer>().sharedMaterial = originalMaterial;
                lastHighlightedObject = null;
            }
        }

        private void HighlightCentreCamera()
        {
            // Ray from the centre of the camera
            // Viewport co-ordinates are normalized
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
            RaycastHit hitInfo;

            //Check the ray to make sure the object is being hit
            if (Physics.Raycast(ray, out hitInfo, rayDistance))
            {
                //Debug.Log("Object hit" + hitInfo.collider.name);
                GameObject hitObject = hitInfo.collider.gameObject;
                HighlightObject(hitObject);
            }

            else
            {
                ClearHighlight();
            }
        }
    }
}
