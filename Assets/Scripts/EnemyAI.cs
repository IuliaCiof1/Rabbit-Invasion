using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public static Action<float> Attack;

    NavMeshAgent agent;
    [SerializeField] float damage;
    [SerializeField] Transform player;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;

    //Patrolling
    [SerializeField] Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    [SerializeField] float sightRange, attackRange;
    [SerializeField] bool playerInSightRange, playerInAttackRange;
    bool alreadyAttacked;
    //public GameObject projectile;
    Animator animator;
    




    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.enabled = true; //seted it false in preset //debugs "Failed to create agent because it is not close enough to the NavMesh" error
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(player.name);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.destination=walkPoint;
        
        animator.SetBool("Walk", true);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        Debug.Log("patroll")
            ;
        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            Debug.Log("reached walkpoint");
            animator.SetBool("Walk", false);
            walkPointSet = false;

        }
        Debug.Log(agent.isPathStale);
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        animator.SetBool("Walk", true);
        animator.SetBool("Attack", false);
        Debug.Log(player.position);
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        animator.SetBool("Walk", false);
        transform.LookAt(player);
        animator.SetBool("Attack", false);
        if (!alreadyAttacked)
        {
            /////Attack code here
            //Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            /////End of attack code

            animator.SetBool("Attack", true);


            Attack?.Invoke(damage);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //public void TakeDamage(int damage)
    //{
    //    health -= damage;

    //    if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    //}
    private void DestroyEnemy()
    {
        animator.SetBool("Death", true);

        //Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
