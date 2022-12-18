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
    public Image turnChangeImage_1;
    public Image turnChangeImage_2;
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI currentPlayerInfo;

    [Space]
    [Header("인벤스킬")]
    public SkillIInvenObj _skill;

    [Space]
    [Header("스킬 카드 위치")]
    public Transform left;
    public Transform right;

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
    [Header("플레이어 스텟")]
    public Image playerStat;

    [Space]
    public PoolManager poolM;
    public LocalPoolManager[] poolLocalM = new LocalPoolManager[2];


    public TextMeshProUGUI costTxt;
    public int cost;
    public int maxCost => mainModule.playerDataSO.cost;


    public List<GameObject> costObj;

    public GameObject costPrefab;
    public Transform costParentTrm;

    [Space]
    public Slider enemyKillCount;

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

    public string playerInfo;
    #endregion

    private void Start()
    {
        enemyKillCount.maxValue = battleManager.maxEnemyCount;
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
        playerInfo += "건강함";
    }

    private void Update()
    {
        enemyKillCount.value = mainModule.playerDataSO.killEnemy;
    }

    public void SetBattleUI()
    {
        SetActiveButton(false);
        playerStat.transform.DOMoveY(1080 + 40, 0.3f);
        skillPanel.transform.DOLocalMoveX(700, 0.6f);
        behaveTextBox.transform.DOLocalMoveY(478, 0.7f);
        TurnEnd.transform.DOLocalMoveX(835, 2f);
        SetQuickInven();
        StartCoroutine(MoveBehaveButtons(true));

        cardCount = GetCardCount();

        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(0.7f);
        seq.Append(turnImage[0].DOFade(1, 0.6f));
        seq.Join(turnImage[1].DOFade(1, 0.6f));
        seq.AppendCallback(() => TurnChangeEffect(battleManager.SetTurn()));
        seq.AppendInterval(0.8f);
        seq.AppendCallback(() => OnClickSkill());
    }

    public void GetBackBattleUI()
    {
        behaveTextBox.transform.DOLocalMoveY(604, 0.7f);

        TurnEnd.transform.DORewind();
        playerStat.transform.DORewind();
        playerStat.transform.DORewind();
        //skillPanel.transform.DOLocalMoveX(700, 0.6f);
        quickInven.transform.DOLocalMoveY(-481.17f, 0.4f);
        turnImage[0].DOFade(1, 0.6f);
        turnImage[1].DOFade(1, 0.6f);
    }

    public void SetChangeTurn(bool isPlayerTurn)
    {
        Sequence seq = DOTween.Sequence();

        if (isPlayerTurn)
        {
            for (int i = 0; i < cardCount; i++)
            {
                SetSkillInfo();
            }

            turnImage[0].DOFade(1, 1.2f);
            turnImage[0].transform.DOScale(1.4f, 0.6f);
            turnImage[1].DOFade(0.13f, 1f);
            turnImage[1].transform.DOScale(0.7f, .5f);

            seq.Append(behaveText.transform.DOLocalMoveY(170, 0.2f));
            seq.AppendCallback(() =>
            {
                behaveText.color = Color.yellow;
                behaveText.fontSize = 60;
                behaveText.text = "Player Turn";
            });
            seq.Append(behaveText.transform.DOLocalMoveY(0, 0.1f));
            seq.AppendCallback(() =>
            {
                cost = maxCost;
                costTxt.text = $"{cost + "/" + maxCost}";
            });
            seq.AppendCallback(() =>
            {
                for (int i = 0; i < cost; i++)
                {
                    GameObject obj = Instantiate(costPrefab, transform.position, Quaternion.identity);
                    obj.transform.SetParent(costParentTrm.transform);
                    obj.SetActive(true);

                    obj.transform.localPosition = new Vector3(0f + (2 * i), 0f, 0f + i);
                    obj.transform.localScale = Vector3.one;
                    obj.transform.Rotate(new Vector3(0, 0f, 0f));

                    costObj.Add(obj);
                }
            });
        }

        else
        {
            turnImage[1].DOFade(1, 1.2f);
            turnImage[1].transform.DOScale(1.4f, .6f);
            turnImage[0].DOFade(0.13f, 1f);
            turnImage[0].transform.DOScale(0.7f, .5f);

            seq.Append(behaveText.transform.DOLocalMoveY(170, 0.2f));
            seq.AppendCallback(() =>
            {
                behaveText.color = Color.red;
                behaveText.fontSize = 60;
                behaveText.text = "Enemy Turn";
            });
            seq.Append(behaveText.transform.DOLocalMoveY(0, 0.1f));
            battleManager.StartCoroutine("ChangeTurn", false);
        }
    }

    public void TurnChangeEffect(bool isPlayer)
    {
        Sequence seq = DOTween.Sequence();
        SetChangeTurn(isPlayer);

        Color color = isPlayer ? turnImage[0].color : turnImage[1].color;
        color.a = 0.8f;
        turnChangeImage_2.color = color;
        turnText.text = isPlayer ? "Player Turn" : "Enemy Turn";

        seq.Append(turnChangeImage_1.transform.DOLocalMoveX(0, 0.1f)).SetEase(Ease.OutQuint);
        seq.Join(turnChangeImage_2.transform.DOLocalMoveX(0, 0.1f)).SetEase(Ease.OutQuint);

        seq.Append(turnText.transform.DOScale(1, 0.2f)).Join(turnText.DOFade(1, 0.1f));

        seq.AppendInterval(0.3f);

        seq.Append(turnText.transform.DOScale(1.4f, 0.2f)).Join(turnText.DOFade(0, 0.1f));
        
        seq.AppendInterval(0.2f);

        seq.Append(turnChangeImage_2.transform.DOLocalMoveX(-2020, 0.3f)).SetEase(Ease.InQuint);
        seq.Append(turnChangeImage_1.transform.DOLocalMoveX(2020, 0.4f)).SetEase(Ease.InQuint);

        seq.AppendInterval(0.3f);
        seq.AppendCallback(() =>
        {
            turnChangeImage_1.transform.localPosition = new Vector3(-2020, turnChangeImage_1.transform.localPosition.y, 0);
            turnChangeImage_2.transform.localPosition = new Vector3(2020, turnChangeImage_2.transform.localPosition.y, 0);
        });
    }

    public void SetQuickInven()
    {
        quickInven.transform.DOLocalMoveY(-601, 0.8f);
    }

    IEnumerator MoveBehaveButtons(bool isOn, bool istextBox = false)
    {
        int weight = isOn ? 0 : 1;

        for (int i = 0; i < behaveButtons.Count; ++i)
        {
            behaveButtons[i].transform.DOLocalMoveX(weight * 504, 0.1f);
            yield return new WaitForSeconds(0.04f);
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
        if (isActive) _boxui.transform.DOLocalMoveY(-371, 0.2f);
        else _boxui.transform.DOLocalMoveY(-700.92f, 0.2f);
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
        SetText();
        ClearCard(1);
        SetQuickInven();
        StartCoroutine(MoveBehaveButtons(true, true));
    }
    
    public void OnClickItem()
    {
        Sequence seq = DOTween.Sequence();
        ClearCard(2);
        SetUIBox(textBox, false);

        seq.AppendInterval(0.25f);
        seq.Append(quickInven.transform.DOLocalMoveY(-481.17f, 0.2f));
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
            playerStat.transform.DOMoveY(1080 - 28, 0.2f);
            quickInven.transform.DOLocalMoveY(-481.17f, 0.4f);

            ClearCard(100);
            currentSkillCard.Clear();
            currentSkill.Clear();
            playerSkill.Clear();
        });
        seq.Append(winorlose.DOFade(1, 0.2f));
        seq.Join(winorlose.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack));
        seq.AppendInterval(0.6f);
        //seq.Append(winorlose.DOFade)
        seq.Append(winorlose.DOFade(0, 0.3f));
        seq.Join(winorlose.transform.DOScale(0f, 0.4f).SetEase(Ease.InBack));
    }

    public void OnClickTurnEnd()   //여기 턴 넘어가는 곳
    {
        TurnChangeEffect(false);
        ClearCard(10);
        
        ClearSkill();

        Transform[] childList = costParentTrm.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                Destroy(childList[i].gameObject);
            }
        }

        costObj.Clear();
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

       //List<Transform> t = OrderCard(left, right, cardCount, 0.5f, Vector3.one);

        for (int i = 0; i < cardCount; i++)
        {
            //float xpos = -570 + (1140f / (cardCount - 1)) * i;
            //float ypos = -400;

            //Vector3 pos = new Vector3(xpos, 0, 0);

            GameObject card = skillCard;

            currentSkillCard.Add(PoolManager.Instance.Pop(PoolType.Card).gameObject);//Instantiate(card, skillBox.transform);//new Vector3(xpos, ypos, 0f), Quaternion.identity);
            currentSkillCard[i].transform.SetParent(skillBox.transform);

            //currentSkillCard[i].transform.position = t[i].position;
            //currentSkillCard[i].transform.rotation = t[i].rotation;
            //currentSkillCard[i].transform.localScale = t[i].localScale;
            //currentSkillCard[i].transform.localPosition = pos;
        }

        for (int i = 0; i < cardCount; i++)
        {
            SetCardInfo(i);
        }

        seq.AppendInterval(.52f);
        seq.AppendCallback(() => SetUIBox(skillBox, true));
        seq.AppendInterval(.3f);
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

        Vector3 randPos = new Vector3(Random.Range(-0.1f, 0.1f), 0f, Random.Range(-0.1f, 0.1f));
        Vector3 v = pos + randPos;
        
        seq.AppendCallback(() =>
        {
            //text.transform.SetParent(skillBox.transform.parent);
            text.transform.position = v;
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
            cardObj.GetComponent<SkillCard>().SetSkillCard(playerSkill[i]);
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
        StartCoroutine(DiscardCost(m_cost));
    }

    IEnumerator DiscardCost(int m_cost)
    {
        for (int i = 0; i < m_cost; i++)
        {
            DoFade(0, 1, 1, costObj[0].transform.Find("NGon002").GetComponent<MeshRenderer>().material);
            costObj.Remove(costObj[0]);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void DoFade(float start, float dest, float time, Material dissolveMat)
    {
        DOTween.To(() => start, x => { start = x; dissolveMat.SetFloat("_Dissolve", start); }, dest, time).SetUpdate(true);
    }

    public void SetText()
    {
        currentPlayerInfo.text = $"플레이어 정보:\n체력: {mainModule._HpModule.hp} / {mainModule._HpModule.maxHp}\n공격력: {mainModule.playerDataSO.Ad}\n방어력: {mainModule.playerDataSO.Def}\n스피드: {mainModule.playerDataSO.Speed}\n현재 카드 수집한 카드 수: {mainModule.playerDataSO._skills.Length + _skill.cards.Count}\n현재 상태: {playerInfo}";
    }

    //List<Transform> OrderCard(Transform left, Transform right, int objcount, float height, Vector3 scale)
    //{
    //    float[] objLerps = new float[objcount];
    //    List<Transform> card = new List<Transform>(objcount);
    //    Transform tt = transform;

    //    switch (objcount)
    //    {
    //        case 1:
    //            objLerps = new float[] { 0.5f };
    //            break;
    //        case 2:
    //            objLerps = new float[] { 0.23f, 0.73f };
    //            break;
    //        case 3:
    //            objLerps = new float[] { 0.1f, 0.5f, 0.9f };
    //            break;
    //        default:
    //            float interval = 1f / (objcount - 1);
    //            for (int i = 0; i < objcount; i++)
    //            {
    //                objLerps[i] = interval * i;
    //            }
    //            break;
    //    }

    //    for(int i = 0; i< objcount; i++)
    //    {
    //        var targetPos = Vector3.Lerp(left.position, right.position, objLerps[i]);
    //        var targetRot = Quaternion.identity;
    //        if(objcount >= 4)
    //        {
    //            float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
    //            curve = height >= 0 ? curve : -curve;
    //            targetPos.y += curve;
    //            targetRot = Quaternion.Slerp(left.rotation, right.rotation, objLerps[i]);
    //        }
    //        tt.position = targetPos;
    //        tt.rotation = targetRot;
    //        tt.localScale = scale;
    //        card.Add(tt);
    //    }

    //    return card;
    //}
}