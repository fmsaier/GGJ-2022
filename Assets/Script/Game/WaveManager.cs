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
    public BonusSpawner BonusSpawner;
    private int _currentWave = 0;
    private List<DamageableEntity> _currentWaveDamageableEntity = new List<DamageableEntity>();


    //Scaling beyond last level
    public float CurrentScalingFactor = 1;
    public float ScalingFactorIncrease = 1.1f;
    public WaveData EndgameBaseWaveData;

    public void StartWave()
    {
        WaveData currentWaveData = WaveDataList[_currentWave];
        StartCoroutine(CreateWave(currentWaveData));
    }

    public void StartEndGameWave()
    {
        CurrentScalingFactor *= ScalingFactorIncrease;
        EndgameBaseWaveData.numberOfEnnemies = (int)(EndgameBaseWaveData.numberOfEnnemies * CurrentScalingFactor);
        EndgameBaseWaveData.TimeBeforeStart = EndgameBaseWaveData.TimeBeforeStart * CurrentScalingFactor;
        StartCoroutine(CreateWave(EndgameBaseWaveData));
    }

    public void OnBonusPickUp()
    {
        //TODO Unfreeze timer;
        BonusSpawner.RemoveBonus();
        GameManager.Instance.FreezeTimer(false);
        _currentWave += 1;
        if (_currentWave < WaveDataList.Count)
        {
            StartWave();
        }
        else
        {
            StartEndGameWave();
        }
    }

    public void FinishWave()
    {
        BonusSpawner.CreatePermanentBonus();
        GameManager.Instance.FreezeTimer(true);
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
            entity.Setup();
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
