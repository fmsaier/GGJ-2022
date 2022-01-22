using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemiesDamageableEnities : DamageableEntity
{
    public override void Die()
    {
        base.Die();
        WaveManager.Instance.RemoveEntityFromCurrentWave(this);
        Destroy(this.gameObject);
    }
}
