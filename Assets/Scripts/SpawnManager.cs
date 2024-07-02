using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Variables
    private bool _stopSpawning = false;

    // Prefabs
    [SerializeField]
    private GameObject _enemyPrefabs;
    [SerializeField]
    private GameObject _enemyContainer;   
    [SerializeField]
    private GameObject[] _PowerUps;

   public void  StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTripleShotPowerUPRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {


            yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {    
            Vector3 PostPos = new Vector3(Random.Range(-7.6f, 8.5f), 8, 0);
            GameObject newEnemy = Instantiate(_enemyPrefabs,PostPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }

    }
    IEnumerator SpawnTripleShotPowerUPRoutine()
    {
            yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            Vector3 PostPos = new Vector3(Random.Range(-7.6f, 8.5f), 8, 0);
            int RandPower = Random.Range(0, 3);
            Instantiate(_PowerUps[RandPower], PostPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(6f, 10f));
        }
    }

    public void OnPlayerDeath()    
    {
       _stopSpawning = true;  
    }

}
