using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    public int EnemyMaxHp { get; set; }
    public int EnemyHp { get; set; }
    public int EnemyDamage { get; set; }
    public int EnemySpeed { get; set; }
    float distancetoPlayer;
    float chaseDistance = 7f;
    float attackDistance = 1f;
    float roamingDistance = 10f;
    float roamRadius = 10f;
    Vector2 roamPoint;

    GameObject player;
    Vector2 vector;
    Animator animator;


    ZombieState currentState = ZombieState.Roaming;
    public enum ZombieState{
        Roaming,
        Chasing,
        Attacking,
        }

    
    // Start is called before the first frame update
    void Start()
    {
        EnemyHp = EnemyMaxHp;
        EnemyDamage = 25;
        EnemySpeed = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorSetData();
     distancetoPlayer = Vector2.Distance(transform.position, player.transform.position);

        SwitchState(distancetoPlayer);
        //Debug.Log(distancetoPlayer);
        switch (currentState) {
   
            case ZombieState.Roaming:
                ZombieRoaming();
                break
                    ;
                case ZombieState.Attacking:
                ZombieAttacking();
                break;
            case ZombieState.Chasing:
                ZombieChasing();
                break;
        }

    }

    void SwitchState(float DistanceToPlayer)
    {
        Debug.Log(currentState);
        if (distancetoPlayer < attackDistance)
        {
            currentState = ZombieState.Attacking;
        }
        else if (distancetoPlayer < chaseDistance)
        {
            currentState = ZombieState.Chasing;

        }
        else
        {
            currentState = ZombieState.Roaming;
        }
    }



    private void ZombieRoaming()
    {
        transform.position = Vector2.MoveTowards(transform.position, roamPoint, EnemySpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, roamPoint) < 0.1f)
            roamPoint = GetRandomPoint();
    }

    private void ZombieAttacking()
    {
        Debug.Log("attack");
    }

    private void ZombieChasing()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.transform.position, EnemySpeed * Time.deltaTime);

    }

    Vector2 GetRandomPoint()
    {
        return (Vector2)transform.position + Random.insideUnitCircle * roamRadius;
    }

    void AnimatorSetData()
    {
        
        animator.SetFloat("eX", roamPoint.x);
        animator.SetFloat("eY", roamPoint.y);
    }

}


