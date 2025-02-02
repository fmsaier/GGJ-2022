using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileBehavior : MonoBehaviour
{
    public float PreparationTime = 1.5f;
    public float PreparationSpeed = 1.5f;
    public float IdleTime = 1;
    public float LockedOnSpeed = 20.0f;
    private PlayerDamageableEntity _playerDamageableEntity;
    private bool _canDamage = false;
    private Vector3 _preparationDirection;
    private float _currentPreparationTime;
    private float _currentIdleTime;
    private Vector3 _targetDirection;
    private DamageableEntity _entity;
    private bool _isSetup = false;

    void Start()
    {
        _currentPreparationTime = PreparationTime;
        _currentIdleTime = IdleTime;
        _preparationDirection = Random.insideUnitCircle.normalized;
        _playerDamageableEntity = FindObjectOfType<PlayerDamageableEntity>();
    }

    void Update()
    {
        if (_playerDamageableEntity != null && _isSetup == true)
        {
            if (_currentPreparationTime > 0)
            {
                _currentPreparationTime -= Time.deltaTime;
                transform.position += _preparationDirection * PreparationSpeed * Time.deltaTime;
            }
            else if (_currentIdleTime > 0)
            {
                _currentIdleTime -= Time.deltaTime;
            }
            else if (_canDamage == false)
            {
                _canDamage = true;
                _targetDirection = (_playerDamageableEntity.transform.position - transform.position).normalized;
                _targetDirection.z = this.transform.position.z;
                Destroy(gameObject, 3.0f);
            }
            else
            {
                transform.position += _targetDirection.normalized * LockedOnSpeed * Time.deltaTime;
            }
        }
    }

    public void Setup(DamageableEntity entity)
    {
        _entity = entity;
        _isSetup = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (_canDamage == true && col.GetComponent<PlayerDamageableEntity>() != null)
        {
            //TODO change ça
            _playerDamageableEntity.TakeDamage(_entity.Damage);
            Destroy(this.gameObject);
        }
    }
}
