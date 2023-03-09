using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public int exits = 4;
    public int routes = 0;
    private float distance;
    private GameLogic Logic;

    void Start(){
        Logic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        distance = Vector2.Distance(transform.position, new Vector3(0f,0f,0f));
        if (Logic.maxDist < distance){
            Logic.maxDist = distance;
            Logic.farestRoom = this.gameObject;
        }
    }
}
