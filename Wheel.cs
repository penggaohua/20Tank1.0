using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public GameObject[] wheels;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        StartCoroutine("move");
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator move()
    {
        while (true)
        {
            Vector3 temp = wheels[0].transform.position;
            for (int i = 0; i < wheels.Length - 1; i++)
            {
                print(i);
                wheels[i].transform.position = wheels[i + 1].transform.position;
            }
            wheels[wheels.Length - 1].transform.position = temp;
            yield return new WaitForSeconds(2f);

        }
    }
}
