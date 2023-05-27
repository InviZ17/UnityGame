using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10; 
    public int health = 10;
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
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        Slider s = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        s.SetValueWithoutNotify(health / maxHealth);
        // s.value = 
    }
}
