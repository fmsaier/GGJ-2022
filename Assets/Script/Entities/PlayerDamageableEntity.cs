using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageableEntity : DamageableEntity
{
    public override void Die()
    {
        base.Die();
        Debug.Log("Player is dead");
    }
}
