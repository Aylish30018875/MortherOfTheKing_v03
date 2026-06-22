using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

[Serializable]
public class WaveData
{
    public float duration = 10;
    public int easyEnemies = 5;

    public int easyEnemiesGreen;
    public int easyEnemiesBlue;
    public int easyEnemiesPurple;

    public int hardEnemies = 2;

    public int hardEnemiesGreen;
    public int hardEnemiesBlue;
    public int hardEnemiesPurple;
}

public class WaveManager : MonoBehaviour
{
    public WaveData[] waves;
    public Button startWaveButton;

    public GameObject easyEnemyPrefab;

    public GameObject easyEnemyGreenPrefab;
    public GameObject easyEnemyBluePrefab;
    public GameObject easyEnemyPurplePrefab;

    public GameObject hardEnemyPrefab;

    public GameObject hardEnemyGreenPrefab;
    public GameObject hardEnemyBluePrefab;
    public GameObject hardEnemyPurplePrefab;

    public Transform[] wayPoints;

    [SerializeField] private int _currentWaveIndex = 0;
    [SerializeField] private bool _isRunning = false;

    //Allows other scripts to check the current wave number
    public int CurrentWaveIndex
    {
        get { return _currentWaveIndex; }
    }

    //Allows other scripts to check if a wave is currently running
    public bool IsRunning
    {
        get { return _isRunning; }
    }

    void Start()
    {
        startWaveButton.onClick.AddListener(StartWave);
    }

    public void StartWave()
    {
        if (_isRunning) return;
        if (_currentWaveIndex >= waves.Length) return;

        StartCoroutine(RunWave());
    }

    IEnumerator RunWave()
    {
        _isRunning = true;
        startWaveButton.interactable = false;

        WaveData wave = waves[_currentWaveIndex];

        for (int i = 0; i < wave.easyEnemies; i++)
        {
            SpawnEnemy(easyEnemyPrefab);
            yield return new WaitForSeconds((wave.duration / 3f) / wave.easyEnemies);
        
        }
        //trying some stuff :)
        for (int i = 0; i < wave.easyEnemiesGreen; i++)
        {
            SpawnEnemy(easyEnemyGreenPrefab);
            yield return new WaitForSeconds((wave.duration / 3f) / wave.easyEnemiesGreen);
        }
        for (int i = 0; i < wave.easyEnemiesBlue; i++)
        {
            SpawnEnemy(easyEnemyBluePrefab);
            yield return new WaitForSeconds((wave.duration / 3f) / wave.easyEnemiesBlue);
        }
        for (int i = 0; i < wave.easyEnemiesPurple; i++)
        {
            SpawnEnemy(easyEnemyPurplePrefab);
            yield return new WaitForSeconds((wave.duration / 3f) / wave.easyEnemiesPurple);
        }
        for (int i = 0; i < wave.hardEnemies; i++)
        {
            SpawnEnemy(hardEnemyPrefab);
            yield return new WaitForSeconds((wave.duration / 3f) / wave.hardEnemies);
        }
        //more of my stuff :)
        for (int i = 0; i < wave.hardEnemiesGreen; i++)
        {
            SpawnEnemy(hardEnemyGreenPrefab);
            yield return new WaitForSeconds((wave.duration / 3f) / wave.hardEnemiesGreen);
        }
        for (int i = 0; i < wave.hardEnemiesBlue; i++)
        {
            SpawnEnemy(hardEnemyBluePrefab);
            yield return new WaitForSeconds((wave.duration / 3f) / wave.hardEnemiesBlue);
        }
        for (int i = 0; i < wave.hardEnemiesPurple; i++)
        {
            SpawnEnemy(hardEnemyPurplePrefab);
            yield return new WaitForSeconds((wave.duration / 3f) / wave.hardEnemiesPurple);
        }

        yield return new WaitForSeconds((wave.duration / 3f));

        _currentWaveIndex++;
        _isRunning = false;
        startWaveButton.interactable = true;
    }

    void SpawnEnemy(GameObject prefab)
    {
        GameObject e = Instantiate(prefab, wayPoints[0].position, Quaternion.identity);
        Enemy enemy = e.GetComponent<Enemy>();
        enemy.wayPoints = wayPoints;
    }
}
