using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float Speed = 10f;
    public float TimeToLive = 2.0f;
    private float _damageDone;
    private GameObject _belongTo;
    private Vector3 _direction;
    private bool _isSetup;

    void Update()
    {
        if (_isSetup == true)
        {
             transform.position += _direction.normalized * Speed * Time.deltaTime;
        }
    }

    public void Setup(float damageDone, GameObject belongTo, Vector3 direction)
    {
        _damageDone = damageDone;
        _belongTo = belongTo;
        _direction = direction;
        _isSetup = true;
        Destroy(this.gameObject, 2.0f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject triggeredObject = col.gameObject;
        if (triggeredObject != _belongTo)
        {
            DamageableEntity damageableEntity = triggeredObject.GetComponent<DamageableEntity>();
            if (damageableEntity != null)
            {
                damageableEntity.TakeDamage(_damageDone);
            }
        }
    }
}
