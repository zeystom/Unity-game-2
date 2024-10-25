using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    CharacterAttack attack;
    CharacterActions CharacterMove;
    // Start is called before the first frame update
    void Start()
    {
        attack = FindObjectOfType<CharacterAttack>();
        CharacterMove = FindObjectOfType<CharacterActions>();
        Vector3 shootingDirection = CharacterMove.GetShootingDirection();
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        gameObject.GetComponent<Rigidbody2D>().velocity = shootingDirection * attack.HandleChangeGun().AttackSpeed;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        Destroy(gameObject, 8f);


    
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            var EMINEM = collision.gameObject.GetComponent<EnemyScript>();
            EMINEM.EnemyHp -= attack.HandleChangeGun().Damage;
            Destroy(gameObject);
        
        }
    }

}
