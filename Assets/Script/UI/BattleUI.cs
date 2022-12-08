using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUI : MonoBehaviour
{
    [Space]
    [Header("전투 UI들")]
    private MainModule mainModule;

    [Space]
    [Header("전투 UI들")]
    public Button TurnEnd;
    public Image skillPanel;
    public Image textBox;
    public Image skillBox;
    public Image behaveBox;
    public Image behaveTextBox;
    public GameObject quickInven;
    public TextMeshProUGUI winorlose;

    [Space]
    public GameObject skillCard;

    [Space]
    public List<GameObject> currentSkillCard = new List<GameObject>();
    public List<Skill> currentSkill = new List<Skill>();
    public List<Skill> playerSkill;

    [Space]
    public List<Button> behaveButtons;
    public Image[] turnImage = new Image[2];
    public Text behaveText;

    [Space]
    [Header("적을 알기 위한 배틀 메니져")]
    [SerializeField] private BattleManager battleManager;

    [Space]
    [Header("카드 개수")]
    public int cardCount = 0;

    [Space]
    public PoolManager poolM;
    public LocalPoolManager[] poolLocalM = new LocalPoolManager[2];


    public TextMeshProUGUI costTxt;
    public int cost = 3;
    public int maxCost = 3;

    public List<GameObject> costObj;

    public GameObject costPrefab;
    public Transform costParentTrm;

    #region 묘지와 덱픽업 관련변수들
    public List<Skill> cemeteryCardDeck = new List<Skill>();

    public int cemeteryCardCnt = 0;

    public GameObject pickCard;
    public GameObject cemeteryCard;

    public GameObject cardPrefab;

    public Transform pickcardParnetTrm;

    public Transform cemetrycardParnetTrm;

    public bool isCemetery;


    public GameObject cemetryBtn;
    public GameObject pickCardBtn;
    #endregion

    private void Start()
    {
        mainModule = GameObject.Find("Player").GetComponent<MainModule>();

        for (int i = 0; i < skillPanel.transform.childCount; i++)
        {
            behaveButtons.Add(skillPanel.transform.GetChild(i).GetComponent<Button>());
        }

        behaveText = behaveTextBox.transform.GetChild(0).GetComponent<Text>();
        turnImage[0] = behaveTextBox.transform.GetChild(1).GetComponent<Image>();
        turnImage[1] = behaveTextBox.transform.GetChild(2).GetComponent<Image>();

        poolM = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        poolLocalM[0] = poolM.transform.GetChild(0).GetComponent<LocalPoolManager>();
        poolLocalM[1] = poolM.transform.GetChild(1).GetComponent<LocalPoolManager>();

        TurnEnd.onClick.AddListener(() => OnClickTurnEnd());
    }

    public void SetBattleUI()
    {

        SetActiveButton(false);
        skillPanel.transform.DOLocalMoveX(700, 0.6f);
        behaveTextBox.transform.DOLocalMoveY(478, 0.7f);
        TurnEnd.transform.DOLocalMoveX(835, 2f);
        SetQuickInven();
        StartCoroutine(MoveBehaveButtons(true));

        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(0.7f);
        seq.Append(turnImage[0].DOFade(1, 0.6f));
        seq.Join(turnImage[1].DOFade(1, 0.6f));
        seq.AppendCallback(() => SetChangeTurn(battleManager.SetTurn()));
        seq.AppendInterval(0.8f);
        seq.AppendCallback(() => OnClickSkill());
    }

    public void GetBackBattleUI()
    {
        behaveTextBox.transform.DOLocalMoveY(604, 0.7f);

        TurnEnd.transform.DOLocalMoveX(835, 1f);

        //skillPanel.transform.DOLocalMoveX(700, 0.6f);
        quickInven.transform.DOMove(new Vector3(60, 80, 0), 0.4f);
        turnImage[0].DOFade(1, 0.6f);
        turnImage[1].DOFade(1, 0.6f);
    }
    public void SetChangeTurn(bool isPlayerTurn)
    {
        Sequence seq = DOTween.Sequence();

        //seq.AppendCallback(() => SetActiveButton(false));
        if (isPlayerTurn)
        {
            for (int i = 0; i < cardCount; i++)
            {
                SetSkillInfo();
            }

            cost = 3;
            Transform[] childList = costParentTrm.GetComponentsInChildren<Transform>();

            if (childList != null)
            {
                for (int i = 1; i < childList.Length; i++)
                {
                    Destroy(childList[i].gameObject);
                }
            }

            for (int i = 0; i < cost; i++)
            {
                GameObject obj = Instantiate(costPrefab, transform.position, Quaternion.identity);
                obj.transform.SetParent(costParentTrm.transform);
                obj.SetActive(true);

                obj.transform.localPosition = new Vector3(0f + (2*i), 0f, 0f + i);
                obj.transform.localScale = Vector3.one;
                obj.transform.Rotate(new Vector3(0, 0f, 0f));

                costObj.Add(obj);
            }

            turnImage[0].DOFade(1, 2.4f);
            turnImage[0].transform.DOScale(1.4f, 1.2f);
            turnImage[1].DOFade(0.13f, 2.4f);
            turnImage[1].transform.DOScale(0.7f, 1.2f);

            seq.Append(behaveText.transform.DOLocalMoveY(170, 0.4f));
            seq.AppendCallback(() =>
            {
                behaveText.color = Color.yellow;
                behaveText.fontSize = 60;
                behaveText.text = "Player Turn";
            });
            seq.Append(behaveText.transform.DOLocalMoveY(0, 0.4f));
        }

        else
        {
            turnImage[1].DOFade(1, 2.4f);
            turnImage[1].transform.DOScale(1.4f, 1.2f);
            turnImage[0].DOFade(0.13f, 2.4f);
            turnImage[0].transform.DOScale(0.7f, 1.2f);

            seq.Append(behaveText.transform.DOLocalMoveY(170, 0.4f));
            seq.AppendCallback(() =>
            {
                behaveText.color = Color.red;
                behaveText.fontSize = 60;
                behaveText.text = "Enemy Turn";
            });
            seq.Append(behaveText.transform.DOLocalMoveY(0, 0.4f));
        }

        //seq.AppendInterval(1f);
        //seq.AppendCallback(() => SetActiveButton(true));
    }

    public void SetQuickInven()
    {
        quickInven.transform.DOMove(new Vector3(640, -70, 0), 0.8f);
    }

    IEnumerator MoveBehaveButtons(bool isOn, bool istextBox = false)
    {
        int weight = isOn ? 0 : 1;

        for (int i = 0; i < behaveButtons.Count; ++i)
        {
            behaveButtons[i].transform.DOLocalMoveX(weight * 504, 0.3f);
            yield return new WaitForSeconds(0.08f);
        }

        //yield return new WaitForSeconds(0.6f);
        if (istextBox) SetUIBox(textBox, istextBox);
    }

    public void SetActiveButton(bool isActive)
    {
        for (int i = 0; i < behaveButtons.Count; ++i)
        {
            behaveButtons[i].interactable = isActive;
        }
        TurnEnd.interactable = isActive;
    }

    private void SetUIBox(Image _boxui, bool isActive)
    {
        if (isActive) _boxui.transform.DOLocalMoveX(-256, 0.4f);
        else _boxui.transform.DOLocalMoveX(1686, 0.4f);
    }

    public void OnClickSkill()
    {
        StartCoroutine(MoveBehaveButtons(true, false));
        SetUIBox(textBox, false);
        SetQuickInven();
        SpawnCard();
    }

    public void OnClickInfo()
    {
        ClearCard(1);
        SetQuickInven();
        StartCoroutine(MoveBehaveButtons(true, true));
    }

    public void OnClickItem()
    {
        Sequence seq = DOTween.Sequence();
        ClearCard(2);
        SetUIBox(textBox, false);

        seq.AppendInterval(0.5f);
        seq.Append(quickInven.transform.DOMove(new Vector3(60, 80, 0), 0.4f));
        //StartCoroutine(MoveBehaveButtons(true, false));
    }

    public void OnClickRun()
    {
        SetActiveButton(false);
        SetUIBox(skillBox, false);
        StartCoroutine(MoveBehaveButtons(false));

        //전투 끝 + ui 초기화 + 적 없애기 + 
    }

    public void GameEnd(string isWin)
    {
        Sequence seq = DOTween.Sequence();

        winorlose.text = isWin;
        seq.AppendCallback(() =>
        {
            SetActiveButton(false);
            SetUIBox(skillBox, false);
            StartCoroutine(MoveBehaveButtons(false));
            skillPanel.transform.DOLocalMoveX(1224, 0.6f);
            behaveTextBox.transform.DOLocalMoveY(656, 0.7f);
            TurnEnd.transform.DOLocalMoveX(1084.5f, 2f);
        });
        seq.Append(winorlose.DOFade(1, 0.3f));
        seq.Join(winorlose.transform.DOScale(1.2f, 0.4f));
        //seq.Append(winorlose.DOFade)

    }

    public void OnClickTurnEnd()   //여기 턴 넘어가는 곳
    {
        SetChangeTurn(false);
        ClearCard(10);
        battleManager.StartCoroutine("ChangeTurn", false);
        ClearSkill();
    }

    private void ClearCard(int num)
    {
        SetActiveButton(false);

        for (int i = 0; i < currentSkillCard.Count; i++)
        {
            currentSkillCard[i].transform.SetParent(poolLocalM[0].transform);
            PoolManager.Instance.Push(PoolType.Card, currentSkillCard[i]);
        }
        currentSkillCard.Clear();
        SetUIBox(skillBox, false);
        if (num < 10)
        {
            SetActiveButton(true);
            behaveButtons[num].interactable = false;
        }
    }

    private void ClearSkill()
    {
        int left = currentSkill.Count;

       

        for (int i = 0; i < currentSkill.Count; i++)
        {
            cemeteryCardDeck.Add(currentSkill[i]);
        }
        currentSkill.Clear();

        //for (int i = 0; i < left; i++)
        //{
        //    playerSkill.Remove(playerSkill[0]);
        //}
    }

    private void SpawnCard()
    {
        Sequence seq = DOTween.Sequence();

        SetActiveButton(false);

        for (int i = 0; i < cardCount; i++)
        {
            float xpos = -550 + i * 275;
            float ypos = -400;

            GameObject card = skillCard;

            currentSkillCard.Add(PoolManager.Instance.Pop(PoolType.Card).gameObject);//Instantiate(card, skillBox.transform);//new Vector3(xpos, ypos, 0f), Quaternion.identity);
            currentSkillCard[i].transform.SetParent(skillBox.transform);
            currentSkillCard[i].transform.localPosition = new Vector3(0, ypos, 0);
        }

        seq.AppendCallback(() => SetUIBox(skillBox, true));
        for (int i = 0; i < cardCount; i++)
        {
            SetCardInfo(i);
        }
        seq.AppendInterval(0.4f);

        seq.AppendCallback(() =>
        {
            SetActiveButton(true);
            behaveButtons[0].interactable = false;
        });
    }

    public void SetSkillInfo()
    {
        if (playerSkill.Count < 1)
            SetDeck();

        currentSkill.Add(playerSkill[0]);
        playerSkill.Remove(playerSkill[0]);
    }

    public void SetCardInfo(int i)
    {
        currentSkillCard[i].GetComponent<SkillCard>().SetSkillCard(currentSkill[i]);
    }

    private void SetDeck()
    {
     
        if (isCemetery)
        {
            for (int i = 0; i < cemeteryCardDeck.Count; i++)
            {
                playerSkill.Add(cemeteryCardDeck[i]);
            }

            cemeteryCardDeck.Clear();
            Shuffle(cemeteryCardDeck);
            Debug.Log("묘지에꺼 잘감");
        }
        else
        {
            for (int i = 0; i < mainModule.playerDataSO._skills.Length; i++)
            {
                playerSkill.Add(mainModule.playerDataSO._skills[i]);
            }
            isCemetery = true;
            Shuffle(playerSkill);
        }


    }

    //private void SetSkill()
    //{
    //    for (int i = 0; i < cardCount; i++)
    //    {
    //        currentSkill.Add(playerSkill[i]);
    //    }
    //}

    public void SpawnSkillEffectText(string dmg, Color32 color, Vector3 pos)
    {
        Sequence seq = DOTween.Sequence();
        SkillEffectUI text = PoolManager.Instance.Pop(PoolType.UI).GetComponent<SkillEffectUI>();

        Vector3 randPos = new Vector3(Random.Range(-10, 10f), Random.Range(-10, 10f), 0);

        seq.AppendCallback(() =>
        {
            text.transform.SetParent(skillBox.transform.parent);
            text.transform.position = pos + randPos;
            text.SetText(dmg, color, pos);
        });

        seq.AppendInterval(2.7f);
        seq.AppendCallback(() => PoolManager.Instance.Push(PoolType.UI, text.gameObject));
    }
    public void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, mainModule.playerDataSO._skills.Length);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public int GetCardCount()
    {
        return 5;
    }
    public void OnPickCard()
    {
        pickCard.SetActive(true);

        RectTransform[] childList = pickcardParnetTrm.GetComponentsInChildren<RectTransform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                Destroy(childList[i].gameObject);
            }
        }

        for (int i = 0; i < playerSkill.Count; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity).gameObject;
            cardObj.GetComponent<SkillCard>().SetSkillCard( playerSkill[i]);
            cardObj.transform.SetParent(pickcardParnetTrm);
            cardObj.SetActive(true);
        }
    }
    public void OffPickCemetryCard()
    {
        pickCard.SetActive(false);
        cemeteryCard.SetActive(false);
    }
    public void OnCemeteryCard()
    {
        cemeteryCard.SetActive(true);

        RectTransform[] childList = cemetrycardParnetTrm.GetComponentsInChildren<RectTransform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                Destroy(childList[i].gameObject);
            }
        }

        for (int i = 0; i < cemeteryCardDeck.Count; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity).gameObject;
            cardObj.GetComponent<SkillCard>().SetSkillCard(cemeteryCardDeck[i]);
            cardObj.transform.SetParent(cemetrycardParnetTrm);
            cardObj.SetActive(true);
        }
    }
    public void SetCost(int m_cost)
    {
        for (int i = 0; i < m_cost; i++)
        {
            DoFade(0, 1, 1, costObj[i].transform.Find("NGon002").GetComponent<MeshRenderer>().material);
            costObj.Remove(costObj[i]);        
        }
    }
    public void DoFade(float start, float dest, float time, Material dissolveMat)
    {
         DOTween.To(() => start, x => { start = x; dissolveMat.SetFloat("_Dissolve", start); }, dest, time).SetUpdate(true);
    }
}