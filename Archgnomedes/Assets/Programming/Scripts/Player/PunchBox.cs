/*----------------------------------------
File Name: PunchBox.cs
Purpose: Controls how the punchbox will 
    behaves when triggered
Author: Tarn Cooper
Modified: 31/10/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;



namespace Archgnomedes
{
    public class PunchBox : MonoBehaviour
    {

        [Header("Glove-Movement Settings")]
        public float maxGloveRange = 5;
        public float maxSpringRange = 40;
        public float motionSpeed = 10;
        public float returnSpeed = 5;
        public float angleOfReturn = 45;
        public float maxFireRange = 10;

        [Header("GameObjects")]
        public GameObject glove = null;
        public GameObject jackBox = null;
        public LineRenderer springLine = null;
        public Transform springPoint = null;
        public Transform glovePoint = null;

        [HideInInspector] public bool inMotion = false;
        [HideInInspector] public bool collided = false;

        private AudioSource audioSource = null;
        private SphereCollider gloveCollider = null;
        private RaycastHit hit = new RaycastHit();
        private PlayerInput inputControl = null;
        private Vector3 originalPosition = Vector3.zero;
        private bool inReturn = false;
        private Rigidbody gloveRB = null;
        private float returnModifier = 0;
        private float maxModifier = 20;
        public float minGloveRange = 0.2f;

        PlayerSoundControl soundControl = null;

        //-----------------------------------------------------------
        // Starts by getting all components and values
        //-----------------------------------------------------------
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            gloveCollider = glove.GetComponent<SphereCollider>();
            inputControl = GetComponent<PlayerInput>();
            gloveRB = glove.GetComponent<Rigidbody>();
            soundControl = GetComponent<PlayerSoundControl>();
        }

        //-----------------------------------------------------------
        // Updates each function to make jackBox move
        //-----------------------------------------------------------
        void FixedUpdate()
        {
            if (inMotion || inReturn)
            {
                GloveCollision();
            }

            if (inMotion)
            {
                MoveGloveForward();
            }
            else if (inReturn)
            {
                MoveGloveBack();
            }

            CreateSpring();

        }

        //-----------------------------------------------------------
        // Activates jackBox
        // return (bool): returns if can fire again
        //-----------------------------------------------------------
        public bool Fire()
        {
            if (Physics.Raycast(new Ray(glove.transform.position, glove.transform.right), out hit, maxFireRange))
            {
                return true;
            }
            else
            {
                SoundManager.instance.PlayAudio("PunchLaunch");
                glove.transform.parent = null;
                inMotion = true;
                originalPosition = glove.transform.position;
                gloveCollider.enabled = true;
                springLine.enabled = true;
                gloveRB.constraints = RigidbodyConstraints.FreezeRotation;
                return false;
            }
            
        }

        //-----------------------------------------------------------
        // Moves glove to maximum reach
        //-----------------------------------------------------------
        void MoveGloveForward()
        {
            if (Vector3.Distance(glove.transform.position, originalPosition) >= maxGloveRange)
            {
                inMotion = false;
                inReturn = true;
                gloveCollider.enabled = false;
            }
            else
            {
                gloveRB.MovePosition(glove.transform.position + (jackBox.transform.right * motionSpeed) * Time.fixedDeltaTime);
            }

        }

        //-----------------------------------------------------------
        // Moves glove back to orginal position
        //-----------------------------------------------------------
        void MoveGloveBack()
        {

            if (Vector3.Distance(glove.transform.position, jackBox.transform.position) <= minGloveRange || Vector3.Angle(jackBox.transform.right, (glove.transform.position - jackBox.transform.position).normalized) >= angleOfReturn)
            {
                inReturn = false;
                inputControl.readyToFirePB = true;
                springLine.enabled = false;
                gloveRB.constraints = RigidbodyConstraints.FreezeAll;
                glove.transform.parent = jackBox.transform.parent;
                glove.transform.rotation = jackBox.transform.rotation;
                glove.transform.position = jackBox.transform.position;
                returnModifier = 0;
            }
            else
            {
                gloveRB.MovePosition(glove.transform.position + ((jackBox.transform.position - glove.transform.position) * (returnSpeed + returnModifier) * Time.fixedDeltaTime));
                if (maxModifier >= returnModifier)
                {
                    returnModifier += 0.5f;
                }
            }
        }

        //-----------------------------------------------------------
        // Glove reacts if it has collided with something 
        // (onCollisonEnter function on glove)
        //-----------------------------------------------------------
        void GloveCollision()
        {
            if (collided)
            {
                SoundManager.instance.PlayAudio("PunchHit");
                if (inMotion)
                {
                    inMotion = false;
                    inReturn = true;
                    gloveCollider.enabled = false;
                }
                collided = false;
            }
        }

        void CreateSpring()
        {
            springLine.SetPosition(0, springPoint.position);
            springLine.SetPosition(1, glovePoint.position);
        }
    }
}



