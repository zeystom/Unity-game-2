using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;

    CharacterController controller;
    public Vector2 moveDirection { get; set; }
    public Vector3 mouseDirection;

    public float moveX { get; set; } 
    public float moveY { get; set; }



    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovementInput();
        
    }

    void FixedUpdate()
    {
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }


    public Vector3 GetShootingDirection()
    {
        mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDirection.z = 0f;
        return (mouseDirection - transform.position).normalized;
    }

    void HandleMovementInput()

    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

  
}