using UnityEngine;


namespace Archgnomedes
{

    /// <summary>
    /// This class is used for the character to interact with the environment
    /// </summary>


    public class ForcePush : MonoBehaviour
    {
        [Header("Attributes")]
        public float mass = 3.0f;
        public float power = 0.2f;

        Vector3 impact = Vector3.zero;
        private CharacterController character;

        //Use this for initialization
        private void Start()
        {
            character = GetComponent<CharacterController>();
        }

        //Update is called once per frame
        private void Update()
        {
            if (impact.magnitude > power)
            {
                character.Move(impact * Time.deltaTime);
            }

            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        }

        // Call to add impact
        public void AddImpact(Vector3 direction, float force)
        {
            direction.Normalize();

            direction *= -100;

            character.Move(new Vector3(direction.x, direction.y, direction.z));


            impact += direction.normalized * force / mass;
        }
    }
}
