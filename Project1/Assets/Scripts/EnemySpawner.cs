using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawns;
    public GameObject ItemSpawn;
    public GameObject center;
    public bool disableSpawn = false;
    public List<GameObject> enemies;
    public GameObject enemy;
    public GameObject item;
    public int maxEnemy;
    private GameLogic Logic;
    void Start()
    {
        Logic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
    }
    public void Spawn(){
        if (!disableSpawn){
            
            if (Logic.farestRoom == center){
                Debug.Log("Boss");
                Instantiate(enemy, center.transform.position, enemy.transform.rotation,this.gameObject.transform);
            }
            else if (center.GetComponent<RoomHandler>().routes == 1){
                SpawnItem();
            }
            else {
                for (int j = 0; j<Random.Range(1, maxEnemy); j++){
                int rand = Random.Range(0,spawns.Length);
                enemies.Add(Instantiate(enemy, spawns[rand].transform.position,spawns[rand].transform.rotation, this.gameObject.transform));
                for (int k = rand; k<spawns.Length-1; k++){
                    spawns[k] = spawns[k+1];
                }
                System.Array.Resize(ref spawns, spawns.Length-1);
            }
            }
        }
    }
    void SpawnItem(){
            Instantiate(item, ItemSpawn.transform.position, ItemSpawn.transform.rotation);
    }


    void FixedUpdate()
    {
        for (int i = 0; i<enemies.Count; i++){
            if (enemies[i] == null){
                enemies.RemoveAt(i);
            }
        }
    }
}
