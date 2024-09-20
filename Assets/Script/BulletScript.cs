using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    CharacterMove CharacterMove;
    // Start is called before the first frame update
    void Start()
    {
        CharacterMove = FindObjectOfType<CharacterMove>();
        Vector3 shootingDirection = CharacterMove.GetShootingDirection();
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        gameObject.GetComponent<Rigidbody2D>().velocity = shootingDirection * 10f;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        Destroy(gameObject, 4f);
    }

}
