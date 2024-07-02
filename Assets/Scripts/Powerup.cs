using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // variables
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField] // 0 = TripleShot , 1 = Speed , 2 = Sheild
    private int _powerID;

    // Prefabs
    private Player _player;

    // Audio and Animation
    [SerializeField]
    private AudioClip _CollectedAudio = null;

 
    void Update()
    {
        float RandSpawn = Random.Range(-9.5f, 9.5f);
        transform.Translate(Vector3.down * _speed *Time.deltaTime);
        Vector3 randomPosition = new Vector3(RandSpawn,0,0);
        if(transform.position.y < -7.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == ("Player"))
        {
           
          Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                    switch(_powerID)
                    {
                        case 0:
                            player.ActiveTriple();
                            break;
                        case 1:
                        player.ActiveSpeed();
                        break;
                        case 2:
                        player.ActiveShield();
                            break;
                        default:
                            Debug.Log("Default value");
                            break;
                    }
                AudioSource.PlayClipAtPoint(_CollectedAudio,transform.position);
                Destroy(this.gameObject);
            }
        }
    }
    
}
