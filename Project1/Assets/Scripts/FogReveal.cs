using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogReveal : MonoBehaviour
{
    public GameObject center;
    private bool spawned = false;
    public bool debugReveal = true;
    public bool debugSpawn = true;
    private GameObject EnemySpawner;
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    void Update(){
        if (debugReveal){
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player") && !debugReveal){
            if (!spawned && !debugSpawn){
                EnemySpawner = center.GetComponent<RoomHandler>().roomLayout;
                EnemySpawner.GetComponent<EnemySpawner>().Spawn();
                spawned = true;
            }
        }
    }
    void OnTriggerStay2D(Collider2D other){
        if (other.CompareTag("Player") && !debugReveal){
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player") && !debugReveal){
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
