using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Vector3 offset;
    private float z;

    private void Start()
    {
        z = transform.position.z;
    }

    void Update()
    {
        if(InputManager.Instance.CurrentTransform != null) { 
            transform.position = new Vector3(InputManager.Instance.CurrentTransform.position.x, InputManager.Instance.CurrentTransform.position.y, z); 
        }
    }
}
