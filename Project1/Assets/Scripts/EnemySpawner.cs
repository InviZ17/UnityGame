using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawns;
    public List<GameObject> enemies;
    public GameObject enemy;
    public int maxEnemy;
    void Start()
    {
        
    }
    public void Spawn(){
        for (int j = 0; j<Random.Range(1, maxEnemy); j++){
            int rand = Random.Range(0,spawns.Length);
            enemies.Add(Instantiate(enemy, spawns[rand].transform.position,spawns[rand].transform.rotation, this.gameObject.transform));
            for (int k = rand; k<spawns.Length-1; k++){
                spawns[k] = spawns[k+1];
            }
            System.Array.Resize(ref spawns, spawns.Length-1);
        }
    }


    void FixedUpdate()
    {
        for (int i = 0; i<enemies.Count; i++){
            if (enemies[i] == null){
                enemies.RemoveAt(i);
                Debug.Log(enemies.Count);
            }
        }
    }
}
