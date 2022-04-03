using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparitionFX : MonoBehaviour
{
    private Transform cameraMain;

    private float duration = 0.4f;

    [SerializeField]
    private Material apparitionMat;

    [SerializeField]
    private AnimationCurve strengthCurve;
    private float distortionStrength;
    [SerializeField]
    private AnimationCurve speedCurve;
    private float rotationSpeed;
    [SerializeField]
    private AnimationCurve scaleCurve;
    private float scale;

    private float curveTime;
    private string strength, speed;

    private void OnEnable()
    {
        cameraMain = Camera.main.transform;
        curveTime = 0f;
        strength = "Vector1_4318a15795c44f36ace4516e5d370ed3";
        speed = "Vector1_07344c060c154b8db4ca316c7dfa1178";
        Destroy(gameObject, duration);
    }

    void Update()
    {
        transform.forward = cameraMain.position - transform.position;

        curveTime += Time.deltaTime * 2.5f;
        if (curveTime > 1) curveTime = 1f;

        distortionStrength = strengthCurve.Evaluate(curveTime);
        rotationSpeed = speedCurve.Evaluate(curveTime);
        scale = scaleCurve.Evaluate(curveTime);

        apparitionMat.SetFloat(strength, distortionStrength);
        apparitionMat.SetFloat(speed, rotationSpeed);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
