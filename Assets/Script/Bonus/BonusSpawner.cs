using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public Transform AttackParent;
    public Transform LifeParent;
    public Transform SpeedParent;
    public Transform KnockBackParent;

    public BonusPickable AttackPickable;
    public BonusPickable LifePickable;
    public BonusPickable SpeedPickable;
    public BonusPickable KnockBackPickable;

    private List<BonusPickable> _list = new List<BonusPickable>();
    public void CreatePermanentBonus()
    {
        BonusPickable atk = Instantiate(AttackPickable, AttackParent.position, Quaternion.identity, AttackParent);
        BonusPickable life = Instantiate(LifePickable, LifeParent.position, Quaternion.identity, LifeParent);
        BonusPickable speed = Instantiate(SpeedPickable, SpeedParent.position, Quaternion.identity, SpeedParent);
        BonusPickable knock = Instantiate(KnockBackPickable, KnockBackParent.position, Quaternion.identity, KnockBackParent);
        _list.Add(atk);
        _list.Add(life);
        _list.Add(speed);
        _list.Add(knock);
    }

    public void RemoveBonus()
    {
        foreach (BonusPickable bonusPickable in _list)
        {
            bonusPickable.EndBatch();
        }
        _list.Clear();
        if (AttackParent.childCount > 0)
        {
            Destroy(AttackParent.GetChild(0).gameObject, 1f);
        }
        if (LifeParent.childCount > 0)
        {
            Destroy(LifeParent.GetChild(0).gameObject, 1f);
        }
        if (SpeedParent.childCount > 0)
        {
            Destroy(SpeedParent.GetChild(0).gameObject, 1f);
        }
        if (KnockBackParent.childCount > 0)
        {
            Destroy(KnockBackParent.GetChild(0).gameObject, 1f);
        }
    }
}
