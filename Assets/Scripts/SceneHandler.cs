using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
    
public class SceneHandler : MonoBehaviour
{
    public Image fadeOutImage = null;

    public static SceneHandler Instance;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Game")
        {
            LoadGameCutScene();
        }
        else if (scene.name == "Menu")
        {

        }
    }
    public void LoadGameCutScene()
    {
        StartCoroutine(GameStartCutScene());

    }
    IEnumerator GameStartCutScene()
    {
        fadeOutImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        fadeOutImage.gameObject.SetActive(false);
    }

}
