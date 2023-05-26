using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public int exits = 4;
    public int routes = 0;
    public bool startRoom = false;
    private float distance;
    private GameLogic Logic;
    public GameObject[] rooms;
    public GameObject roomLayout;

    void Start(){
        if (!startRoom){
            roomLayout = Instantiate(rooms[Random.Range(0,rooms.Length)], this.transform.position, this.transform.rotation, this.transform);
        }
        Logic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        distance = Vector2.Distance(transform.position, new Vector3(0f,0f,0f));
        if (Logic.maxDist < distance){
            Logic.maxDist = distance;
            Logic.farestRoom = this.gameObject;
        }
    }
}
