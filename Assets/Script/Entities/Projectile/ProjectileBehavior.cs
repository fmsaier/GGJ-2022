using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public List<Sprite> ProjectileSpriteList = new List<Sprite>();
    public float Speed = 10f;
    public float TimeToLive = 2.0f;
    [Header("Knockback parameter")]
    public bool ApplyKnockBack = false;
    public float KnockbackDuration = 0.5f;
    private DamageableEntity _entity;
    private GameObject _belongTo;
    private Vector3 _direction;
    private bool _isSetup;
    private List<DamageableEntity> _hitList = new List<DamageableEntity>();

    void Update()
    {
        if (_isSetup == true)
        {
            transform.position += _direction.normalized * Speed * Time.deltaTime;
        }
    }

    public void Setup(DamageableEntity entity, GameObject belongTo, Vector3 direction)
    {
        _entity = entity;
        _belongTo = belongTo;
        _direction = direction;
        _isSetup = true;
        if (ProjectileSpriteList != null && ProjectileSpriteList.Count > 0)
        {
            GetComponent<SpriteRenderer>().sprite = ProjectileSpriteList[Random.Range(0, ProjectileSpriteList.Count)];
        }
        Destroy(this.gameObject, TimeToLive);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject triggeredObject = col.gameObject;
        if (triggeredObject != _belongTo)
        {
            DamageableEntity damageableEntity = triggeredObject.GetComponent<DamageableEntity>();
            if (damageableEntity != null && !_hitList.Contains(damageableEntity))
            {
                _hitList.Add(damageableEntity);
                damageableEntity.TakeDamage(_entity.Damage);
                if (ApplyKnockBack == true)
                {
                    damageableEntity.ApplyKnockBack(KnockbackDuration,  _entity.KnockBackPower, (col.transform.position - transform.position).normalized);
                }
            }
        }
    }

    void OnDestroy()
    {
        _hitList.Clear();
    }
}
