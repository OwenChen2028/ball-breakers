using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Player;
    public GameObject EnemyBasic;
    public GameObject EnemyRanged;
    public GameObject[] PowerUps;

    public bool enemiesAlive;
    public bool rewardsActive;
    public float currentWave;
    public bool waveReady;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waveReady)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0)
            {
                enemiesAlive = false;
            }

            GameObject[] rewards = GameObject.FindGameObjectsWithTag("Drop");
            if (rewards.Length == 0)
            {
                rewardsActive = false;
            }
            SpawnWave();
        }
    }
    void SpawnWave()
    {
        if (enemiesAlive || rewardsActive)
        {
            return;
        }

        waveReady = false;

        if (currentWave - (int) currentWave == 0.5f)
        {
            rewardsActive = true;
            Invoke("SpawnPowerup", 0.0f);
            Invoke("SpawnPowerup", 0.5f);
            Invoke("SpawnPowerup", 1.0f);
            Invoke("WaveReady", 1.5f);
        }
        else if (currentWave == 0)
        {
            Invoke("StartWave", 3.0f);
        }
        else
        {
            enemiesAlive = true;
        }

        if (currentWave == 1)
        {
            Invoke("SpawnBasic", 0.0f);
            Invoke("SpawnBasic", 0.5f);

            Invoke("SpawnBasic", 2.5f);
            Invoke("SpawnBasic", 3.0f);
            Invoke("SpawnBasic", 3.5f);
            Invoke("SpawnBasic", 4.0f);
            Invoke("SpawnBasic", 4.5f);

            Invoke("SpawnBasic", 6.5f);
            Invoke("SpawnBasic", 7.0f);
            Invoke("SpawnBasic", 7.5f);
            Invoke("SpawnBasic", 8.0f);
            Invoke("SpawnBasic", 8.5f);

            Invoke("WaveReady", 9.0f);
        }
        else if (currentWave == 2)
        {
            Invoke("SpawnBasic", 0.0f);
            Invoke("SpawnBasic", 0.5f);
            Invoke("SpawnBasic", 1.0f);
            Invoke("SpawnBasic", 1.5f);
            Invoke("SpawnBasic", 2.0f);

            Invoke("SpawnRanged", 4.0f);

            Invoke("SpawnBasic", 8.0f);
            Invoke("SpawnBasic", 8.5f);
            Invoke("SpawnBasic", 9.0f);
            Invoke("SpawnRanged", 9.5f);
            Invoke("SpawnRanged", 10.0f);

            Invoke("WaveReady", 10.5f);
        }
        else if (currentWave == 3)
        {
            Invoke("SpawnBasic", 0.0f);
            Invoke("SpawnRanged", 0.5f);
            Invoke("SpawnBasic", 1.0f);
            Invoke("SpawnRanged", 1.5f);
            Invoke("SpawnBasic", 2.0f);

            Invoke("SpawnRanged", 4.0f);
            Invoke("SpawnBasic", 4.5f);
            Invoke("SpawnBasic", 5.0f);
            Invoke("SpawnBasic", 5.5f);
            Invoke("SpawnRanged", 6.0f);

            Invoke("SpawnBasic", 8.0f);
            Invoke("SpawnBasic", 8.5f);
            Invoke("SpawnBasic", 9.0f);
            Invoke("SpawnRanged", 9.5f);
            Invoke("SpawnRanged", 10.0f);

            Invoke("WaveReady", 10.5f);
        }
        else if (currentWave == 4)
        {
            Invoke("SpawnRanged", 0.0f);
            Invoke("SpawnRanged", 0.5f);
            Invoke("SpawnRanged", 1.0f);

            Invoke("SpawnBasic", 3.0f);
            Invoke("SpawnBasic", 3.5f);
            Invoke("SpawnBasic", 4.0f);

            Invoke("SpawnRanged", 6.0f);
            Invoke("SpawnRanged", 6.5f);
            Invoke("SpawnRanged", 7.0f);

            Invoke("SpawnBasic", 9.0f);
            Invoke("SpawnBasic", 9.5f);
            Invoke("SpawnBasic", 10.0f);

            Invoke("WaveReady", 10.5f);
        }
        else if (currentWave == 5)
        {
            Invoke("SpawnBasic", 0.0f);
            Invoke("SpawnBasic", 0.5f);
            Invoke("SpawnBasic", 1.0f);
            Invoke("SpawnRanged", 1.5f);
            Invoke("SpawnRanged", 2.0f);

            Invoke("SpawnBasic", 4.0f);
            Invoke("SpawnBasic", 4.5f);
            Invoke("SpawnBasic", 5.0f);
            Invoke("SpawnRanged", 5.5f);
            Invoke("SpawnRanged", 6.0f);

            Invoke("SpawnBasic", 8.0f);
            Invoke("SpawnBasic", 8.5f);
            Invoke("SpawnBasic", 9.0f);
            Invoke("SpawnRanged", 9.5f);
            Invoke("SpawnRanged", 10.0f);

            Invoke("SpawnBasic", 11.0f);
            Invoke("SpawnBasic", 12.5f);
            Invoke("SpawnBasic", 13.0f);
            Invoke("SpawnRanged", 13.5f);
            Invoke("SpawnRanged", 14.0f);

            Invoke("WaveReady", 14.5f);
        }
        else if (currentWave >= 6 && currentWave - (int) currentWave != 0.5f)
        {
            Invoke("SpawnRanged", 0.0f);
            Invoke("SpawnRanged", 0.5f);
            Invoke("SpawnRanged", 1.0f);
            Invoke("SpawnRanged", 1.5f);
            Invoke("SpawnRanged", 2.0f);

            Invoke("SpawnRanged", 4.0f);
            Invoke("SpawnRanged", 4.5f);
            Invoke("SpawnRanged", 5.0f);
            Invoke("SpawnRanged", 5.5f);
            Invoke("SpawnRanged", 6.0f);

            Invoke("SpawnRanged", 8.0f);
            Invoke("SpawnRanged", 8.5f);
            Invoke("SpawnRanged", 9.0f);
            Invoke("SpawnRanged", 9.5f);
            Invoke("SpawnRanged", 10.0f);

            Invoke("SpawnRanged", 12.0f);
            Invoke("SpawnRanged", 12.5f);
            Invoke("SpawnRanged", 13.0f);
            Invoke("SpawnRanged", 13.5f);
            Invoke("SpawnRanged", 14.0f);

            Invoke("WaveReady", 14.5f);
        }
    }
    void StartWave()
    {
        waveReady = true;
        currentWave = 1;
    }

    void WaveReady()
    {
        waveReady = true;
        currentWave += 0.5f;
    }

    void SpawnBasic()
    {
        if (Player != null)
        {
            GameObject instance = Instantiate(EnemyBasic, transform.position, transform.rotation);
            instance.GetComponent<EnemyController>().target = Player;
            instance.GetComponent<Rigidbody2D>().AddForce(Quaternion.Euler(0, 0, UnityEngine.Random.Range(-60f, 60f)) * (transform.right * -1) * UnityEngine.Random.Range(1000f, 1500f));
        }   
    }

    void SpawnRanged()
    {
        if (Player != null)
        {
            GameObject instance = Instantiate(EnemyRanged, transform.position, transform.rotation);
            instance.GetComponent<EnemyController>().target = Player;
            instance.GetComponent<Rigidbody2D>().AddForce(Quaternion.Euler(0, 0, UnityEngine.Random.Range(-60f, 60f)) * (transform.right * -1) * UnityEngine.Random.Range(1000f, 1500f));
        }
    }
    void SpawnPowerup()
    {
        GameObject instance = Instantiate(PowerUps[UnityEngine.Random.Range(0, PowerUps.Length)], transform.position, transform.rotation);
        instance.GetComponent<PowerUp>().target = Player;
        instance.GetComponent<Rigidbody2D>().AddForce(Quaternion.Euler(0, 0, UnityEngine.Random.Range(-60f, 60f)) * (transform.right * -1) * UnityEngine.Random.Range(2000, 2500));
    }
}
