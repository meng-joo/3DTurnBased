using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class MenuManager : MonoBehaviour
{
    public Image fadeUI;


    public TextMeshProUGUI tip;

    public TipSO tipSo;

    public Image maskTargetImage;

    public Image maskImage;

    public Button a;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RandomTipText()
    {
        int rand = Random.Range(0, tipSo.tipStr.Length);
        tip.text = $"Tip: {tipSo.tipStr[rand]}";
        Debug.Log(rand);
    }
    public void StartInit()
    {
        Sequence seq = DOTween.Sequence();


        fadeUI.gameObject.SetActive(true);
        seq.Append(fadeUI.DOFade(1f, 2f));


        seq.AppendCallback(() =>
        {
            //Glitch.GlitchManager.Instance.StartSceneValue();
            SceneManager.LoadScene(1);
        });

        seq.AppendInterval(0.2f);

        seq.AppendCallback(() =>
        {
            RandomTipText();
        });

        #region 글자와 로고 화면에 오기
        seq.Append(maskImage.transform.DOMoveX(1700f, 0.4f));
        seq.AppendInterval(0.6f);
        seq.Append(tip.transform.DOMoveX(600, 0.5f));
        seq.AppendInterval(0.6f);
        seq.Append(maskTargetImage.transform.DOLocalMoveX(0f, 5f));
        seq.AppendInterval(0.5f);
        #endregion

        #region 글자와 로고 날려보내면서 투명도 없애기
        seq.Append(maskImage.transform.DOMoveX(2100, 0.93f));
        seq.Join(maskImage.DOFade(0, 0.7f));
        seq.Join(maskTargetImage.DOFade(0, 0.7f));
        seq.Append(tip.transform.DOMoveX(-600, 0.93f));
        seq.Join(tip.DOFade(0, 0.6f));
        #endregion

        #region 페이드 해제
        seq.AppendInterval(0.7f);
        seq.AppendCallback(() =>
        {
            a.gameObject.SetActive(false);
        });
        seq.Append(fadeUI.DOFade(0f, 1.5f));
        #endregion 
    }
}
