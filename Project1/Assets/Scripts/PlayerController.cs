using System;
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
    public event Action OnUpdate;

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
        OnUpdate?.Invoke();

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

        AbstractStats cs = GetComponent<AbstractStats>();
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
            Damageable d = damage[i].gameObject.GetComponent<Damageable>();
            
            // TODO: дописать формулу расчета урона с руки, пока это 1=1 от физ силы
            float meleeDamage = d.GetStats().GetStatValueByName("Physical Strength");

            // TODO: дописать формулу расчета отдачи от оружия
            float knockbackForce = 0.01f;

            Vector2 direction = (damage[i].gameObject.transform.position - _rb.transform.position).normalized;

            Damage dmg = new Damage(meleeDamage, direction, knockbackForce, Damage.Type.Physical);

            d.TakeDamage(dmg);
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

    

    private void HandleActiveAbility(InputAction.CallbackContext context, int abilityIndex)
    {
        if (context.started)
        {
            _ah.SetAbilityUsing(abilityIndex, Ability.SkillStatusUsing);
        }
        else if (context.canceled)
        {
            _ah.SetAbilityUsing(abilityIndex, Ability.SkillStatusNotUsing);
        }
    }

    public void UseAbility(InputAction.CallbackContext context)
    {
        HandleActiveAbility(context, Ability.SkillIndexDash);
    }

    public void UseActiveAbility1(InputAction.CallbackContext context)
    {
        HandleActiveAbility(context, Ability.SkillIndexFirst);
    }

    public void UseActiveAbility2(InputAction.CallbackContext context)
    {
        HandleActiveAbility(context, Ability.SkillIndexSecond);
    }

    public void UseActiveAbility3(InputAction.CallbackContext context)
    {
        HandleActiveAbility(context, Ability.SkillIndexThird);
    }

    public void UseActiveAbility4(InputAction.CallbackContext context)
    {
        HandleActiveAbility(context, Ability.SkillIndexFourth);
    }

}
