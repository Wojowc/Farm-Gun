using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform playerCamera;

    void LateUpdate()
    {
        transform.LookAt(transform.position + playerCamera.forward);
    }
}
