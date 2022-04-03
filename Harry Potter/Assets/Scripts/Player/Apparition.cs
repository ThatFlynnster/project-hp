using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apparition : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve scaleCurve;
    private float scale;

    private float curveTime;

    void Update()
    {
        curveTime += Time.deltaTime * 2.5f;
        if (curveTime > 1) curveTime = 1f;

        scale = scaleCurve.Evaluate(curveTime);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
