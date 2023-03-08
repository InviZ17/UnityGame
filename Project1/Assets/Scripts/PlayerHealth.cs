using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 4;
    public float invul =1f;
    private float nextInvul;

    public void takeDamage(int damage){

        if (Time.time > nextInvul){
            nextInvul = Time.time + invul;
            health -=damage;
        }
    }

    void FixedUpdate(){
        if (health<=0){
            Destroy(this.gameObject);
        }
    }
}
