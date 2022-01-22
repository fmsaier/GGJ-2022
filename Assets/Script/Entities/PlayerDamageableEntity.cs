using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageableEntity : DamageableEntity
{
    public void AddStat(PermanentBuffScriptable permanentBuffScriptable)
    {
        Damage += permanentBuffScriptable.AttackModifier;
        MaxLife += permanentBuffScriptable.LifeModifier;
        base.Heal(permanentBuffScriptable.LifeModifier);
        MoveSpeed += permanentBuffScriptable.SpeedModifier;
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Player is dead");
    }
}
