using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawner : MonoBehaviour
{
    public GameObject room1;
    public bool init = false;
    public bool spawned = false;
    public GameObject wall1;
    public int direction;
    public bool needWall=false;
    public float roomOfs;
    public GameObject center;
    public EnemySpawner enemySpawner;

    private int opened;
    private GameObject room;
    private GameObject wall;
    private GameLogic Logic;
    private Vector3 roomOffset;
    private Vector3 playerOffset;
    private Vector3 rot;


    void Start(){
        Logic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        room = Logic.room;
        wall = Logic.wall;
        roomOfs = Logic.roomOffset;
        float rand = Random.Range(20,30)/100f;
        if (direction == 1){
                roomOffset = new Vector3(0f,roomOfs,0f);
                playerOffset = new Vector3(0f,2f,0f);
                rot = new Vector3(0f,0f,0f);
            }
            else if (direction == 2){
                roomOffset = new Vector3(0f,-roomOfs,0f);
                playerOffset = new Vector3(0f,-2f,0f);
                rot = new Vector3(0f,0f,180f);
            }
            else if (direction == 3){
                roomOffset = new Vector3(-roomOfs,0f,0f);
                playerOffset = new Vector3(-2f,0f,0f);
                rot = new Vector3(0f,0f,90f);
            }
            else if (direction == 4){
                roomOffset = new Vector3(roomOfs,0f,0f);
                playerOffset = new Vector3(2f,0f,0f);
                rot = new Vector3(0f,0f,270f);
            }
        Invoke("Spawn", rand);
        //Invoke("BugFixer", 2f);
    }


    void Spawn(){ 
        if (!init){
            Collider2D[] check = Physics2D.OverlapCircleAll( this.transform.position + roomOffset, 0.01f);
                for (int i = 0; i<check.Length;i++){
                    if (check[i].CompareTag("Center")){
                        init = true;
                        return;
                    }
                }
            if ((center.GetComponent<RoomHandler>().exits == 1 && Logic.roomNumber < Logic.maxRooms)
                || (center.GetComponent<RoomHandler>().exits - center.GetComponent<RoomHandler>().routes == 1 && Logic.roomNumber < Logic.maxRooms)){
                opened = 1;
            }
            else if (Logic.roomNumber < Logic.maxRooms){
            opened = Random.Range(0, 2);
            }
            else {
                opened = 0;
            }
            //if (opened == 1) {
                
                if (!init && opened==1){
                    room1 = Instantiate(room, this.transform.position + roomOffset, room.transform.rotation);
                    Logic.roomNumber +=1;
                    spawned = true;
                    center.GetComponent<RoomHandler>().exits -=1;
                    center.GetComponent<RoomHandler>().routes +=1;
                }
            //}
            else {
                needWall = true;
            }
            init = true;
            
        }

    }

    // void BugFixer(){
    //     Collider2D[] collide = Physics2D.OverlapCircleAll( transform.position, 0.05f);
    //     int i;
    //     for (i = 0; i<collide.Length; i++){
    //         if (collide[i].gameObject.CompareTag("Door") || collide[i].gameObject.CompareTag("TriggerWall")){
    //             if (collide[i].gameObject.CompareTag("Door") && wall1!=null){
    //                 center.GetComponent<RoomHandler>().routes +=1;
    //                 Destroy(wall1);
    //             }
    //             return;
    //         }
    //     }
    //     if (collide.Length >0){
    //     needWall = true;
    //     }
    // }
    void Update(){
        if (needWall && wall1 == null){
            center.GetComponent<RoomHandler>().exits -=1;

            wall1 = Instantiate(wall, transform.position, wall.transform.rotation,this.gameObject.transform);
            wall1.transform.Rotate(rot);
        }
    }


    void OnTriggerEnter2D(Collider2D other){
        enemySpawner = center.GetComponent<RoomHandler>().roomLayout.GetComponent<EnemySpawner>();
        if (!init){
            if (other.CompareTag("Door") && other.GetComponent<DoorSpawner>().spawned == true){
                    init = true;
                    center.GetComponent<RoomHandler>().exits -=1;
                    center.GetComponent<RoomHandler>().routes +=1;
                }

            if (other.CompareTag("TriggerWall")){
                init = true;
                needWall = true;
            }
        }
        
        if (other.CompareTag("Player") && !needWall && enemySpawner.enemies.Count == 0){
            GameObject.FindGameObjectWithTag("MainCamera").transform.position += roomOffset*2-playerOffset/3f;
            other.transform.position += playerOffset;
        }
    }
}
