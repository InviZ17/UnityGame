using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private float distance;
    public float attackCooldown = 1f;
    private float previousAttackTime = 0f;
    //private GameObject pTemp;
    private Rigidbody2D rb;
    private DamageHandler Dh;
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Dh = GetComponent<DamageHandler>();
    }


    
    void FixedUpdate()
    {
        GetComponent<Animator>().SetBool("attacking", false);
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        if (Time.time >= Dh.KnockoutTime)
        rb.MovePosition(Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime));
    }
    void OnCollisionStay2D(Collision2D other){
        if(player.GetInstanceID()==other.gameObject.GetInstanceID()){
            if (Time.time >= previousAttackTime+attackCooldown && Time.time >= Dh.KnockoutTime){
                previousAttackTime = Time.time;
                GetComponent<Animator>().SetBool("attacking", true);
                other.gameObject.GetComponent<PlayerHealth>().takeDamage(1);
            }
            
        }
    }
}
