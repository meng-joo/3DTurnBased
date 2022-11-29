using DG.Tweening;
using System.Collections.Generic;
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

    public GameObject targetPos;

    public Image itemsprite;
    private void Awake()
    {
    }
    public void AppearTrophy()
    {
        trophyPanel.transform.DOLocalMoveY(0, 0.5f);
        trophyPanel.GetComponent<Image>().DOFade(1f, 0.8f);
        trophyPanel.transform.Find("TrophyImage").GetComponent<Image>().DOFade(1f, 0.8f);
        AddNewItem();
        SetTrophy();
    }
    public Skill RandomSkill()
    {
        int randomSKill = Random.Range(0, allSkills._allSkills.Count);
       // Debug.Log(allSkills._allSkills[randomSKill]);
        return allSkills._allSkills[randomSKill];
    }
    public void SetTrophy( )
    {
        GameObject skillObj = Instantiate(skillTrophy, parentTrm.position, Quaternion.identity);
        skillObj.transform.SetParent(parentTrm);
        //skillObj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        //skillObj.transform.localScale = Vector3.one;

        //skillObj.transform.Find("SkillImage").GetComponent<Image>().sprite = skill.skillInfo._skillImage;
        //skillObj.transform.Find("SkillNameText").GetComponent<TextMeshProUGUI>().text = skill.skillInfo._skillName;
        //skillObj.transform.Find("SkillCountText").GetComponent<TextMeshProUGUI>().text = skill.skillInfo._skillExplanation;

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
        //itemTrophyObj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        //itemTrophyObj.transform.localScale = Vector3.one;
        int randomCnt = 0;

        if (databaseObj.itemObjs.Length > 0)
        {
            ItemObj newItemObject = databaseObj.itemObjs[Random.Range(0, databaseObj.itemObjs.Length)];
            Item newItem = new Item(newItemObject);

            randomCnt = Random.Range(1, 3);
            //Debug.Log(newItemObject);

            itemTrophyObj.transform.Find("ItemImage").GetComponent<Image>().sprite = newItemObject.itemIcon;
            itemTrophyObj.transform.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = newItemObject.itemData.item_name;

            itemsprite.sprite = itemTrophyObj.transform.Find("ItemImage").GetComponent<Image>().sprite;
            
            if (newItemObject.getFlagStackable)
            {
                itemTrophyObj.transform.Find("ItemCountText").GetComponent<TextMeshProUGUI>().text = randomCnt.ToString();
                //Debug.Log(itemTrophy);
                Debug.Log(itemTrophyObj.name);

                itemTrophyObj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    inventoryObj.AddItem(newItemObject.itemData, randomCnt);
                    Vector3.Slerp(itemsprite.rectTransform.position, targetPos.transform.position, 0.5f);

                    Destroy(itemTrophyObj);
                    Debug.Log(parentTrm.childCount);

                    
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
                    Vector3.Slerp(itemsprite.rectTransform.position, targetPos.transform.position, 0.5f);

                    Destroy(itemTrophyObj);
                    Debug.Log(parentTrm.childCount);

                    Invoke("InitTrophy", 1f);
                });

                //inventoryObj.AddItem(newItemObject.itemData, 1);
            }
        }
        #endregion
    }
    
    public void InitTrophy()
    {
        if (parentTrm.childCount <= 0)
        {
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(0.5f);
            seq.Append(trophyPanel.transform.DOLocalMoveY(1500f, 0.5f));
            seq.AppendCallback(()=> 
            {
                trophyPanel.transform.localPosition = new Vector3(0, -1500f, 0);
                mainModule.isTrophy = false;
                mainModule.canMove = false;
            });
        }
        else
        {
            Debug.Log("자식있음");
        }
    }
    
}
