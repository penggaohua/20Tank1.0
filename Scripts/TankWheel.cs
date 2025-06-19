using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWheel : MonoBehaviour
{
    AudioSource ad;
    public AudioClip tankMoveClip;
    public AudioClip tankIdleClip;

    float tankSpeed;
    // Start is called before the first frame update
    void Start()
    {
        ad = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        tankSpeed = Tank.Speed;
        //Debug.Log("坦克的即时速度"+tankSpeed);
        if (tankSpeed != 0)
        {
            ad.clip = tankMoveClip;
            if (ad.isPlaying == false)
            {
                ad.Play();
            }
            ad.pitch = Mathf.Abs(Tank.GetSpeedProportion)*0.5f+0.5f;
        }
        else
        {
            ad.clip = tankIdleClip;
            //if (ad.isPlaying == false)
             //   ad.Play();
        }
    }

}
