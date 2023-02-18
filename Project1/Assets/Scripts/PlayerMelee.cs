using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public float dashDistance = 0.3f;
    public float damage = 1f;
    public float knockback = 1f;
    public float knockoutDuration = 1f;
    public Transform attackLocation;
    public float attackRange;
    public LayerMask enemies;
    public float attackRate = 2f;
    public float attackDuration = 0.5f;
    float nextAttackDuration = 0f;
    float nextAttackTime = 0f;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isAttacking=false;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public static Vector3 GetMouseWorldPosition(){
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition,Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(){
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera){
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera){
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    void Update()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 attackDir = (mousePosition - transform.position).normalized;
        if (Time.time >= nextAttackDuration |  !isAttacking){
            isAttacking = false;
        if (Time.time >= nextAttackTime ){
        if (Input.GetButton("Fire1")){
            nextAttackTime = Time.time + 1f/attackRate;
            rb.AddForce(attackDir * 100*dashDistance);
            //transform.position += attackDir * dashDistance;
            animator.SetBool("isAttacking", true);
            attackLocation.position = transform.position+0.3f*attackRange*attackDir;
            nextAttackDuration = Time.time + attackDuration;
            isAttacking = true;
            
                
        }
        }
        else {
            animator.SetBool("isAttacking", false);
            //attackLocation.position = transform.position+0.1f*attackRange*attackDir;
            attackLocation.position = transform.position;
        }
        }

        //Обработка попадания
        if (isAttacking){
        Collider2D[] damageC = Physics2D.OverlapCircleAll( attackLocation.position, 0.05f*attackRange, enemies );
        if (damageC.Length>0){
              for (int i = 0; i < damageC.Length; i++)
                 {
                  //Animator anim = damage[i].gameObject.GetComponent<Animator>();
                  //anim.SetBool("isDead",true);
                  //Destroy( damageC[i].gameObject);
                  damageC[i].GetComponent<DamageHandler>().TakeDamage(damage,20*knockback,attackDir);
                  damageC[i].GetComponent<DamageHandler>().Knockout(knockoutDuration);
                 }
        isAttacking = false;
        }
        }
    }
}
