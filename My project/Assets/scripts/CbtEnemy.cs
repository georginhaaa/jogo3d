using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CbtEnemy : MonoBehaviour
{
    [Header("Atributtes")]
    public float totalHealth;
    public float attackDamage;
    public float movementSpeed;
    public float lookRadius;

    [Header("Components")]
    private Animator anim;
    private CapsuleCollider capsule;
    private NavMeshAgent agent;

    [Header("Others")] 
    private Transform player;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, transform.position);
        
            if (distance <= lookRadius)
            {
                Debug.Log ("Dentro do raio");
                agent.SetDestination(player.position);
                anim.SetBool("Walk Forward", true);
            }
            if(distance <= agent.stoppingDistance)
            {
                
            }
            else
            {
                Debug.Log ("Fora do raio");
                anim.SetBool("Walk Forward", false);
            }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
