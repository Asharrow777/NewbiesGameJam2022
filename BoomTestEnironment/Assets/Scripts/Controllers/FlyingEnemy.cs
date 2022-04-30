using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemy : MonoBehaviour
{
    public float lookRadius = 10f;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask targetLayers;
    public int attackdamage = 10;
    public float attackspeed = 1f;
    public float attackCooldown = 0f;
    public float attackDelay = 0.6f;

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.Player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        attackCooldown -= Time.deltaTime;

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            //Debug.Log("distance: " + distance);
            //Debug.Log("agent.stoppingDistance: " + agent.stoppingDistance);

            if ((int)distance <= agent.stoppingDistance)
            {
                //Debug.Log("in range of target");
                //attack target
                if (attackCooldown <= 0f)
                {
                    AttackTarget();
                    attackCooldown = 1f / attackspeed;
                }

                //Face target
                FaceTarget();
            }
        }
    }


    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void AttackTarget()
    {

        Debug.Log(" Attack Target");
        //play attack animation
        //deal damage to player
        Collider[] hitTargets = Physics.OverlapSphere(attackPoint.position, attackRange, targetLayers);
        //attack timer

        foreach (Collider target in hitTargets)
        {
            Debug.Log(" Player detected");
            StartCoroutine(DoDamage(attackDelay));

        }
    }



    void RangedAttackTarget()
    {

        Debug.Log(" Attack Target");
        //play attack animation
        //deal damage to player
        Collider[] hitTargets = Physics.OverlapSphere(attackPoint.position, attackRange, targetLayers);
        //attack timer

        foreach (Collider target in hitTargets)
        {
            Debug.Log(" Player detected");
            StartCoroutine(DoDamage(attackDelay));

        }
    }

    IEnumerator DoDamage(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        target.GetComponent<targetdummyhealth>().TakeDamage(attackdamage);

    }
}
