using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TrophyUIManager : MonoBehaviour
{
    [SerializeField] private GameObject trophyPanel;

    [SerializeField] private GameObject skillTrophy;

    private Image skillTrophyImage;
    private TextMeshProUGUI skillTrophyName;
    private TextMeshProUGUI skillTrophyCount;

    [SerializeField] private GameObject itemTrophy;
    [SerializeField] private Transform parentTrm;

    [SerializeField] private AllSkills allSkills;
    [SerializeField] private SkillIInvenObj skillIInvenObj;
    //ㅇ어차피 카드 몇개있는거아님?

    [SerializeField] private InventoryObj inventoryObj;
    [SerializeField] private ItemDBObj databaseObj;



    private void Awake()
    {
    }
    public void AppearTrophy()
    {
        trophyPanel.transform.DOLocalMoveY(0, 0.5f);
        trophyPanel.GetComponent<Image>().DOFade(1f, 0.8f);
        trophyPanel.transform.Find("TrophyImage").GetComponent<Image>().DOFade(1f, 0.8f);
        AddNewItem();
        SetTrophy(RandomSkill());

    }
    public Skill RandomSkill()
    {
        int randomSKill = Random.Range(0, allSkills._allSkills.Count);
        Debug.Log(allSkills._allSkills[randomSKill]);
        return allSkills._allSkills[randomSKill];
    }
    public void SetTrophy(Skill skill)
    {
        GameObject skillObj = Instantiate(skillTrophy, parentTrm.position, Quaternion.identity);
        skillObj.transform.SetParent(parentTrm);

        skillObj.transform.Find("SkillImage").GetComponent<Image>().sprite = skill.skillInfo._skillImage;
        skillObj.transform.Find("SkillNameText").GetComponent<TextMeshProUGUI>().text = skill.skillInfo._skillName;
        skillObj.transform.Find("SkillCountText").GetComponent<TextMeshProUGUI>().text = skill.skillInfo._skillExplanation;

        allSkills._allSkills.Add(skill);
        skillIInvenObj.cards.Add(skill);
    }
    public List<Skill> data;

    public Skill[] GetRandDataList()
    {
        List<Skill> returnData = new();
        for (int i = 0; i < allSkills._allSkills.Count; i++)
        {
            data.Add(allSkills._allSkills[i]);
        }

        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, allSkills._allSkills.Count);

            Skill generatedType = allSkills._allSkills[rand];
            if (allSkills._allSkills.Contains(generatedType))
            {
                data.Remove(generatedType);
                i--;
            }
            else
            {
                returnData.Add(generatedType);
            }
        }
        Debug.Log(returnData.ToArray());
        return returnData.ToArray();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
        }
    }
    /// <summary>
    /// 아이템 추가
    /// </summary>
    public void AddNewItem()
    {
        #region 나중에 가중치로 바꿔서 수정할 필요 있음

        GameObject itemTrophyObj = Instantiate(itemTrophy, parentTrm.position, Quaternion.identity);
        itemTrophyObj.transform.SetParent(parentTrm);

        int randomCnt = 0;

        if (databaseObj.itemObjs.Length > 0)
        {
            ItemObj newItemObject = databaseObj.itemObjs[Random.Range(0, databaseObj.itemObjs.Length)];
            Item newItem = new Item(newItemObject);

            randomCnt = Random.Range(1, 3);
            Debug.Log(newItemObject);
            if (newItemObject.getFlagStackable)
            {
                itemTrophyObj.transform.Find("ItemCountText").GetComponent<TextMeshProUGUI>().text = randomCnt.ToString();

                inventoryObj.AddItem(newItemObject.itemData, randomCnt);

            }
            else
            {
                itemTrophyObj.transform.Find("ItemCountText").GetComponent<TextMeshProUGUI>().text = $"1";

                inventoryObj.AddItem(newItemObject.itemData, 1);
            }
            itemTrophyObj.transform.Find("ItemImage").GetComponent<Image>().sprite = newItemObject.itemIcon;
            itemTrophyObj.transform.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = newItemObject.itemData.item_name;
        }

        #endregion
    }

    //뭐먹었는지 알려주고 
    //스킬 3개뽑아서 하나 고르게
}
