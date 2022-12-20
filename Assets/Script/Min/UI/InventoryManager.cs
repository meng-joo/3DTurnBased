using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryManager : MonoBehaviour
{
    public bool isInven = false;
    public GameObject inventory;

    [SerializeField] private RectTransform leftInvenUI;
    [SerializeField] private RectTransform rightInvenUI;

    [SerializeField] private Button skillBtn;
    [SerializeField] private Button itemBtn;

    public GameObject[] skillUI;
    public GameObject[] itemUI;

    private MainModule mainModule;

    [SerializeField] private InvenSkill invenSkill;



    public GameObject useTab;
    public GameObject exitBtn;
    public GameObject useTapOffBtn;
    public GameObject relic;
    public GameObject relicExplain;
    private void Awake()
    {
        mainModule = FindObjectOfType<MainModule>();
    }
    void Update()
    {
        if (mainModule.canInven == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isInven = !isInven;
            if (!isInven)
            {
                InitInvenUI();
            }
            else
            {
                inventory.gameObject.SetActive(isInven);
                AppearInvenUIEffect();
            }
        }
    }
    public void OpenInven()
    {
        isInven = !isInven;
        if (!isInven)
        {
            InitInvenUI();
        }
        else
        {
            inventory.gameObject.SetActive(isInven);
            AppearInvenUIEffect();
            exitBtn.SetActive(true);
            useTapOffBtn.SetActive(true);
        }
    }
    /// <summary>
    /// 인벤 나타날때 다트윈 효과
    /// </summary>
    public void AppearInvenUIEffect()
    {
        Sequence seq = DOTween.Sequence();

        invenSkill.DefaultCreate();
        //seq.AppendCallback(() =>
        //{
        //    if (gameInfoSO.isGameStart)
        //    {
        //        invenSkill.CreateFirst();
        //    }
        //    else
        //    {
        //        invenSkill.DefaultCreate();
        //    }
        //    gameInfoSO.isGameStart = false;
        //});

        seq.Append(leftInvenUI.DOLocalMoveX(-800f, 0.5f)).SetUpdate(true)
            .Join(rightInvenUI.DOLocalMoveX(800f, 0.5f)).SetUpdate(true)
            .Join(itemBtn.transform.DOLocalMoveX(330, 0.5f)).SetUpdate(true)
            .Join(skillBtn.transform.DOLocalMoveX(640, 0.5f)).SetUpdate(true);

        seq.Insert(0.2f, leftInvenUI.GetComponentInChildren<Image>().DOFade(1f, 0.5f));
        seq.Insert(0.2f, rightInvenUI.GetComponentInChildren<Image>().DOFade(1f, 0.5f));

        seq.Insert(0.3f, itemBtn.GetComponent<Image>().DOFade(1f, 0.5f));
        seq.Insert(0.3f, skillBtn.GetComponent<Image>().DOFade(1f, 0.5f));
        mainModule.canMove = true;
        useTab.SetActive(false);
        relic.SetActive(false);
        relicExplain.SetActive(false);


    }

    /// <summary>
    /// 인벤 들어갈때 다트윈 효과
    /// </summary>
    public void InitInvenUI()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(leftInvenUI.DOLocalMoveX(-2000f, 0.5f)).SetUpdate(true)
            .Join(rightInvenUI.DOLocalMoveX(2000f, 0.5f)).SetUpdate(true)
            .Join(itemBtn.transform.DOLocalMoveX(1510f, 0.3f)).SetUpdate(true)
            .Join(skillBtn.transform.DOLocalMoveX(1510f, 0.3f)).SetUpdate(true);


        seq.Insert(0.2f, leftInvenUI.GetComponentInChildren<Image>().DOFade(0f, 0.5f));
        seq.Insert(0.2f, rightInvenUI.GetComponentInChildren<Image>().DOFade(0f, 0.5f));


        seq.Insert(0.3f, itemBtn.GetComponent<Image>().DOFade(0f, 0.5f));
        seq.Insert(0.3f, skillBtn.GetComponent<Image>().DOFade(0f, 0.5f));

        seq.AppendCallback(() => OnItemUI());                    //<<----여기도 위에 있던거 밑으로 스퀀스 내림
        //seq.AppendCallback(() => invenSkill.DeleteCard());               
        invenSkill.DeleteCard();
        mainModule.canMove = false;
        exitBtn.SetActive(false);
        useTapOffBtn.SetActive(false);
        useTab.SetActive(false);
        relic.SetActive(true);
    }
    public void OnSkillUI()
    {

        for (int i = 0; i < skillUI.Length; i++)
        {
            skillUI[i].SetActive(true);
        }
        for (int i = 0; i < itemUI.Length; i++)
        {
            itemUI[i].SetActive(false);
        }
    }

    public void OnItemUI()
    {

        for (int i = 0; i < skillUI.Length; i++)
        {
            skillUI[i].SetActive(false);
        }
        for (int i = 0; i < itemUI.Length; i++)
        {
            itemUI[i].SetActive(true);
        }
    }

    public void OutClick()
    {
        useTab.gameObject.SetActive(false);
        useTapOffBtn.gameObject.SetActive(false);
    }
}
