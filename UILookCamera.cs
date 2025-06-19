using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookCamera : MonoBehaviour
{
    private Camera refCamera;

    // Start is called before the first frame update
    void Start()
    {
        refCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(refCamera)
            transform.rotation = refCamera.transform.rotation;
    }
}
