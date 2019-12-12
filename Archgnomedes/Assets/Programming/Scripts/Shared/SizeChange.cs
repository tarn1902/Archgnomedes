/*----------------------------------------
File Name: SizeChange.cs
Purpose: Changes size of object
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

namespace Archgnomedes
{
    public class SizeChange : MonoBehaviour
    {
        [Header("Rigidbody Settings")]
        public Vector3[] sizes;
        public float[] masses;
        public float[] angularDrags;
        public float[] drags;

        [Header("Sizing Settings")]
        public int originalSize = 1;


        [Header("Objects")]
        public Material[] glowBlueMaterials = null;
        public Material[] glowRedMaterials = null;
        public Material glowBlue = null;
        public Material glowRed = null;


        [Header("Sizes Settings")]
        public bool sizeable = true;
        public int currentSize = 1;


        private Rigidbody boxrig;
        private bool ReadyToFire = false;
        private float currentTime = 0;
        private float delayTime = 3;
        private float change = 0;
        private float rateOfChange = 0;
        [HideInInspector] public bool isChangingSize = false;
        private int sizeDirection = -1;
        private Material originalMaterial = null;
        private Material[] originalMaterials = null;
        private Vector3 updatedLocalScale = Vector3.zero;
        private PlayerInput input = null;
        private GameObject player = null; 
        private GameObject spawner = null;
        private EnemyLife enemyLife = null;
        private Despawn despawn = null;
        private EnemySpawn enemySpawn = null;
        private Animator anim = null;
        private MeshRenderer mesh = null;

        //-----------------------------------------------------------
        // Sets up data and components
        //-----------------------------------------------------------
        private void Start()
        {
            mesh = GetComponent<MeshRenderer>();
            transform.localScale = sizes[currentSize];
            boxrig = GetComponent<Rigidbody>();
            boxrig.mass = masses[currentSize];
            boxrig.angularDrag = angularDrags[currentSize];
            boxrig.drag = drags[currentSize];
            if (transform.tag == "Object")
            {
                originalMaterial = mesh.sharedMaterial;
            }
            else if(transform.tag == "Enemy")
            {
                originalMaterials = new Material[GetComponentsInChildren<MeshRenderer>().Length];
                int i = 0;
                foreach(MeshRenderer ogMat in GetComponentsInChildren<MeshRenderer>())
                {
                    originalMaterials[i] = ogMat.sharedMaterial;
                    i++;
                }
            }
            spawner = GameObject.FindGameObjectWithTag("Spawner");
            enemySpawn = spawner.GetComponent<EnemySpawn>();
            enemyLife = GetComponent<EnemyLife>();
            despawn = GetComponent<Despawn>();
            anim = GetComponent<Animator>();
        }

        //-----------------------------------------------------------
        // Starts changing size
        //-----------------------------------------------------------
        private void Update()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    input = player.GetComponent<PlayerInput>();
                    rateOfChange = input.rateOfChange;
                }
            }
            else
            {
                ChangingSize();
            }
        }

        //-----------------------------------------------------------
        // Lerps through current size to new size and sets if smaller
        //-----------------------------------------------------------
        void ChangingSize()
        {
            
            if (isChangingSize)
            {
                
                if (sizeDirection == 0)
                {
                    updatedLocalScale.x = Mathf.Lerp(sizes[currentSize].x, sizes[currentSize - 1].x, change);
                    updatedLocalScale.y = Mathf.Lerp(sizes[currentSize].y, sizes[currentSize - 1].y, change);
                    updatedLocalScale.z = Mathf.Lerp(sizes[currentSize].z, sizes[currentSize - 1].z, change);
                    transform.localScale = updatedLocalScale;
                    if (transform.tag == "Object")
                    {
                        mesh.sharedMaterial = glowBlue;
                    }
                    else if (transform.tag == "Enemy")
                    {
                        int i = 0;
                        foreach (MeshRenderer ogMat in GetComponentsInChildren<MeshRenderer>())
                        {
                            ogMat.sharedMaterial = glowBlueMaterials[i];
                            i++;
                        }
                    }
                    change += rateOfChange;
                }
                else if (sizeDirection == 1)
                {
                    updatedLocalScale.x = Mathf.Lerp(sizes[currentSize].x, sizes[currentSize + 1].x, change);
                    updatedLocalScale.y = Mathf.Lerp(sizes[currentSize].y, sizes[currentSize + 1].y, change);
                    updatedLocalScale.z = Mathf.Lerp(sizes[currentSize].z, sizes[currentSize + 1].z, change);
                    transform.localScale = updatedLocalScale;
                    if (transform.tag == "Object")
                    {
                        mesh.sharedMaterial = glowRed;
                    }
                    else if (transform.tag == "Enemy")
                    {
                        int i = 0;
                        foreach(MeshRenderer ogMat in GetComponentsInChildren<MeshRenderer>())
                        {
                            ogMat.sharedMaterial = glowRedMaterials[i];
                            i++;
                        }
                    }
                    change += rateOfChange;
                }
                if (change >= 1.1f)
                {
                    if (sizeDirection == 1)
                    {
                        currentSize++;
                    }
                    else if (sizeDirection == 0)
                    {
                        currentSize--;
                    }
                    if (transform.gameObject.tag == "Enemy")
                    {
                        if (currentSize < originalSize)
                        {
                            anim.SetBool("isSmall", true);
                            anim.SetTrigger("Shrunk");
                            if (enemyLife.isWaveTrigger)
                            {
                                enemySpawn.enemyCount--;
                            }
                        }
                        else if (currentSize == originalSize)
                        {
                            anim.SetBool("isSmall", false);
                            despawn.lifeTime = 5;
                            if (enemyLife.isWaveTrigger && sizeDirection == 1)
                            {
                                enemySpawn.enemyCount++;
                            }
                        }

                    }
                    boxrig.mass = masses[currentSize];
                    boxrig.angularDrag = angularDrags[currentSize];
                    boxrig.drag = drags[currentSize];
                    change = 0;
                    sizeDirection = -1;
                    isChangingSize = false;
                    if (transform.tag == "Object")
                    {
                        mesh.sharedMaterial = originalMaterial;
                    }
                    else if (transform.tag == "Enemy")
                    {
                        int i = 0;
                        foreach (MeshRenderer ogMat in GetComponentsInChildren<MeshRenderer>())
                        {
                            ogMat.sharedMaterial = originalMaterials[i];
                            i++;
                        }
                    }

                }
            }
        }

        //-----------------------------------------------------------
        // Turns on change ability to change size
        //-----------------------------------------------------------
        public void ChangeSize(int sizeDirection)
        {
            this.sizeDirection = sizeDirection;
            isChangingSize = true;
            
        }

        //-----------------------------------------------------------
        // Tests if able to change size
        // Return (bool): if able to change size
        //-----------------------------------------------------------
        public bool Sizeable(int sizeDirection)
        {
            if (sizeable)
            {
                if (sizeDirection == 1)
                {
                    if (currentSize != sizes.Length - 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (sizeDirection == 0)
                {
                    if (currentSize != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
