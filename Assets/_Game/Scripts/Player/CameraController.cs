using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float value;

    private void LateUpdate()
    {
        Vector3 pos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, pos, value * Time.deltaTime);
    }
}
