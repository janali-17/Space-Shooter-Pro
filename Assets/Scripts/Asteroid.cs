using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
   [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _ExplosionPrefab;
    private SpawnManager _SpawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {    
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Instantiate(_ExplosionPrefab,transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _SpawnManager.StartSpawning();
            Destroy(this.gameObject,0.25f);
        }
    }
}
