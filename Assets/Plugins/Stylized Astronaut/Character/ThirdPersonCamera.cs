using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform lookAt;
    public Transform camTransform;
    public float distance = 5.0f;

    public float yOffset = 2f;

    private float currentX = 0.0f;

    private void Start()
    {
        camTransform = transform;
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(35f, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(new Vector3(lookAt.position.x, lookAt.position.y + yOffset, lookAt.position.z));
    }
}
