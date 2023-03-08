using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public int roomNumber = 0;
    public int maxRooms;
    public float roomOffset = 3.84f;
    public GameObject room;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown("r")){
            ResetTheGame();
        }
    }

    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
