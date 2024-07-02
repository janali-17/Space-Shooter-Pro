using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // variable
    [SerializeField]
    private float _speed = 3.0f;
    private bool _IsEnemyLaser = false;


    void Update()
    {
        if(_IsEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
        
    }
    public void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8)
        {
            Destroy(this.gameObject);
        }
        if (transform.parent != null && transform.position.y > 8)
        { Destroy(transform.parent.gameObject); }
    }
    public void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8)
        {
            if (transform.parent != null)
            { 
                Destroy(transform.parent.gameObject); 
            }
            Destroy(this.gameObject);
        }
    }
    public void AssignEnemyLaser()
    {
        _IsEnemyLaser = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.tag == "Player" && _IsEnemyLaser == true)
        { 
            Player player = other.GetComponent<Player>();
            player.Damage();
        }
        
    }
}
