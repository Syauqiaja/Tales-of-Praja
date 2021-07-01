using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float healthPoint = 100f;
    public float sightRadius = 7f;
    
    [SerializeField] private Animator animator;
    private Rigidbody rigidbody;
    private NavMeshAgent agent;
    private bool isFollowing = false;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Hitted(float damage){
        healthPoint -= damage;
        rigidbody.AddRelativeForce(-2f * Vector3.forward, ForceMode.Impulse);
        animator.SetTrigger("Hitted");
    }
    private void Update() {
        Vector3 destination = PlayerControl.Instance.playerTransform.position;
        if((transform.position - destination).magnitude <= sightRadius){
            isFollowing = true;
        }
        if(isFollowing){
            agent.SetDestination(destination);
        }
         if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    animator.SetBool("IsFollowing", false);
                } else animator.SetBool("IsFollowing", true);
            }else animator.SetBool("IsFollowing", true);
        }else animator.SetBool("IsFollowing", true);
    }
    
}
