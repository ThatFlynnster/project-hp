using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Base Stats")]
public class BaseStats : ScriptableObject
{
    public string chrName = "";
    public int hp = 250;

    public int atkDamage;
    public float atkSpeed;
    public float atkDuration;
    public AnimationCurve atkVelocity;

    public int atkStamina;
    public int defStamina;

    public int dashCount = 3;
}
