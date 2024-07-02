using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    [SerializeField]
    private float _speed = 1.5f;
    [SerializeField]
    private float _FireRate = 0.15f;
    [SerializeField]
    private float _canfire = -1f;
    [SerializeField]
    private int _lives = 3;
    private bool _IsTripleShotActive = false;
    private bool _IsSpeedBoostActive = false;
    private bool _IsShieldActive = false;
    [SerializeField]
    private int _score = 0;

    // Prefabs
    [SerializeField]
    private GameObject _LaserPrefab;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private GameObject _ShieldVisualizer;
    [SerializeField]
    private GameObject _rightEng;
    [SerializeField]
    private GameObject _leftEng;
    private SpawnManager _SpawnManager;
    private UI_Manager _uiManager;

    // Audio And Animation 
    [SerializeField]
    AudioClip _laserAudio;
    [SerializeField]
    AudioClip _ExplosionAudio;
    AudioSource audioSource;

    void Start()
    {
      
        transform.position = new Vector3(0, 0, 0);

        _SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null ) 
        {
            Debug.LogError("The Audio on player is Null");
        }
        else
        {
          audioSource.clip  = _laserAudio;
        }
        if (_SpawnManager == null )
        {
          Debug.LogError("The Spawn manager is Null");
        }
    }
    void Update()
    {
        calculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
        {
            FireLaser();
        }
    }
    void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical"); 
        Vector3 Direction = new Vector3(horizontalInput, verticalInput, 0);
        
        if(_IsSpeedBoostActive == true)
        {
           
            transform.Translate(Direction * (_speed) * Time.deltaTime);
        }
        else
        { 
        transform.Translate(Direction * _speed * Time.deltaTime);
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.9f, 0), 0);

        if (transform.position.x <= -9.2)
        {
            transform.position = new Vector3(11.4f, transform.position.y, 0);
        }
        else if (transform.position.x >= 11.4)
        {
            transform.position = new Vector3(-9.2f, transform.position.x, 0);
        }
    }
    void FireLaser()
    {
        _canfire = Time.time + _FireRate;
        
        if(_IsTripleShotActive == true)
        {
            Instantiate(_TripleShotPrefab,transform.position,Quaternion.identity);
        }
        else {
            Instantiate(_LaserPrefab, transform.position + new Vector3(0, 1.02f, 0), Quaternion.identity);
        }
        audioSource.Play();
    }
    public void Damage()
    {
        if (_IsShieldActive == true)
        {   
            _ShieldVisualizer.SetActive(false);
            _IsShieldActive = false;
            return;
        }
            _lives--;
        if (_lives == 2)
        {
            _rightEng.SetActive(true);
        }
        else if(_lives == 1)
        {
            _leftEng.SetActive(true);
        }
        if (_lives == 0)
        {
            _SpawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
        _uiManager.UpdateLives(_lives);

    }
    public void ActiveTriple()
    {
        _IsTripleShotActive = true;
        StartCoroutine(DeactiveTripleShot());
    }
    IEnumerator DeactiveTripleShot()
    {
         yield return new WaitForSeconds(5.0f);
            _IsTripleShotActive = false;
    }
    public void ActiveSpeed()
    {
        _IsSpeedBoostActive = true;
        StartCoroutine(DeactiveSpeedBoost());
        _speed = (_speed + 5);
    }    
    IEnumerator DeactiveSpeedBoost()
    {
        yield return new WaitForSeconds(5.0f);
        _IsSpeedBoostActive=false;
        _speed = (_speed - 5);
    }
    public void ActiveShield()
    {
        _IsShieldActive = true;
        _ShieldVisualizer.SetActive(true);
    }
    public void AddScore(int points)
    {
        _score = _score + points;
        _uiManager.UpdateScore(_score);
    }
   public void RestartGame()
    {
        
    }
}
