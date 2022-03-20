using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    public Transform cameraTransform;

    void Update()
    {
        Vector3 newRotation = new Vector3(0, cameraTransform.transform.eulerAngles.y, 0);
        this.transform.eulerAngles = newRotation;
    }
}
