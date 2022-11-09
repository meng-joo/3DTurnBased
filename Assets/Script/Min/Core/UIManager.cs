using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("오버레이 캔버스")]
    [SerializeField] private Canvas _canvas;

    [Space]
    [Header("페이드 이미지")]
    [SerializeField] public Image _fadeImage;

    [Space]
    [Header("UI오브젝트들")]
    [SerializeField] public Image _signImage;
    [SerializeField] public Button backButton;

    [Space]
    [Header("표지판UI 활성화?")]
    public bool isSignUp = false;

    public void SignUIOn()
    {
        _fadeImage.gameObject.SetActive(true);
        _fadeImage.DOFade(.75f, 0.4f);
        _signImage.transform.DOMoveY(1080 / 2, 0.6f);
        backButton.transform.DOMoveX(200, 0.6f);
        isSignUp = true;
    }

    public void SignUIOff()
    {
        _fadeImage.DOFade(0, 0.4f).OnComplete(() => _fadeImage.gameObject.SetActive(false));
        _signImage.transform.DOMoveY(-1080 / 2, 0.6f);
        backButton.transform.DOMoveX(-200, 0.6f);
        isSignUp = false;
    }
}
