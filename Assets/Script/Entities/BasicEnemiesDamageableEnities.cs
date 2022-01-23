using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemiesDamageableEnities : DamageableEntity
{
    public override void Setup()
    {
        base.Setup();
        Damage *= WaveManager.Instance.CurrentScalingFactor;
        MaxLife *= WaveManager.Instance.CurrentScalingFactor;
        CurrentLife = MaxLife;
        MoveSpeed *= WaveManager.Instance.CurrentScalingFactor;
    }

    public override void Die()
    {
        WaveManager.Instance.RemoveEntityFromCurrentWave(this);
        base.Die();
    }
}
