using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    #region TimerCountdown
    public GameObject textDisplay;
    public float secondsLeft;
    public bool countingDown;
    #endregion

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float spawnRate;
    }

    public int currentHealth;
    public int healthMultiplier;

    public Wave[] waves;
    private int nextWave = 0;
    public float nextWaveCountdown = 20.0f;
    public float nextWaveCountdownTimer;

    public int NextWave
    {
        get { return nextWave + 1; }
    }

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    public float WaveCountdown
    {
        get { return waveCountdown; }
    }

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    //private float currentWave;

    public SpawnState State
    {
        get { return state; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            print("no spawn points referenced");
        }

        nextWaveCountdownTimer = nextWaveCountdown;
        waveCountdown = timeBetweenWaves;
        secondsLeft = nextWaveCountdownTimer;
        textDisplay.GetComponent<Text>().text = "00:" + nextWaveCountdownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CountDown());

        // Where we wait for the player to kill enemies or a time period until next wave spawns
        if (state == SpawnState.WAITING)
        {
            nextWaveCountdownTimer -= Time.deltaTime;
            if (EnemyListScript.enemyList.Count == 0 || nextWaveCountdownTimer <= 0)//CHECK IF TIME HAS PASSED FOR NEXT WAVE
            {
                //Begin a new round
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
                StartCoroutine( SpawnWave ( waves[nextWave] ) );//Start spawning wave
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime; //decrease 1 sec from waveCountdown
        }

        
    }

    void WaveCompleted()
    {
        print("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        nextWaveCountdownTimer = nextWaveCountdown;

        if (nextWave + 1 > waves.Length - 1)
        {
            //within this if statement we can have multipliers of any kind, game over screen, etc.
            nextWave = 0;
            print("Completed all waves. Looping...");
        }
        else
        {
            nextWave++;
        }
    } 

    bool EnemyIsAlive()
    { // This method will need to be altered to reflect just time and not enemies
      // I want to have a the waveSpawner based on time and not enemies to allow the pile up of extra zombies once they out-scale the player as the waves continue.
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if(GameObject.FindGameObjectsWithTag("Enemy") == null )
            {
                print("return false. no enemies detected");
                return false;
            }
        }
        print("return true. enemies detected");
        return true;
    }

    public IEnumerator SpawnWave(Wave _wave)
    {
        print("Spawning Wave:" + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)//Spawn
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds( 1f/_wave.spawnRate );
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy) //GameObject previously Transform

    {
        print("Spawning Enemy:" + _enemy.name);
        //Spawn enemy

        Transform _sp = spawnPoints[ Random.Range (0, spawnPoints.Length) ];

        //COMMENTED OUT TO TEST NEW HEALTH MULTIPLIER SYNTAX
        Instantiate (_enemy, _sp.position, _sp.rotation);

        //currently multiplies the wave# by healthMultiplier, DESIGN UPDATE: we may lower our weapon damage PENDING.
        currentHealth = NextWave * healthMultiplier;

        EnemyHealth.maxHealth = currentHealth * healthMultiplier;

    }

    
    IEnumerator CountDown()
    {
        secondsLeft = nextWaveCountdownTimer;
        secondsLeft = Mathf.Round(secondsLeft * 1f) / 1f;
        countingDown = true;
        yield return new WaitForSeconds(1);
        
        if(secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        }
        else{
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        }
        countingDown = false;
    }    

}
