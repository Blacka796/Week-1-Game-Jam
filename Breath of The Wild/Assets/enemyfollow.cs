using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public enum EnemyState
{
    idle,
    run
}
public class enemyfollow : MonoBehaviour
{
    public EnemyState CurrentState = EnemyState.idle;
    private Animation ani;
    private Transform player;
    private NavMeshAgent agent;
    public InputManager playr;
    public GameObject life1, life2, life3, life4, life5;
    private int life = 5;

    void Start()
    {
        ani = GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        playr = GameObject.FindWithTag("Player").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        switch (CurrentState)
        {
            case EnemyState.idle:
              
               
                    if (distance > 1&& distance <= 20)
                    {
                    CurrentState = EnemyState.run;
                }
                //ani.Play("idle");
                agent.isStopped = true;
                break;
            case EnemyState.run:
                if (distance > 15)
                {
                    CurrentState = EnemyState.idle;
                }
                //ani.Play("run");
                agent.isStopped = false;
                agent.SetDestination(player.position);
                break;
            

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            life--;
            Life();
        }
    }
    void Life()
    {
        if (life == 5)
        {
            life5.SetActive(true);
            life4.SetActive(true);
            life3.SetActive(true);
            life2.SetActive(true);
            life1.SetActive(true);
        }
        if (life ==4)
        {
            life5.SetActive(false);
            life4.SetActive(true);
            life3.SetActive(true);
            life2.SetActive(true);
            life1.SetActive(true);
        }
        if (life == 3)
        {
            life5.SetActive(false);
            life4.SetActive(false);
            life3.SetActive(true);
            life2.SetActive(true);
            life1.SetActive(true);
        }
        if (life == 2)
        {
            life5.SetActive(false);
            life4.SetActive(false);
            life3.SetActive(false);
            life2.SetActive(true);
            life1.SetActive(true);
        }
        if (life == 1)
        {
            life5.SetActive(false);
            life4.SetActive(false);
            life3.SetActive(false);
            life2.SetActive(false);
            life1.SetActive(true);
        }
        if (life < 1)
        {
            life5.SetActive(false);
            life4.SetActive(false);
            life3.SetActive(false);
            life2.SetActive(false);
            life1.SetActive(false);

            SceneManager.LoadScene("End");
           
        }
    }
}
