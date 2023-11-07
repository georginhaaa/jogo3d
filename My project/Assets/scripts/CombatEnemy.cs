using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class CombatEnemy : MonoBehaviour
{
    [Header("Atributes")]
    public float totalHealth = 100;
    public float attackDamage;
    public float movementSpeed;
    public float lookRadius;
    public float colliderRadius = 2f;

    [Header("Components")]
    private Animator anim;
    private CapsuleCollider capsule;
    private NavMeshAgent agent;

    [Header("Others")] 
    private Transform player;

    private bool attacking;
    private bool walking;
    private bool hitting;

    private bool waitFor;
    
    
    
    
    
    
    
    
    
    
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
                StartCoroutine("Attack");
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
    
    IEnumerator Attack()
    {
        if (!waitFor)
        {
            waitFor = true;
            attacking = true;
            walking = false;
            anim.SetBool("Walk Forward", false);
            anim.SetBool("Bite Attack", true);
            yield return new WaitForSeconds(1.2f);
            GetPlayer(); 
            //yield return new WaitForSeconds(1f);
            waitFor = false;
        }
    }

    void GetPlayer()
    {
       
        foreach (Collider c in Physics.OverlapSphere((transform.position + transform.forward * colliderRadius), colliderRadius))
        {
            if (c.gameObject.CompareTag("Player"))
            {
              //aplicar dano no player 
              Debug.Log("Bateu no Player");
            }
        }
    }

    public void GetHit(float damage)
    {
        totalHealth -= damage;

        if (totalHealth > 0)
        {
            // inimigo está vivo
            StopCoroutine("Attack");
            anim.SetTrigger("Take Damage");
            hitting = true;
            

        }
        else
        {
            //inimigo morre
            anim.SetTrigger("Die");
        }
    }

    IEnumerator RecoveryFromHit()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Walk Forward", false);
        anim.SetBool("Bite Attack", false);
        hitting = false;
        waitFor = false;

    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        
    }
}
