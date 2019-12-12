/*----------------------------------------
File Name: EnemyAI.cs
Purpose: Controls navmesh based on animator
Author: Tarn Cooper
Modified: 31/10/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Chase Settings")]
    public float chaseRange = 10;
    public float chaseAngle = 45;

    [Header("Swarm Settings")]
    public float swarmRange = 10;

    [Header("Flee Settings")]
    public float fleeRange = 5;

    [Header("Attack Settings")]
    public float attackRange = 5;
    public float attackAngle = 45;
    public bool isRanged = false;

    [Header("Throw Settings")]
    public float force = 10;
    public float lengthInFront = 5;
    public GameObject thrownObjectPrefab = null;

    [Header("Alert Settings")]
    public bool alerted = false;
    public Transform alertedTarget;

    [Header("Patrol Settings")]
    public bool onPartrol = false;
    public Transform[] patrolPoints;
    public int targetPoint = 0;

    private float range = 0;
    private float angle = 0;
    private bool inSight = false;
    private GameObject thrownObject;
    private RaycastHit hit;
    private GameObject player;
    private Animator anim;
    private NavMeshAgent agent;
    public bool swarm = false;

    //-----------------------------------------------------------
    // Gets Components
    //-----------------------------------------------------------
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    //-----------------------------------------------------------
    // Changes behaviour of navmesh agent depending on animator 
    //  state
    //-----------------------------------------------------------
    void Update()
    {
        GetData();
        anim.SetBool("isPlayerSeen", false);
        if (swarm)
        {
            anim.SetBool("isPlayerSeen", true);
        }
        else
        {
            anim.SetBool("isPlayerSeen", range <= chaseRange && angle <= chaseAngle && inSight);
        }
        anim.SetBool("isAttackingPlayer", (range <= attackRange && angle <= attackAngle && inSight));
        anim.SetBool("isPartrolling", onPartrol);
        anim.SetBool("isAlerted", alerted);
        alerted = false;
        anim.SetBool("isRanged", isRanged);

        if (agent.enabled)
        {
            
            anim.SetBool("isStopped", agent.isStopped);
            agent.isStopped = true;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Chase"))
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (enemy == gameObject)
                    {
                        continue;
                    }
                    else if (Vector3.Distance(transform.position, enemy.transform.position) <= swarmRange)
                    {
                        enemy.GetComponent<EnemyAI>().swarm = true;
                    }
                    else
                    {
                        enemy.GetComponent<EnemyAI>().swarm = false;
                    }
                }
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack "))
            {
                foreach (BoxCollider box in GetComponents<BoxCollider>())
                {
                    box.enabled = false;
                }
            }
            else
            {
                foreach (BoxCollider box in GetComponents<BoxCollider>())
                {
                    box.enabled = true;
                }

            }

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Flee"))
            {
                agent.isStopped = false;
                agent.SetDestination(transform.position - (player.transform.position - transform.position).normalized * fleeRange);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Patrol"))
            {
                agent.isStopped = false;

                agent.SetDestination(patrolPoints[targetPoint].position);
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    targetPoint++;
                }
                if (targetPoint == patrolPoints.Length)
                {
                    targetPoint = 0;
                }
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Investigate"))
            {
                agent.isStopped = false;
                if (alertedTarget != null)
                {
                    agent.SetDestination(alertedTarget.transform.position);
                    alertedTarget = null;
                }
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Throw"))
            {
                
                if (thrownObject == null)
                {
                    thrownObject = Instantiate(thrownObjectPrefab, transform);
                    thrownObject.transform.position += (player.transform.position - transform.position).normalized * lengthInFront;
                    thrownObject.transform.Rotate(Vector3.forward, 90);
                    thrownObject.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * force, ForceMode.Impulse);
                    thrownObject.transform.parent = null;
                }
            }
            else
            {
                thrownObject = null;
            }
        }
    }

    //-----------------------------------------------------------
    // Gets data based on player position
    //-----------------------------------------------------------
    void GetData()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            range = Vector3.Distance(transform.position, player.transform.position);
            angle = Vector3.Angle((player.transform.position - transform.position).normalized, transform.forward.normalized);
            if (range <= chaseRange && angle <= chaseAngle)
            {
                if (Physics.Raycast(new Ray(transform.position, (player.transform.position - transform.position).normalized), out hit, range + 10, ~(1 << 16)))
                {

                    if (hit.collider.gameObject.tag == "Player")
                    {
                        inSight = true;
                        return;
                    }
                }
            }
            inSight = false;
        }
    }
}
