using DG.Tweening;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrophyUIManager : MonoBehaviour
{
    public GameObject trophyPanel;

    [SerializeField] private GameObject skillTrophy;


    [SerializeField] private GameObject itemTrophy;
    public Transform parentTrm;

    [SerializeField] private AllSkills allSkills;
    [SerializeField] private SkillIInvenObj skillIInvenObj;
    //ㅇ어차피 카드 몇개있는거아님?

    [SerializeField] private InventoryObj inventoryObj;
    [SerializeField] private ItemDBObj databaseObj;

    public List<CardImage> cardsTrm;
    public GameObject selectPanel;

    public MainModule mainModule;

    // public Image itemsprite;

    public Animator animator;

    public Image inImage;

    public RectTransform way;
    public Vector3[] waypoint;

    public Ease ease;

    public GameObject trail;

    public AnimationCurve asda;

    public RelicDataSO allRelic;
    public RelicDataSO playerRelic;

    public GameObject relicTrophy;

    public List<MethodInfo> relicEffect = new List<MethodInfo>();


    public GameObject relicPrefab;
    public Transform relicParent;
    public Transform battlerelicParent;


    public Button bagBtn;
    public Button settingBtn;

    public GameObject waitBtn;

    private void Awake()
    {
        Transform[] childList = relicParent.GetComponentsInChildren<RectTransform>();
        foreach (var deletecard in childList)
        {
            if (deletecard == relicParent)
                continue;

            Destroy(deletecard.gameObject);
        }

        for (int i = 0; i < playerRelic.relics.Count; i++)
        {
            GameObject relicdObj = Instantiate(relicPrefab, relicParent.transform.position, Quaternion.identity).gameObject;
            relicdObj.transform.SetParent(relicParent);
            relicdObj.GetComponent<RelicImage>().Set(playerRelic.relics[i]);
        }


        Transform[] rellicList = battlerelicParent.GetComponentsInChildren<RectTransform>();
        foreach (var deletecard in rellicList)
        {
            if (deletecard == battlerelicParent)
                continue;

            Destroy(deletecard.gameObject);
        }

        for (int i = 0; i < playerRelic.relics.Count; i++)
        {
            GameObject relicdObj = Instantiate(relicPrefab, battlerelicParent.transform.position, Quaternion.identity).gameObject;
            relicdObj.transform.SetParent(battlerelicParent);
            relicdObj.GetComponent<RelicImage>().Set(playerRelic.relics[i]);
        }
    }


    public void AppearTrophy()
    {
        trophyPanel.transform.DOLocalMoveY(0, 0.5f);
        trophyPanel.GetComponent<Image>().DOFade(1f, 0.8f);
        trophyPanel.transform.Find("TrophyImage").GetComponent<Image>().DOFade(1f, 0.8f);
       
        AddNewItem();
        SetTrophy();
        AddRelic();
    }
    public Skill RandomSkill()
    {
        int randomSKill = Random.Range(0, allSkills._allSkills.Count);
        return allSkills._allSkills[randomSKill];
    }
    public void SetTrophy()
    {
        GameObject skillObj = Instantiate(skillTrophy, parentTrm.position, Quaternion.identity);
        skillObj.transform.SetParent(parentTrm);
        skillObj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        skillObj.transform.localScale = Vector3.one;

        skillObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                selectPanel.SetActive(true);
                Skill[] skills = GetRandDataList();
                cardsTrm[0].Init(skills[0]);
                cardsTrm[1].Init(skills[1]);
                cardsTrm[2].Init(skills[2]);

                trophyPanel.SetActive(false);
                cardsTrm[0].GetComponent<RectTransform>().DOLocalMoveX(-460f, 0.5f);
                cardsTrm[2].GetComponent<RectTransform>().DOLocalMoveX(460f, 0.5f);

                Destroy(skillObj);
            });
    }

    public Skill[] GetRandDataList()
    {
        List<Skill> data = new();
        List<Skill> returnData = new();

        for (int i = 0; i < allSkills._allSkills.Count; i++)
        {
            data.Add(allSkills._allSkills[i]);
        }

        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, data.Count);

            Skill generatedType = data[rand];

            if (data.Contains(generatedType))
            {
                data.Remove(generatedType);
                returnData.Add(generatedType);
            }
            else
            {
                continue;
            }

        }
        Debug.Log(returnData.ToArray());
        return returnData.ToArray();
    }

    /// <summary>
    /// 아이템 추가
    /// </summary>
    public void AddNewItem()
    {
        #region 나중에 가중치로 바꿔서 수정할 필요 있음

        GameObject itemTrophyObj = Instantiate(itemTrophy, parentTrm.position, Quaternion.identity);
        itemTrophyObj.transform.SetParent(parentTrm);
        itemTrophyObj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        itemTrophyObj.transform.localScale = Vector3.one;
        int randomCnt = 0;

        if (databaseObj.itemObjs.Length > 0)
        {
            ItemObj newItemObject = databaseObj.itemObjs[Random.Range(0, databaseObj.itemObjs.Length)]; 
            Item newItem = new Item(newItemObject);

            randomCnt = Random.Range(1, 3);
            //Debug.Log(newItemObject);

            itemTrophyObj.transform.Find("ItemImage").GetComponent<Image>().sprite = newItemObject.itemIcon;
            itemTrophyObj.transform.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = newItemObject.itemData.item_name;

            //itemsprite.sprite = itemTrophyObj.transform.Find("ItemImage").GetComponent<Image>().sprite;

            if (newItemObject.getFlagStackable)
            {
                itemTrophyObj.transform.Find("ItemCountText").GetComponent<TextMeshProUGUI>().text = randomCnt.ToString();
                //Debug.Log(itemTrophy);
                Debug.Log(itemTrophyObj.name);

                itemTrophyObj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    inventoryObj.AddItem(newItemObject.itemData, randomCnt);

                    Destroy(itemTrophyObj);
                    Debug.Log(parentTrm.childCount);

                    waitBtn.SetActive(true);

                    EffectCard(itemTrophyObj.transform.Find("ItemImage").GetComponent<Image>().sprite);

                    Invoke("InitTrophy", 1f);


                });
            }
            else
            {
                Debug.Log(itemTrophyObj.name);
                itemTrophyObj.transform.Find("ItemCountText").GetComponent<TextMeshProUGUI>().text = $"1";
                itemTrophyObj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    inventoryObj.AddItem(newItemObject.itemData, 1);

                    Destroy(itemTrophyObj);
                    Debug.Log(parentTrm.childCount);

                    waitBtn.SetActive(true);

                    EffectCard(itemTrophyObj.transform.Find("ItemImage").GetComponent<Image>().sprite);
                    Invoke("InitTrophy", 1f);


                });

                //inventoryObj.AddItem(newItemObject.itemData, 1);
            }
        }
        #endregion
    }

    /// <summary>
    /// 유물추가
    /// </summary>
    public RelicSO GetRandomRelic()
    {
        int randomRelic = Random.Range(0, allRelic.relics.Count);

        foreach (var item in playerRelic.relics)
        {
            if (allRelic.relics[randomRelic] == item)
            {
                randomRelic = Random.Range(0, allRelic.relics.Count);
                Debug.Log(randomRelic);
                //같은게있으면 다시찾아라
                continue;
            }
        }
        playerRelic.relics.Add(allRelic.relics[randomRelic]);
        return allRelic.relics[randomRelic];
    }

    public void AddRelic()
    {
        GameObject relicObj = Instantiate(relicTrophy, parentTrm.position, Quaternion.identity);
        relicObj.transform.SetParent(parentTrm);
        relicObj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        relicObj.transform.localScale = Vector3.one;

        RelicSO so = GetRandomRelic();
        Debug.Log(so);

        relicObj.transform.Find("relicImage").GetComponent<Image>().sprite = so.relicImage;
        relicObj.GetComponentInChildren<TextMeshProUGUI>().text = so.relicName;
        
        relicObj.GetComponent<Button>().onClick.AddListener(() =>
        {
            relicEffect = so._SetRelick._Methods;

            foreach (var method in relicEffect)
            {
                method.Invoke(null, null);
            }

            waitBtn.SetActive(true);

            //slot.uploadSlot(slot.ItemObject.itemData, --slot.itemCnt);
            //useTap.gameObject.SetActive(false);
            EffectRelic(so);
            Invoke("InitTrophy", 1f);
            Destroy(relicObj);
        });
    }
    public void EffectRelic(RelicSO relicSO)
    {
        inImage.transform.DOKill();

        GameObject relic = Instantiate(relicPrefab, relicParent.position, Quaternion.identity);
        relic.transform.SetParent(relicParent);

        relic.GetComponent<RelicImage>().Set(relicSO);

        relic.GetComponent<Image>().sprite = relicSO.relicImage;
        relic.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);

        Sequence seq = DOTween.Sequence();
        seq.Append(relic.GetComponent<Image>().DOFade(1f, 1f));
        seq.Join(relic.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f));
        seq.Append(relic.transform.DOScale(Vector3.one, 0.5f));

        trail.SetActive(true);
        Invoke("Dont", 2.5f);
        Invoke("OffImage", 2.2f);
    }
    public void EffectCard(Sprite itemImg)
    {
        inImage.transform.DOKill();

        inImage.transform.localPosition = new Vector3(0, 0, 0);
        inImage.gameObject.SetActive(true);
        inImage.sprite = itemImg;

        Vector3 firstPos = Camera.main.WorldToScreenPoint(inImage.transform.position);


        Vector3 middlePoint = new Vector3();
        Vector3 lastPoint = new Vector3();
        float x = firstPos.x + (firstPos.x * Random.Range(0.3f, 0.5f));
        float y = firstPos.y + (firstPos.y * Random.Range(0.1f, 0.2f) * (Random.Range(0, 100) < 50 ? 1f : -1f));

        middlePoint.x = x - Screen.currentResolution.width * 0.5f;
        middlePoint.y = y - Screen.currentResolution.height * 0.5f;
        firstPos.x = firstPos.x - Screen.currentResolution.width * 0.5f;
        firstPos.y = firstPos.y - Screen.currentResolution.height * 0.5f;
        lastPoint.x = way.position.x - Screen.currentResolution.width * 0.5f;
        lastPoint.y = way.position.y - Screen.currentResolution.height * 0.5f;

        
        firstPos.z = 0;
        middlePoint.z = firstPos.z;
        lastPoint.z = firstPos.z;
        waypoint = new Vector3[3];


        waypoint.SetValue(firstPos, 0);
        waypoint.SetValue(middlePoint, 1);
        waypoint.SetValue(lastPoint, 2);

        inImage.rectTransform.DOLocalPath(waypoint, 1.2f, PathType.CatmullRom).SetEase(asda);//    .SetEase(asda);

        trail.SetActive(true);
        Invoke("Dont", 2.5f);
        Invoke("OffImage", 2.2f);
    }
    public void Dont()
    {
        waitBtn.SetActive(false);
    }
    public void OffImage()
    {
        inImage.gameObject.SetActive(false);
        inImage.color = new Color(1, 1, 1);
        trail.SetActive(false);
    }
    public void InitTrophy()
    {
        if (parentTrm.childCount <= 0)
        {
            Debug.Log(parentTrm.childCount);
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(.5f);
            seq.Append(trophyPanel.transform.DOLocalMoveY(1500f, 0.5f));
            seq.AppendCallback(() =>
            {
                trophyPanel.transform.localPosition = new Vector3(0, -1500f, 0);
                mainModule.isTrophy = false;
                mainModule.canMove = false;
                mainModule.canInven = true;
                waitBtn.SetActive(false);

                bagBtn.interactable = true;
                settingBtn.interactable = true;

                Destroy(mainModule.chestCreateManager.chestAnimators[mainModule.physicsModule.index].gameObject);
            });
        }
        else
        {
        }
    }

}
