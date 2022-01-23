using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public DamageableEntity entity;
    public Transform PatternOnePos;
    public Animator Animator;
    public float TriggerAttackTimer = 10;
    public BossProjectileBehavior BossProjectileBehavior;
    public ProjectileBehavior ProjectileBehavior;
    public int numberOfProjectilePerPattern = 5;
    public float WaitTimeBetweenProjectile = 1;
    private float _currentPatternTimer;
    private PlayerDamageableEntity _playerDamageableEntity;
    private bool _isAttacking = false;
    private List<BossPattern> _pattern = new List<BossPattern>();
    private Transform _selectedPattern;
    private PlayerDamageableEntity _playerEntity;
    private Transform _targetPlayerPosition = null;


    void Start()
    {
        _currentPatternTimer = TriggerAttackTimer;
        _playerDamageableEntity = FindObjectOfType<PlayerDamageableEntity>();
        _pattern = FindObjectsOfType<BossPattern>().ToList();
        _playerEntity = FindObjectOfType<PlayerDamageableEntity>();
        if (_playerEntity != null)
        {
            _targetPlayerPosition = _playerEntity.transform;
        }
        SelectRandomPattern();
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
                if (Vector2.Distance(transform.position, _targetPlayerPosition.position) < entity.Range)
                {
                    Animator.SetBool("Walking", false);
                    TriggerAttack();
                    _currentPatternTimer = TriggerAttackTimer;
                }
            }
            if (_isAttacking == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, _selectedPattern.position, entity.MoveSpeed * Time.deltaTime);
                Animator.SetBool("Walking", true);
                Flip(_selectedPattern);
                if (Vector2.Distance(transform.position, _selectedPattern.position) <= 0.1f)
                {
                    SelectRandomPattern();
                }
            }
            if (_isAttacking == true)
            {
                Flip(_playerDamageableEntity.transform);
            }
        }
    }

    void TriggerAttack()
    {
        int randomNbr = Random.Range(0, 2);
        if (randomNbr == 0)
            StartCoroutine(AttackPatternOne());
        else
            StartCoroutine(AttackPatternTwo());
        SelectRandomPattern();
    }

    private void SelectRandomPattern()
    {
        _selectedPattern = _pattern[Random.Range(0, _pattern.Count)].transform;
    }

    public void Flip(Transform transformToCheck)
    {
        if (transformToCheck.transform.position.x > this.transform.position.x)
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
        int numberOfProjectile = numberOfProjectilePerPattern;
        entity.Animator.Play("Special");
        _isAttacking = true;
        while (numberOfProjectile > 0)
        {
            BossProjectileBehavior bossProjectileBehavior = Instantiate(BossProjectileBehavior, PatternOnePos.position, Quaternion.identity, null);
            bossProjectileBehavior.Setup(entity);
            numberOfProjectile--;
            yield return new WaitForSeconds(WaitTimeBetweenProjectile);
            _isAttacking = false;
        }
    }

    IEnumerator AttackPatternTwo()
    {
        int numberOfProjectile = numberOfProjectilePerPattern;
        _isAttacking = true;
        while (numberOfProjectile > 0)
        {
            ProjectileBehavior pBeh = Instantiate(ProjectileBehavior, transform.position, Quaternion.identity, null);
            Vector3 targetDirection = (_playerDamageableEntity.transform.position - transform.position).normalized;
            targetDirection.z = this.transform.position.z;
            float AngleRad = Mathf.Atan2(targetDirection.y, targetDirection.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            pBeh.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
            pBeh.Setup(entity, this.gameObject, targetDirection);
            entity.Animator.Play("Attack");
            numberOfProjectile--;
            yield return new WaitForSeconds(WaitTimeBetweenProjectile);
        }
        _isAttacking = false;
    }
}
