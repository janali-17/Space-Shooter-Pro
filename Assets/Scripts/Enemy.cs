using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variables
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private float _fireRate = 3.0f;
    [SerializeField]
    private float _canFire = -1;

 
    //Prefabs
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyLaserPrefab;
    private Player _player;
    private Laser lasers;

    // Audio and Animation
    private Animator _animator;
    [SerializeField]
    private AudioClip _explosionClip;
    private AudioSource _audioSource;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if ( _player == null )  
        {
            Debug.LogError("There is no player");
        }
        _animator = GetComponent<Animator>();
        if( _animator == null )
        {
            Debug.LogError("Animator is EMpty");
        }
        if( _explosionClip == null )
        {
            Debug.LogError("The audio of Explosion on enemy is Null");
        }
        else
        {
            _audioSource.clip = _explosionClip;
        }
    }
  
    void Update()
    {
        CalculateMovement();
        if(_canFire < Time.time)
        {
            _fireRate = Random.Range(3.0f, 6.0f);
            _canFire = Time.time + _fireRate;
            GameObject _enemylaser = Instantiate(_enemyLaserPrefab,transform.position, Quaternion.identity);
            Laser[] lasers = _enemylaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser(); 
            }
        }
    }
    private void CalculateMovement()
    {
        float randomSpawn = Random.Range(-7.6f, 8.5f);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        Vector3 randomPositon = new Vector3(randomSpawn, 0, 0);

        if (transform.position.y < -5)
        {
            transform.position = new Vector3(randomSpawn, 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject,2.6f);

        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _player.AddScore(10);
            _animator.SetTrigger("OnEnemyDeath"); 
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2.5f);
        }
    }
}