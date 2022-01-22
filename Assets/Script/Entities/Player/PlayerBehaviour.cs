using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Animator Animator;
    public ProjectileBehavior projectile;
    public DamageableEntity entity;
    
    void Update()
    {
        if (GameManager.Instance.IsGameStarted == true && GameManager.Instance.IsPlayerInControl == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos = (targetPos - transform.position).normalized;
                targetPos.z = this.transform.position.z;
                Attack(targetPos);
            }
        }
    }

    void Attack(Vector3 targetPos)
    {
        Animator.Play("Attack");
        ProjectileBehavior createdProj = Instantiate(projectile, transform.position, Quaternion.identity, null);
        createdProj.Setup(entity.Damage, this.gameObject, targetPos);
    }
}
