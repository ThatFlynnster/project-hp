using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HP
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Status Efx")]
    public class StatusEFX : ScriptableObject
    {
        public EffectType effectType;
        public enum EffectType { staticHP, tmpHP, mvmSpd, stamina } 
        public float duration;
        public float mainModifier;       
    }
}
