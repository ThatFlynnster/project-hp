using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootIKController : MonoBehaviour
{
    [SerializeField]
    private Transform body;

    [SerializeField]
    private float footSpacing;

    [SerializeField]
    private float stepDistance;

    private Vector3 newPosition;

    void Update()
    {
        Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit info, 10))
        {
            if (Vector3.Distance(newPosition, info.point) > stepDistance)
            {
                newPosition.x = info.point.x;
                newPosition.y = info.point.y;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition, 0.5f);
    }
}