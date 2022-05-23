using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HP
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ability Stats")]
    public class AbilityStats : ScriptableObject
    {
        public string abilityName;

        public AbilityType abilityType;
        public enum AbilityType { LockOn, Aim, Area, OnSelf }

        public float projectileLifeTime;
        public float projectileSpeed;
        
        public GameObject areaOfEffect;

        public StatusEFX effect;
        public float duration;


    }
}
