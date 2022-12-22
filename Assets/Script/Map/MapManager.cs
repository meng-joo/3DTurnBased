using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MapManager : MonoBehaviour
{
    [Header("플레이어")]
    public MainModule _mainModule;
    public Canvas _playerCanvas;

    [Space]
    [Header("UI캔버스")]
    public GameObject maincanvas;
    public GameObject minicanvas;

    [Space]
    [Header("카메라")]
    public Camera miniMapCam;
    public Camera mainMapCam;

    [Space]
    [Header("미니맵이 켜져있는가?")]
    public bool isMiniMapUp;

    private Canvas loadingCanvas;

    private Image fadeUI;
    private TextMeshProUGUI tipText;
    private TextMeshProUGUI tip;
    private TipDataSO tipSo;
    private Image maskTargetImage;
    private TextMeshProUGUI maskImage;
    private Image barImage;



    private void Start()
    {
        isMiniMapUp = false;
        loadingCanvas = GameObject.Find("LoadingCanvas")?.GetComponent<Canvas>();
        if (loadingCanvas)
        {
            fadeUI = loadingCanvas.transform.GetChild(0).GetComponent<Image>();
            barImage = fadeUI.transform.GetChild(0).GetComponent<Image>();

            tipText = fadeUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            tip = fadeUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            maskImage = fadeUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

            maskTargetImage = maskImage.transform.GetChild(0).GetComponent<Image>();

            tipSo = AddressableManager.Instance.GetResource<TipDataSO>("Assets/SO/TipSO");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            miniMapCam.gameObject.SetActive(!isMiniMapUp);
            maincanvas.SetActive(isMiniMapUp);
            minicanvas.SetActive(!isMiniMapUp);
            mainMapCam.gameObject.SetActive(isMiniMapUp);

            _playerCanvas.gameObject.SetActive(!isMiniMapUp);
            _mainModule.canMove = !isMiniMapUp;
            isMiniMapUp = !isMiniMapUp;
        }
    }
    public void RandomTipText()
    {
        int rand = Random.Range(0, tipSo.tipStr.Length);
        tipText.text = $"{tipSo.tipStr[rand]}";
        Debug.Log(rand);
    }
    public void StartInit(int num)
    {
        Sequence seq = DOTween.Sequence();

        Vector3 pos = maskTargetImage.transform.position;

        seq.AppendCallback(() => fadeUI.gameObject.SetActive(true));
        seq.Append(fadeUI.DOFade(1f, 1.6f));

        seq.AppendCallback(() =>
        {
            //Glitch.GlitchManager.Instance.StartSceneValue();
            SceneManager.LoadScene(num);
        });

        seq.AppendInterval(0.2f);

        seq.AppendCallback(() =>
        {
            RandomTipText();
        });

        #region 글자와 로고 화면에 오기
        seq.Append(barImage.transform.DOMoveX((1920f / 2), 0.6f));
        seq.Join(barImage.DOFade(1, 0.5f));
        seq.Append(maskImage.transform.DOMoveX(1700f, 0.4f));
        seq.AppendInterval(0.3f);
        seq.Append(tip.transform.DOMoveX(94, 0.8f));
        seq.Append(tipText.transform.DOMoveX(280, 1.1f));
        seq.AppendInterval(0.6f);
        seq.Append(maskTargetImage.transform.DOLocalMoveX(0f, 5f));
        seq.AppendInterval(0.5f);
        #endregion

        #region 글자와 로고 날려보내면서 투명도 없애기
        seq.Append(maskImage.transform.DOMoveX(2100, 0.93f));
        //seq.Join(maskImage.DOFade(0, 0.7f));
        //seq.Join(maskTargetImage.DOFade(0, 0.7f));
        seq.Append(tipText.transform.DOMoveX(-700, 0.4f));
        //seq.Join(tipText.DOFade(0, 0.6f));
        seq.Append(tip.transform.DOMoveX(-600, 0.93f));
        //seq.Join(tip.DOFade(0, 0.6f));
        seq.Append(barImage.transform.DOMoveX(-(1920f / 2), 0.3f));
        seq.Join(barImage.DOFade(0, 0.2f));
        #endregion

        #region 페이드 해제
        seq.AppendInterval(0.5f);
        seq.Append(fadeUI.DOFade(0f, 1.5f));
        #endregion 

        seq.AppendCallback(() => maskTargetImage.transform.position = pos);

        seq.AppendCallback(() => fadeUI.gameObject.SetActive(false));
    }


    public void Ending()
    {
        if (_mainModule.playerDataSO.isClear == true)
        {
            GameObject.Find("UIManager").GetComponent<GameOverUI>().Init();
            SceneManager.LoadScene("Start");


            //ㅁ머ㅓ
        }
    }
}