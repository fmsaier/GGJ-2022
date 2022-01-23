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

    public void CreatePermanentBonus()
    {
        Instantiate(AttackPickable, AttackParent.position, Quaternion.identity, AttackParent);
        Instantiate(LifePickable, LifeParent.position, Quaternion.identity, LifeParent);
        Instantiate(SpeedPickable, SpeedParent.position, Quaternion.identity, SpeedParent);
        Instantiate(KnockBackPickable, KnockBackParent.position, Quaternion.identity, KnockBackParent);
    }

    public void RemoveBonus()
    {
        if (AttackParent.childCount > 0)
        {
            Destroy(AttackParent.GetChild(0).gameObject);
        }
        if (LifeParent.childCount > 0)
        {
            Destroy(LifeParent.GetChild(0).gameObject);
        }
        if (SpeedParent.childCount > 0)
        {
            Destroy(SpeedParent.GetChild(0).gameObject);
        }
        if (KnockBackParent.childCount > 0)
        {
            Destroy(KnockBackParent.GetChild(0).gameObject);
        }
    }
}
