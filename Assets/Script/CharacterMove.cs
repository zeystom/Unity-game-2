using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;
    CharacterController controller;
    Vector2 moveDirection;
    float moveX;
    float moveY;
    Animator animator;
    Vector3 mouseDirection;
    float idleMoveX;
    float idleMoveY;
    public GameObject bulletPref;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2 (moveX, moveY).normalized;
        if(moveX !=0 || moveY != 0)
        {
            Animate(moveX, moveY, 1);
            idleMoveX = moveX;
            idleMoveY = moveY;
        }
        else
        {
            Animate(idleMoveX, idleMoveY, 0);
        }
    }

    private void FixedUpdate()
    {

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Animate(float moveX, float moveY, int layer)
    {
        switch (layer)
        {
            case 0: 
                animator.SetLayerWeight(0, 1);
                animator.SetLayerWeight(1, 0);
            break;

            case 1:
                animator.SetLayerWeight(1, 1);
                animator.SetLayerWeight(0, 0);
                break;
        }
        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);
        Attack();

    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsAttack", true);
            animator.SetFloat("MoveX", idleMoveY);
            animator.SetFloat("MoveY", idleMoveX);
            mouseDirection = Input.mousePosition;
            mouseDirection = Camera.main.ScreenToWorldPoint(mouseDirection);
            mouseDirection.z = 0f;
            mouseDirection = mouseDirection - transform.position;
            GameObject bulletInstance = Instantiate(bulletPref, transform.position, Quaternion.identity);
            bulletInstance.GetComponent<Rigidbody>().velocity = mouseDirection * 5f;
            Destroy(bulletInstance, 4);
            
        }
        else
        {
            animator.SetBool("IsAttack", false);


        }
    }
}
