using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePingpong : MonoBehaviour
{
    float value = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        value = Mathf.PingPong(Time.deltaTime, 5);
      //  print(value);
        //transform.position =new Vector3(value, transform.position.y, transform.position.z);
    }
}
