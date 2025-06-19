using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telescope : MonoBehaviour
{
    /// <summary>
    /// 用于控制望远镜的视野
    /// </summary>
    public float multiple=2f;
    private float initFov;
    private bool telescopeOpen = true;
    // Start is called before the first frame update

    public GameObject tele_image;

    void Awake()
    {
        initFov = Camera.main.fieldOfView;
       // print(initFov);
    }
    private void Start()
    {
        tele_image.gameObject.SetActive(false);
   
    }

    public void telescopeController()
    {
        Debug.Log("按了瞄准镜");
        //取异或来控制开与关
        telescopeOpen ^= true;
        if (telescopeOpen == false)
            ZoomView();
        else
            ZoomOut();
    }
    void ZoomView()
    {
        Camera.main.fieldOfView = initFov/multiple;
        tele_image.gameObject.SetActive(true);

    }

    void ZoomOut()
    {
        Camera.main.fieldOfView = initFov;
        tele_image.gameObject.SetActive(false);

    }
   

}
