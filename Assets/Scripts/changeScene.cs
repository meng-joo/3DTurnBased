using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class changeScene : MonoBehaviour
{
    public static changeScene instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene("Blacksmith's Forge 1");
        }
    }
    public void CH()
    {
           SceneManager.LoadScene("Blacksmith's Forge 1");
    }
}
