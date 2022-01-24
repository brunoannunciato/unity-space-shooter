using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject[] _powerUp;

    [SerializeField]
    private bool isToStopRespawnEnemy = false;
    [SerializeField]
    private bool isToStopRespawnPowerup = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerup());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2f);

        while (isToStopRespawnEnemy == false) {
            Vector3 enemyPos = new Vector3(Random.Range(-9.54f, 9.54f), 7, 0);
            Instantiate(_enemy, enemyPos, Quaternion.identity);

            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPowerup()
    {
        yield return new WaitForSeconds(2.3f);

        while(isToStopRespawnPowerup == false)
        {
            int randomPowerUpId = Random.Range(0, _powerUp.Length);
            Vector3 powerupPos = new Vector3(Random.Range(-9.54f, 9.54f), 7, 0);
            Instantiate(_powerUp[randomPowerUpId], powerupPos, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3, 7));
        }
    }

    public void StopRespawn()
    {
        isToStopRespawnEnemy = true;
        isToStopRespawnPowerup = true;
    }
}
