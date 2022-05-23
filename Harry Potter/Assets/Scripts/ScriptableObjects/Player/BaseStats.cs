using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HP
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Player Base Stats")]
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

        public int dashCount;

        public int meleeDmg;
    }
}