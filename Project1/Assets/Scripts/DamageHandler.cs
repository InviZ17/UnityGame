using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public float KnockoutTime = 0f;
    public float health = 3f;

    void Update()
    {
        if (health<=0){
            Destroy(gameObject);
        }
    }
    public void Knockout(float knock){
        KnockoutTime = Time.time + knock;
    }
    public void TakeDamage(float damage,float Knockback,Vector2 KnockbackDir){
        //this.GetComponent<SpriteRenderer>().color +=new Color(0.1f,0f,0f); 
        health -= damage;
        
        this.GetComponent<Rigidbody2D>().AddForce(KnockbackDir*Knockback);
    }
}
