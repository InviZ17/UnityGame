using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDestroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Spawnpoint")){
        Destroy(other.gameObject);
        }
    }
}
