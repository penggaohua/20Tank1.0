using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToWorkShop()
    {
        SceneManager.LoadScene("workshop");
    }
    public void GoToLevel()
    {
        SceneManager.LoadScene("LevelScene");

    }

    public void GoToBattle()
    {
        SceneManager.LoadScene("MainScene");

    }



}
