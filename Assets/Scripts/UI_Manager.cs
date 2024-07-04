using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Manager : MonoBehaviour
{
    // Prefabs
    [SerializeField]
    private Text _scoretext;
    [SerializeField]
    private Image _liveImage;
    [SerializeField]
    private Image _liveImage2;
    [SerializeField]
    private Sprite[] _livesprites;
    [SerializeField]
    private Text _GameOver;
    [SerializeField]
    private Text _Restarttxt;
    private GameManager _gameManager;


    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _GameOver.gameObject.SetActive(false);
        _Restarttxt.gameObject.SetActive(false);
        _scoretext.text = "Score : " + 0;
    }
    public void UpdateScore(int scorePoints)
    {
        _scoretext.text = "Score : " + scorePoints.ToString();
    }
    public void UpdateLives(int CurrentLives)
    {
        _liveImage.sprite = _livesprites[CurrentLives];
        if (CurrentLives == 0)
        {
            _gameManager.GameOver();
            GameOverSequence();
        }
    }
    public void GameOverSequence() {
        _GameOver.gameObject.SetActive(true);
        _Restarttxt.gameObject.SetActive(true);
        StartCoroutine(FlickeringText());
    }
    public IEnumerator FlickeringText()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            _GameOver.gameObject.SetActive(false);
            _Restarttxt.gameObject.SetActive(false);
            yield return new WaitForSeconds(.5f);
            _GameOver.gameObject.SetActive(true);
            _Restarttxt.gameObject.SetActive(true);

        }
    }
    public void Pause()
    {
        Time.timeScale = 0.0f;
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
    }

}
