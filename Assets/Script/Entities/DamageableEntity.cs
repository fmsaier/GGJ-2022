using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntity : MonoBehaviour
{
    public float CurrentLife = 10;
    public float MaxLife = 10;
    public float MoveSpeed = 5;
    public float AttackSpeed = 1.0f;
    public float Damage = 1;
    public bool IsInControl = true;
    public bool IsInvincible = false;

    [Header("Ennemies parameter")]
    public bool ScaleWithTime = false;
    public float Range = 1f;

    public virtual void Setup() { }

    public virtual void TakeDamage(float amount)
    {
        if (IsInvincible != true)
        {
            CurrentLife -= amount;
            if (CurrentLife <= 0)
            {
                CurrentLife = 0;
                Die();
            }
        }
    }

    public virtual void Heal(float amount)
    {
        CurrentLife += amount;
        if (CurrentLife >= MaxLife)
        {
            CurrentLife = MaxLife;
        }
    }

    public virtual void Die() { }
}
