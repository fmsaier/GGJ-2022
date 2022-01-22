using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCreatureBehaviour : MonoBehaviour
{
    public DamageableEntity entity;

    private PlayerDamageableEntity _playerEntity;
    private Transform _targetPlayerPosition = null;

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
            if (Vector2.Distance(transform.position, _targetPlayerPosition.position) > entity.Range)
            {
                float step = entity.MoveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, _targetPlayerPosition.position, step);
            }
            else
            {
                Debug.Log("InRange");
            }
        }
    }
}
