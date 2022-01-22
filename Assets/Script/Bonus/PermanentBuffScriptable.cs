using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PermanentBuff", order = 1)]
public class PermanentBuffScriptable : ScriptableObject
{
    public float AttackModifier = 1;
    public float LifeModifier = 1;
    public float SpeedModifier = 1;

    public string Description = "This is a buff";
}
