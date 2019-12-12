using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Archgnomedes
{
    /// <summary>
    /// Class to display the Frames per second on the UI
    /// </summary>
    public class FPSCounter : MonoBehaviour
    {
        [Header("Attributes")]
        public int frameRate;
        public Text textBox;


        private void LateUpdate()
        {
            float current = 0;
            current = (int)(1f / Time.unscaledDeltaTime);
            frameRate = (int)current;
            textBox.text = "FPS: " + frameRate.ToString();

            if (textBox == null)
            {
                return;
            }

            
                    
        }
    }
}
