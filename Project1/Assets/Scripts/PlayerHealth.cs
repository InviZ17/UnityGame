using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 4;
    public float invul =1f;
    private float nextInvul;
    public bool invulnerability = true;

    public void takeDamage(int damage){
        if (!invulnerability){
            if (Time.time > nextInvul){
                nextInvul = Time.time + invul;
                health -=damage;
        }
        }
        
    }

    void FixedUpdate(){
        if (health<=0){
            Destroy(this.gameObject);
        }
    }
}
