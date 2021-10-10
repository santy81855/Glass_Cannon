using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {  SPAWNING, WAITING, COUNTING };

    // allows us to change the values inside of unity
    [System.Serializable] 
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    
    // Start is called before the first frame update
    void Start()
    {
        waveCountdown = timeBetweenWaves;

    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            // check if enemies are still alive
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
           if (state != SpawnState.SPAWNING)
           {
               // start spawning wave
               StartCoroutine( SpawnWave ( waves[nextWave] ) );
           } 
        }

        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted ()
    {
        Debug.Log("Wave completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log ("All waves complete! Looping...");
        }

        nextWave++;
    }

    // enemies have to be tagged with "Enemy" tag
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
    
    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        // spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds( 1f/_wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        // spawn enemy
        Debug.Log("Spawning Enemy: " + _enemy.name);    
        Instantiate (_enemy, transform.position, transform.rotation);
    }
}