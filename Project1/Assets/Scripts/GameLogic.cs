using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public int roomNumber = 0;
    public int maxRooms;
    public float roomOffset = 3.84f;
    public float maxDist = 0f;
    public GameObject farestRoom;
    public GameObject room;
    public GameObject boss;
    public GameObject wall;
    public GameObject[] enemy;
    private GameObject[] fog;
    void Start()
    {
        
    }

    void Update()
    {
        fog = GameObject.FindGameObjectsWithTag("Fog");
        if (Input.GetKeyDown("r")){
            ResetTheGame();
        }
        if (Input.GetKeyDown("escape")){
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown("n")){
            for (int i = 0;i<fog.Length;i++){
                fog[i].GetComponent<FogReveal>().debugSpawn = !fog[i].GetComponent<FogReveal>().debugSpawn;

            }
        }
        if (Input.GetKeyDown("p")){

            for (int i = 0;i<fog.Length;i++){
                fog[i].GetComponent<FogReveal>().debugReveal = !fog[i].GetComponent<FogReveal>().debugReveal;
                fog[i].GetComponent<SpriteRenderer>().enabled = true;
                //SceneVisibilityManager.instance.ToggleVisibility(fog[i], false);
            }
            if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize == 20.15f){
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 6.73f;
            }
            else{
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 20.15f;
            }

        }
    }

    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
