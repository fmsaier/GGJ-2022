using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPickable : MonoBehaviour
{
    public Animator Animator;
    public float WaitTimeBeforePecikUp = 1.5f;
    public PermanentBuffScriptable permanentBuffScriptable;

    private bool _isPickable = false;
    private bool _batchDone = false;
    void Update()
    {
        if (_batchDone == false && _isPickable == false)
        {
            if (WaitTimeBeforePecikUp > 0)
            {
                WaitTimeBeforePecikUp -= Time.deltaTime;
            }
            else
            {
                _isPickable = true;
            }
        }
    }

    public void EndBatch()
    {   
        _batchDone = true;
        _isPickable = false;
        Animator.Play("Die");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager.Instance.IsGamePaused != true && _isPickable == true)
        {
            PlayerDamageableEntity playerDamageableEntity = col.GetComponent<PlayerDamageableEntity>();
            if (playerDamageableEntity != null)
            {
                playerDamageableEntity.AddStat(permanentBuffScriptable);
                AudioManager.Instance.Play("Bonus");
                WaveManager.Instance.OnBonusPickUp();
            }
        }
    }
}
