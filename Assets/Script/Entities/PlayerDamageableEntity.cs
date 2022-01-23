using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageableEntity : DamageableEntity
{
    public void AddStat(PermanentBuffScriptable permanentBuffScriptable)
    {
        Damage += permanentBuffScriptable.AttackModifier;
        MaxLife += permanentBuffScriptable.LifeModifier;
        Heal(permanentBuffScriptable.LifeModifier);
        MoveSpeed += permanentBuffScriptable.SpeedModifier;
        KnockBackPower += permanentBuffScriptable.KnockBackModifier;
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        GameUIManager.Instance.UpdateDarkness(CurrentLife, MaxLife);
        //C PAS bo
        transform.GetChild(0).GetComponent<Animator>().Play("Hit");
    }

    public override void Heal(float amount)
    {
        base.Heal(amount);
        GameUIManager.Instance.UpdateDarkness(CurrentLife, MaxLife);
    }

    public override void Die()
    {
        base.Die();
        GameManager.Instance.FinishGame();
        GameUIManager.Instance.ShowDefeat();
    }
}
