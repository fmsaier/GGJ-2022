using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPickable : MonoBehaviour
{
    public float WaitTimeBeforePecikUp = 1.5f;
    public PermanentBuffScriptable permanentBuffScriptable;

    private bool _isPickable = false;

    void Update()
    {
        if (_isPickable == false)
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (_isPickable == true)
        {
            PlayerDamageableEntity playerDamageableEntity = col.GetComponent<PlayerDamageableEntity>();
            if (playerDamageableEntity != null)
            {
                playerDamageableEntity.AddStat(permanentBuffScriptable);
                WaveManager.Instance.OnBonusPickUp();
            }
        }
    }
}
