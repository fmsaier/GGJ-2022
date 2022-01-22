using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region Singleton pattern

    /*
    ** Singleton pattern
    */

    private static WaveManager _instance;

    public static WaveManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public List<WaveData> WaveDataList = new List<WaveData>();
    public List<Transform> Spawners = new List<Transform>();
    private int _currentWave = 0;
    private List<DamageableEntity> _currentWaveDamageableEntity = new List<DamageableEntity>();

    public void StartWave()
    {
        WaveData currentWaveData = WaveDataList[_currentWave];
        StartCoroutine(CreateWave(currentWaveData));
    }

    public void StartEndGameWave()
    {
        Debug.Log("We are in the endgame now");
    }

    public void FinishWave()
    {
        _currentWave += 1;
        if (_currentWave < WaveDataList.Count)
        {
            Debug.Log("Starting next wave");
            StartWave();
        }
        else
        {
            StartEndGameWave();
        }
    }

    public void RemoveEntityFromCurrentWave(DamageableEntity entity)
    {
        if (_currentWaveDamageableEntity.Contains(entity))
        {
            _currentWaveDamageableEntity.Remove(entity);
            if (_currentWaveDamageableEntity.Count == 0)
            {
                FinishWave();
            }
        }
    }

    private IEnumerator CreateWave(WaveData currentWaveData)
    {

        int currentBestiarySize = currentWaveData.bestiary.Count;
        yield return new WaitForSeconds(currentWaveData.TimeBeforeStart);

        for (int i = 0; i < currentWaveData.numberOfEnnemies; i++)
        {
            DamageableEntity entity = Instantiate(currentWaveData.bestiary[Random.Range(0, currentBestiarySize)], Spawners[Random.Range(0, Spawners.Count)].position, Quaternion.identity);
            _currentWaveDamageableEntity.Add(entity);
            yield return new WaitForSeconds(currentWaveData.WaveTime / (float)currentWaveData.numberOfEnnemies);
        }
    }

    #region WaveData
    [System.Serializable]
    public class WaveData
    {
        public int numberOfEnnemies = 1;
        public float WaveTime = 1;
        public float TimeBeforeStart = 10f;
        public List<DamageableEntity> bestiary = new List<DamageableEntity>();

    }
    #endregion
}
