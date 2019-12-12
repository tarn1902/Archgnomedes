using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Designers making scripts in lowercase
/// It hurts the soul. - PJ
/// </summary>
namespace Archgnomedes
{
    public class playaudio : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject uiObject;
        void Start()
        {
            uiObject.SetActive(false);
        }

        void OnTriggerEnter(Collider player)
        {
            if (player.gameObject.tag == "Player")
            {
                uiObject.SetActive(true);

            }
        }

    }
}