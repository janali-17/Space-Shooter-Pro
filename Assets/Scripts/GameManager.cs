using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variables
    [SerializeField]
    private bool _IsGameOver;
    [SerializeField]
    public bool _IsCO_OpMode = false;

    // GameObjects

    void Update()
    {
        
        if(_IsCO_OpMode ==  false)
        { 
            if (Input.GetKeyUp(KeyCode.R) && _IsGameOver == true)
            {
                SceneManager.LoadScene(1); // Single Player Game Scene
            }
        }
        else if (_IsCO_OpMode == true) {
            if (Input.GetKeyUp(KeyCode.R) && _IsGameOver == true)
            {
                SceneManager.LoadScene(2); // Multi Player Game Scene
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    public void GameOver()
    {

            _IsGameOver = true;

    }
    
}