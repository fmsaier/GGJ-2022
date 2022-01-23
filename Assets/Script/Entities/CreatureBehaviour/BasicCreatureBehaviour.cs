using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCreatureBehaviour : MonoBehaviour
{
    public DamageableEntity entity;
    public Animator Animator;
    public AnimationClip AttackClip;
    private PlayerDamageableEntity _playerEntity;
    private Transform _targetPlayerPosition = null;
    private bool _isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerEntity = FindObjectOfType<PlayerDamageableEntity>();
        if (_playerEntity != null)
        {
            _targetPlayerPosition = _playerEntity.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerEntity != null && entity != null)
        {
            if (entity.IsInControl == true && _isAttacking == false)
            {
                if (Vector2.Distance(transform.position, _targetPlayerPosition.position) > entity.Range)
                {
                    float step = entity.MoveSpeed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, _targetPlayerPosition.position, step);
                }
                else
                {
                    StartCoroutine(AttackCoroutine());
                }
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        _isAttacking = true;
        float t = AttackClip.length;
        Animator.Play("Attack");
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (entity.IsInControl)
        {
            //check if still in range
            if (Vector2.Distance(transform.position, _targetPlayerPosition.position) < entity.Range)
            {
                _playerEntity.TakeDamage(entity.Damage);
                Debug.Log("Still in range");
            }
            else
            {
                Debug.Log("Not in range");
            }
        }
        _isAttacking = false;
    }
}
