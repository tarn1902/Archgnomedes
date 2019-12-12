using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archgnomedes
{
    public class DayController : MonoBehaviour
    {
        [Header("Light Source")]
        public Light sun;

        [Header("Time settings")]
        public float dayInSeconds = 240f; // 4mins for a day
        [Range(0, 1)]
        public float currentTime = .25f; // between 0 and 1, 0: is midnight, 0.5 is noon; .025f = 6am

        [Header("Adjustments")]
        public float timeMultipler = 1f;
        public float sunInitalIntensity;
        public float minBrightness = 0.15f;



        private void Start()
        {
            sunInitalIntensity = sun.intensity;
            transform.position = new Vector3(400.46f, 109.43f, 208.34f);
            transform.rotation = new Quaternion(38.852f, -110.059f, -96.409f, 0f);
        }


        private void Update()
        {
            UpdateSun();

            currentTime += (Time.deltaTime / dayInSeconds) * timeMultipler;

            if (currentTime >= 1)
            {
                currentTime = 0;
            }
        }

        private void UpdateSun()
        {
            sun.transform.rotation = Quaternion.Euler(45, (currentTime * 360f) - 90, 0);

            float intensityMultipler = 1;
 
            if (currentTime <= .125f || currentTime >= .875f)
            {
                // 9 p.m. to 3 a.m.
                intensityMultipler = minBrightness;
            }
            else if (currentTime <= .25f)
            {
                // 3 a.m. to 6 a.m.
                intensityMultipler = Mathf.Lerp(minBrightness, 1f, (currentTime - 0.125f) / 0.125f);
            }
            else if (currentTime >= .75f)
            {
                // 6 p.m. to 9 p.m.
                intensityMultipler = Mathf.Lerp(1f, minBrightness, (currentTime - .75f) / 0.125f);
            }

            sun.intensity = sunInitalIntensity * intensityMultipler;
        }
    }
}
