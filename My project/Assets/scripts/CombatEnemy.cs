using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class CombatEnemy : MonoBehaviour
{
    [Header("Atributes")]
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

    private bool attacking;
    private bool walking;
    
    
    
    
    
    
    
    
    
    
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
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= lookRadius)
        {
            //dentro do raio
            agent.isStopped = false;
            
            if (!attacking)
            {
                
                agent.SetDestination(player.position);
                anim.SetBool("Walk Forward", true);
                walking = true;
            }
            
            
            
            if (distance <= agent.stoppingDistance)
            {
                // o player esta dentro do raio de ataque
                // metodo de ataque
                agent.isStopped = true;
                Debug.Log("Atacar");
            }
            else
            {
                attacking = false;
            }
        }
        else
        {
           //fora do raio
           agent.isStopped = true;
           anim.SetBool("Walk Forward", false);
           walking = false;
           attacking = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        
    }
}
