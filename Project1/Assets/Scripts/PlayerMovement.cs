using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private Animator animator;
    private Vector2 direction;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Считывание клавиш движения
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        //Анимация бега
        animator.SetFloat("Speed", direction.sqrMagnitude);

    }
    void FixedUpdate(){
        //Придание ускорения
        rb.AddForce(direction*speed);
    }

}
