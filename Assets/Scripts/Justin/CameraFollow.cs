using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]private Transform target;
    private float smoothFactor = 0.2f;

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position;

        Vector3 vec = new(0, 0, 0);

        Vector3 smoothPositon = Vector3.SmoothDamp(transform.position, targetPosition, ref vec, smoothFactor);

        smoothPositon.z = -10;

        transform.position = smoothPositon;
    }
}
