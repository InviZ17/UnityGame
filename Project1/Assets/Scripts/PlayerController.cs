using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform attackLocation;
    public float attackRange;
    public LayerMask enemies;

    public Animator animator;
    private Vector2 _direction;
    public Vector2 Direction { get; set; }

    private Rigidbody2D _rb;

    public Rigidbody2D Rb
    {
        get
        {
            return _rb;
        }
        set
        {
            _rb = value;
        }
    }

    private AbilityHolder _ah;

    private bool meleeAttack;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ah = GetComponent<AbilityHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        // Изменение направления спрайта
        if (_direction.x<0) {
            Vector3 lTemp = transform.localScale;
            lTemp.x = -1.5f;
            transform.localScale = lTemp;
        }
        else if (_direction.x>0){
            Vector3 lTemp = transform.localScale;
            lTemp.x = 1.5f;
            transform.localScale = lTemp;
        }

        // Анимация бега
        animator.SetFloat("Speed", _direction.sqrMagnitude);

       
        // Ближний бой
        if (meleeAttack)
        {
            animator.SetBool("isAttacking", true);
            MeleeAttack();
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }
    void FixedUpdate(){

        // Придание ускорения

        CharacterStats cs = GetComponent<CharacterStats>();
        float speed = cs.GetStatValueByName("Movement Speed");
        float speedNormalized = speed / 60;
        _rb.AddForce(_direction*speedNormalized);
    }

    public void Move(InputAction.CallbackContext context)
    {
        // Считывание клавиш движения
        _direction = context.ReadValue<Vector2>();
    }


    private void MeleeAttack()
    {

        // Обработка попадания
        Collider2D[] damage = Physics2D.OverlapCircleAll(attackLocation.position, attackRange, enemies);
        for (int i = 0; i < damage.Length; i++)
        {
            Animator anim = damage[i].gameObject.GetComponent<Animator>();
            anim.SetBool("isDead", true);
            Destroy(damage[i].gameObject, 1f);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            meleeAttack = true;

        }
        else if (context.canceled)
        {
            meleeAttack = false;
        }
    }

    public void UseAbility(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("ability used");
            _ah.AbilityUsing = true;

        }
        else if (context.canceled)
        {
            _ah.AbilityUsing = false;
        }
    }


}
