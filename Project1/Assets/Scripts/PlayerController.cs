using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform attackLocation;
    public float attackRange;
    public LayerMask enemies;

    public float speed;
    public Animator animator;
    private Vector2 direction;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Считывание клавиш движения
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        //Изменение направления спрайта
        if (direction.x<0) {
            Vector3 lTemp = transform.localScale;
            lTemp.x = -1.5f;
            transform.localScale = lTemp;
        }
        else if (direction.x>0){
            Vector3 lTemp = transform.localScale;
            lTemp.x = 1.5f;
            transform.localScale = lTemp;
        }

        //Анимация бега
        animator.SetFloat("Speed", direction.sqrMagnitude);

        //Ближний бой
        if(Input.GetButton("Fire1")){
            animator.SetBool("isAttacking", true);

            //Обработка попадания
            Collider2D[] damage = Physics2D.OverlapCircleAll( attackLocation.position, attackRange, enemies );
              for (int i = 0; i < damage.Length; i++)
                 {
                  Animator anim = damage[i].gameObject.GetComponent<Animator>();
                  anim.SetBool("isDead",true);
                  Destroy( damage[i].gameObject,1f );
                 }
                
        }
        else animator.SetBool("isAttacking", false);
    }
    void FixedUpdate(){

        //Придание ускорения
        rb.AddForce(direction*speed);
    }

}
