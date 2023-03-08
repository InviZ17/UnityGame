using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogReveal : MonoBehaviour
{

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
