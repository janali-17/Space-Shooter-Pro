﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _IsGameOver;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.R) && _IsGameOver== true) {
            SceneManager.LoadScene(1); //  Game Scene
        }
    }
    public void GameOver()
    {
        _IsGameOver = true;
    }
}
