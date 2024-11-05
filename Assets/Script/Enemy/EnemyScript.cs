using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    public int EnemyMaxHp;
    public int EnemyHp;
    public int EnemyDamage;
    public int EnemySpeed;
    float distancetoPlayer;
    float chaseDistance = 9f;
    public float attackDistance = 1f;
    float roamingDistance = 15f;
    float roamRadius = 10f;
    Vector2 roamPoint;


    bool isAttacking;


    CharacterStats characterStats;

    GameObject player;
    Vector2 vector;
    Animator animator;

    bool deathBlock = false;

   public List<GameObject> items;

    ZombieState currentState = ZombieState.Roaming;
    public enum ZombieState{
        Roaming,
        Chasing,
        Attacking,
        Death
        }

    
    // Start is called before the first frame update
    void Start()
    {

        characterStats =FindObjectOfType<CharacterStats>();
        EnemyHp = EnemyMaxHp;

        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
     distancetoPlayer = Vector2.Distance(transform.position, player.transform.position);

        SwitchState(distancetoPlayer);
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
            case ZombieState.Death:
                ZombieDying();
                break;

        }

    }



    public void ZombieDying()
    {
  
        if (!deathBlock)
        {
            animator.SetTrigger("deathTrigger");
            deathBlock = true;
        }

        }

    void Destroy()
    {
        DropGoods();
        Destroy(gameObject);
        EnemySpawner.enemyCounter += 1;   
    }

    void SwitchState(float DistanceToPlayer)
    {
        Debug.Log(currentState);

        if (EnemyHp <= 0)
        {
            currentState = ZombieState.Death;
            return;
        }       
        else if (distancetoPlayer < attackDistance)
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
        vector = (Vector3)roamPoint - gameObject.transform.position;
        AnimatorSetData(vector);
        if (Vector2.Distance(transform.position, roamPoint) < 0.1f)
            roamPoint = GetRandomPoint(transform.position, roamRadius);
    }

    private void ZombieAttacking()
    {
        animator.SetBool("isAttackZomb", true);

    }

    private void ZombieChasing()
    {
        animator.SetBool("isAttackZomb", false);
        vector = player.transform.position - gameObject.transform.position;
        AnimatorSetData(vector);
        transform.position = Vector2.MoveTowards(transform.position,player.transform.position, EnemySpeed * Time.deltaTime);

    }

    public Vector3 GetRandomPoint(Vector3 transpos, float radius)
    {
        return transpos + (Random.insideUnitSphere * radius);
    }

    void AnimatorSetData(Vector3 vector)
    {
        
        animator.SetFloat("eX", vector.x);
        animator.SetFloat("eY", vector.y);
    }


    public void Attack()
    {
        if (!isAttacking)
        {
            EnemyDamage = Random.Range(5, 10);
            isAttacking = true;
            if (characterStats.Armor >= EnemyDamage)
            {
                characterStats.Armor -= EnemyDamage;
            }
            else
            {
                characterStats.Hp -= Math.Abs(characterStats.Armor - EnemyDamage);
                characterStats.Armor = 0;
            }

            
            if (characterStats.Hp <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }


    public void EndAttack()
    {
        isAttacking = false;
    }
    
    void DropGoods()
    {
        var randomCount = Random.Range(1, 3);
        for (int i = 0; i < randomCount; i++) 
        {
            var randomItem = Random.Range(0, items.Count);

            var Randomchik = Random.Range(1, 5);

            Instantiate(items[randomItem], GetRandomPoint(transform.position,2)  , Quaternion.identity);

        }

    }

}


