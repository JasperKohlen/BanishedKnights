using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/UnitStats")]
public class UnitStats : ScriptableObject
{
    public float aggroRange;
    public float atkRange;
    public float attackDmg;
    public float attackDelay;
    public float maxHealth;
    public float armor;
}
