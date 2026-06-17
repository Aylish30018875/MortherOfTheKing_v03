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
    public int hardEnemies = 2;
}

public class WaveManager : MonoBehaviour
{
    public WaveData[] waves;
    public Button startWaveButton;

    public GameObject easyEnemyPrefab;
    public GameObject hardEnemyPrefab;

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
            yield return new WaitForSeconds((wave.duration * 3f) / wave.easyEnemies);
        }
        for (int i = 0; i < wave.hardEnemies; i++)
        {
            SpawnEnemy(hardEnemyPrefab);
            yield return new WaitForSeconds((wave.duration * 3f) / wave.hardEnemies);
        }

        yield return new WaitForSeconds((wave.duration * 3f));

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
