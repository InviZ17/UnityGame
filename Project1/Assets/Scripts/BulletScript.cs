using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject hitEffect;
    private GameObject pTemp;
    // Start is called before the first frame update
    void Start()
    {
        pTemp=GameObject.Find("Player");
        Debug.Log(pTemp);
    }



    void OnCollisionEnter2D(Collision2D other){
        if(pTemp.GetInstanceID()==other.gameObject.GetInstanceID()){
         Physics2D.IgnoreCollision(GetComponent<Collider2D>(), pTemp.GetComponent<Collider2D>());
        }
        else {
            if (other.gameObject.layer == 6){
            other.gameObject.GetComponent<Animator>().SetBool("isDead",true);
            Destroy(other.gameObject,1f);
            }
            GameObject effect = Instantiate(hitEffect,transform.position,transform.rotation);
            Destroy(effect,1f);
            Destroy(gameObject);

        }

    }
}
