using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Single_LoadGame()
    {
        SceneManager.LoadScene(1);// Game Scene
    }
    public void Co_OP_LoadGame()
    {
        SceneManager.LoadScene(2);// Game Scene
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

#else
        {
            Application.Quit()}
#endif
    }
}
