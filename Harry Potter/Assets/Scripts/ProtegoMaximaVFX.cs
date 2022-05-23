using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HP
{
    public class ProtegoMaximaVFX : MonoBehaviour
    {
        [SerializeField] private AnimationCurve scaleCurve;
        [SerializeField] private AnimationCurve rotationCurve;
        private float curveTime = 0f;
        private float rotationSpeed;
        private float scale;
        
        private void OnEnable()
        {
            
        }

        private void Update()
        {            
            curveTime += Time.deltaTime;
            rotationSpeed = -rotationCurve.Evaluate(curveTime);
            scale = scaleCurve.Evaluate(curveTime * 1.75f);

            Vector3 rotation = new Vector3(0f, 0f, rotationSpeed);
            this.transform.Rotate(rotation);
            Vector3 currentScale = new Vector3(scale, scale, scale);
            this.transform.localScale = currentScale;
        }
    }
}
