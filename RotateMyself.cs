using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMyself : MonoBehaviour
{
    float maxRotateSpeed = 20f;
    float currentRotateSpeed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentRotateSpeed = Tank.GetSpeedProportion * maxRotateSpeed;
        transform.Rotate(transform.right, currentRotateSpeed,Space.World);
    }
}
