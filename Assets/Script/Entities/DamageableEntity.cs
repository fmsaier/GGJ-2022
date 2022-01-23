using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntity : MonoBehaviour
{
    public Animator Animator;
    public AnimationClip DeathClip;
    public float CurrentLife = 10;
    public float MaxLife = 10;
    public float MoveSpeed = 5;
    public float AttackSpeed = 1.0f;
    public float Damage = 1;
    public float KnockBackPower = 0f;
    public bool IsInControl = true;
    public bool IsInvincible = false;
    public bool IsImmuneToKnockBack = false;

    [Header("Ennemies parameter")]
    public bool ScaleWithTime = false;
    public float Range = 1f;

    [HideInInspector]
    public bool IsDead = false;
    public virtual void Setup() { }

    public virtual void TakeDamage(float amount)
    {
        if (GameManager.Instance.IsGamePaused == false)
        {
            if (IsDead == false)
            {
                if (IsInvincible != true)
                {
                    CurrentLife -= amount;
                    if (Animator != null)
                    {
                        Animator.Play("Hit");
                    }
                    if (CurrentLife <= 0)
                    {
                        CurrentLife = 0;
                        Die();
                    }
                }
            }
        }
    }

    public virtual void ApplyKnockBack(float duration, float knockBackForce, Vector3 direction)
    {
        if (IsImmuneToKnockBack != true)
        {
            //StopAllCoroutines();
            StartCoroutine(KnockBackCoroutine(duration, knockBackForce, direction));
        }
    }

    public virtual void Heal(float amount)
    {
        if (IsDead == false)
        {
            CurrentLife += amount;
            if (CurrentLife >= MaxLife)
            {
                CurrentLife = MaxLife;
            }
        }
    }

    public virtual void Die()
    {
        IsDead = true;
        IsInControl = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        if (DeathClip != null)
        {
            Animator.Play("Die");
            Destroy(this.gameObject, DeathClip.length);
            AudioManager.Instance.Play("BadDeath");
        }
    }

    IEnumerator KnockBackCoroutine(float duration, float knockBackForce, Vector3 direction)
    {
        float forceRatio = knockBackForce;
        IsInControl = false;

        while (duration > 0)
        {
            transform.position += direction.normalized * forceRatio * Time.deltaTime;
            forceRatio *= 0.99f;
            duration -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        IsInControl = true;
    }
}
