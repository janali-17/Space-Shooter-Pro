using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // Variables
    [SerializeField]
    private bool _IsGameOver;
    [SerializeField]
    public bool _IsCO_OpMode = false;

    // GameObjects
    [SerializeField]
    private GameObject _Pausepanel;
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
            Pause();
        }

    }
    public void GameOver()
    {

            _IsGameOver = true;

    }
    public void Pause()
    { 
        _Pausepanel.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void Resume()
    {
        _Pausepanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}