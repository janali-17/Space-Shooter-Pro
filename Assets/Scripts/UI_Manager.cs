using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Manager : MonoBehaviour
{
    // Variables
    public int CurrentPoints , BestPoints;
    // Prefabs
    [SerializeField]
    private Text _scoretext, _bestScore;
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
        BestPoints = PlayerPrefs.GetInt("Best : ", 0);
        if(_bestScore != null)
        {
            _bestScore.text = "Best : " + BestPoints;
        }
    }

    public void UpdateScore()
    {
        CurrentPoints += 10;
        _scoretext.text = "Score : " + CurrentPoints.ToString();
    }
    public void CheckForBest()
    {
        if(CurrentPoints > BestPoints)
        {
            BestPoints = CurrentPoints;
            PlayerPrefs.SetInt("Best : ", BestPoints);
            if(_bestScore != null)
            {
                _bestScore.text = "Best : " + BestPoints;
            }
        }
    }
    public void UpdateLives1(int CurrentLives)
    {
        _liveImage.sprite = _livesprites[CurrentLives];
        if (CurrentLives == 0)
        {
            _gameManager.GameOver();
            GameOverSequence();
        }
    }
    public void UpdateLives2(int CurrentLives)
    {
        _liveImage2.sprite = _livesprites[CurrentLives];
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


}
