using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public DamageableEntity entity;
    public float TriggerAttackTimer = 10;
    public BossProjectileBehavior BossProjectileBehavior;
    public ProjectileBehavior ProjectileBehavior;

    private float _currentPatternTimer;
    private PlayerDamageableEntity _playerDamageableEntity;

    void Start()
    {
        _currentPatternTimer = TriggerAttackTimer;
        _playerDamageableEntity = FindObjectOfType<PlayerDamageableEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerDamageableEntity != null && entity.IsInControl == true)
        {
            if (_currentPatternTimer > 0)
            {
                _currentPatternTimer -= Time.deltaTime;
            }
            else
            {
                TriggerAttack();
                _currentPatternTimer = TriggerAttackTimer;
            }
            Flip();
        }
    }

    void TriggerAttack()
    {
        StartCoroutine(AttackPatternTwo());
    }

    public void Flip()
    {
        if (_playerDamageableEntity.transform.position.x > this.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    IEnumerator AttackPatternOne()
    {
        int numberOfProjectile = 4;
        while (numberOfProjectile > 0)
        {
            Instantiate(BossProjectileBehavior, transform.position, Quaternion.identity, null);
            numberOfProjectile--;
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator AttackPatternTwo()
    {
        int numberOfProjectile = 4;

        while (numberOfProjectile > 0)
        {
            ProjectileBehavior pBeh = Instantiate(ProjectileBehavior, transform.position, Quaternion.identity, null);
            Vector3 targetDirection = (_playerDamageableEntity.transform.position - transform.position).normalized;
            targetDirection.z = this.transform.position.z;
            float AngleRad = Mathf.Atan2(targetDirection.y, targetDirection.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            pBeh.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
            pBeh.Setup(entity, this.gameObject, targetDirection);
            numberOfProjectile--;
            yield return new WaitForSeconds(1);
        }
    }
}
