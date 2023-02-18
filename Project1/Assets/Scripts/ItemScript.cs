using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private GameObject pTemp;

    public float health = 0f;
    public float dashDistance = 0f;
    public float attackRadius = 0f;
    public float damage = 0f;
    public float knockback = 0f;
    public float knockoutDuration = 0f;
    public float attackRange = 0f;
    public float attackRate = 0f;
    public float attackDuration = 0f;
    public float speed = 0f;
    PlayerMelee pMelee;

    void Start(){
        pTemp = GameObject.Find("Player");
        pMelee = pTemp.GetComponent<PlayerMelee>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if (pTemp.GetInstanceID()==other.gameObject.GetInstanceID()){
            pMelee.dashDistance += pMelee.dashDistance*dashDistance/100f;
            pMelee.attackRadius += pMelee.attackRadius*attackRadius/100f;
            pMelee.damage += pMelee.damage*damage/100f;
            pMelee.knockback += pMelee.knockback*knockback/100f;
            pMelee.knockoutDuration += pMelee.knockoutDuration*knockoutDuration/100f;
            pMelee.attackRange += pMelee.attackRange*attackRange/100f;
            pMelee.attackRate += pMelee.attackRate*attackRate/100f;
            pMelee.attackDuration += pMelee.attackDuration*attackDuration/100f;
            pTemp.GetComponent<PlayerMovement>().speed += pTemp.GetComponent<PlayerMovement>().speed*speed/100f;
            Destroy(gameObject);
        }

    }
}
