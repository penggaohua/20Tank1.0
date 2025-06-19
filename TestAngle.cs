using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAngle : MonoBehaviour
{
    float x;
    public Transform targetTransform;
    // Start is called before the first frame update
    void Start()
    {
        //x = 90;

        Vector2 v = Random.insideUnitCircle;
        print(v);
        // Vector3 randomFireRotaion = new Vector3(transform.rotation.x + v.x * rate, transform.rotation.y + v.y * rate, transform.rotation.z);
        transform.Rotate(Vector3.right, 15f);
        transform.Rotate(Vector3.up, 15f);
        Invoke("Test", 10f);

    }

    // Update is called once per frame
    void Update()
    {
        //x = Mathf.Lerp(0, x, 0.1f);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetTransform.rotation, Time.deltaTime );
    }

    void Test()
    {
        print("this is a test");
    }


}
