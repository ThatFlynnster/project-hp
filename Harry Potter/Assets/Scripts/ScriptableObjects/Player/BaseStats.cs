using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Base Stats")]
public class BaseStats : ScriptableObject
{
    public string chrName;
    public int hp;

    public int atkDamage;
    public float atkSpeed;
    public float atkDuration;
    public AnimationCurve atkVelocity;

    public float atkStamina;
    public float defStamina;

    public int meleeDamage;
    public int dashCount;
    public string passiveIdx;
}
