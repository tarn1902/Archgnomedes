using System.Collections;
using UnityEngine;

namespace Archgnomedes
{

    /// <summary>
    /// This class is used to indicate to the player the length of the laser when they fire the weapon
    /// </summary>
    public class RayIndicator : MonoBehaviour
    {
        [Header("Attributes")]
        public LineRenderer rayLine;
        public float rayWidth = .1f;
        public float rayMaxLength = 15f;

        private void Start()
        {
            //
            Vector3[] rayLaserPos = new Vector3[2]
            {
            Vector3.zero, Vector3.zero
            };
            rayLine.SetPositions(rayLaserPos);
            rayLine.startWidth = rayWidth;
            rayLine.endWidth = rayWidth;
        }

        private void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                RayFromUser(transform.position, Vector3.forward, rayMaxLength);
                rayLine.enabled = true;
            }

            else
            {
                rayLine.enabled = false;
            }
        }


        private void RayFromUser(Vector3 targetPos, Vector3 dir, float length)
        {
            Ray ray = new Ray(targetPos, dir);
            RaycastHit hitInfo;
            Vector3 endPos = targetPos + (length * dir);

            if (Physics.Raycast(ray, out hitInfo, length))
            {
                endPos = hitInfo.point;
            }

            rayLine.SetPosition(0, targetPos);
            rayLine.SetPosition(1, endPos);
        }
    }
}
