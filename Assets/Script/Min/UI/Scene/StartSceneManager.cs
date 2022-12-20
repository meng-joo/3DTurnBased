using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class StartSceneManager : MonoBehaviour
{
    public GameObject dicObj;

    public Image cardimg;
    public Image rellicimg;
    public Image itemimg;

    public GameObject skillObj;
    public AllSkills all;
    public GameObject card;
    public Transform skillParentTrm;

    #region 아이템
    public GameObject itemObj;
    public ItemDBObj itemDB;

    public GameObject item;

    public Transform itemPotionParentTrm;
    public Transform itemEquipParentTrm;
    #endregion 

    public GameObject relicObj;
    public RelicDataSO relicAllSo;
    public GameObject relic;
    public Transform relicParentTrm;


    public List<GameObject> scroll;



    public GameObject escBtn;

    public RectTransform startTrm;
    public RectTransform dicTrm;
    public RectTransform exitTrm;
    public void GameStartBtn()
    {
        SceneManager.LoadScene("InGame_Hollway");
        //탑으로 가는 씬
    }

    public void DicBtn()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            dicObj.SetActive(true);
            cardimg.transform.DOLocalMoveY(0, 0.45f);
            rellicimg.transform.DOLocalMoveY(0, 0.5f);
            itemimg.transform.DOLocalMoveY(0, 0.43f);
        });

        seq.AppendInterval(0.2f);
        seq.AppendCallback(() =>
        {
            escBtn.GetComponent<Image>().DOFade(1f, 0.5f);
            escBtn.GetComponentInChildren<TextMeshProUGUI>().DOFade(1f, 0.5f);

            startTrm.DOLocalMoveX(-1200f, 0.3f);
            dicTrm.DOLocalMoveX(-1200f, 0.3f);
            exitTrm.DOLocalMoveX(-1200f, 0.3f);
        });
        seq.AppendCallback(() =>
        {
            dicTrm.gameObject.SetActive(false);
        });
    }
   
    public void ExitBtn()
    {
        Application.Quit();
    }
    public void CreateAllCard()
    {
        dicObj.SetActive(false);
        skillObj.SetActive(true);

        for (int i = 0; i < all._allSkills.Count; i++)
        {
            GameObject skillObj = Instantiate(card, transform.position, Quaternion.identity);
            skillObj.transform.SetParent(skillParentTrm);
            skillObj.GetComponent<StartCardImage>().Set(all._allSkills[i]);
            skillObj.transform.Find("Deco").transform.Find("SkillImageBackground").transform.Find("Image").GetComponentInChildren<Image>().sprite = all._allSkills[i].skillInfo._skillImage;
            skillObj.transform.Find("Deco").transform.Find("NameImage").GetComponentInChildren<TextMeshProUGUI>().text = all._allSkills[i].skillInfo._skillName;
            skillObj.transform.Find("CostImage").GetComponentInChildren<TextMeshProUGUI>().text = $"{all._allSkills[i].skillInfo._skillCost}";
            skillObj.transform.Find("Deco").transform.Find("SkillInfoImage").GetComponentInChildren<TextMeshProUGUI>().text = all._allSkills[i].skillInfo._skillExplanation;

            skillObj.transform.localPosition = Vector3.zero;
            skillObj.transform.localScale = Vector3.one;
            skillObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
        skillObj.transform.DOLocalMoveY(0, 0.3f);
    }
    public void CreateAllItem()
    {
        dicObj.SetActive(false);
        itemObj.SetActive(true);
        for (int i = 0; i < itemDB.itemObjs.Length; i++)
        {
            GameObject itemObj =  Instantiate(item, transform.position, Quaternion.identity);
            if (itemDB.itemObjs[i].itemType == ItemType.Default)
            {
                itemObj.transform.SetParent(itemPotionParentTrm);
            }
            else
            {
                itemObj.transform.SetParent(itemEquipParentTrm);
            }
            itemObj.GetComponent<StartItemImage>().Set(itemDB.itemObjs[i]);
            itemObj.GetComponent<Image>().sprite = itemDB.itemObjs[i].itemIcon;
            itemObj.transform.localPosition = Vector3.zero;
            itemObj.transform.localScale = Vector3.one;
            itemObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
        itemObj.transform.DOLocalMoveY(0, 0.3f);
    }
    public void CreateRelic()
    {
        dicObj.SetActive(false);
        relicObj.SetActive(true);

        for (int i = 0; i < relicAllSo.relics.Count; i++)
        {
            GameObject relicObj = Instantiate(relic, transform.position, Quaternion.identity);
            relicObj.transform.SetParent(relicParentTrm);
            relicObj.transform.localPosition = Vector3.zero;
            relicObj.transform.localScale = Vector3.one;
            relicObj.transform.localRotation = Quaternion.Euler(Vector3.zero);

            relicObj.GetComponent<StartRelicImage>().Set(relicAllSo.relics[i]);
            relicObj.GetComponent<Image>().sprite = relicAllSo.relics[i].relicImage;
        }
        relicObj.transform.DOLocalMoveY(0, 0.3f);

    }
    public void BackInfoBtn()
    {
        Delete();

        dicObj.SetActive(false);
        // for (int i = 0; i < scroll.Count; i++)
        //  {
        //  scroll[i].SetActive(false);
        //  }
        dicTrm.gameObject.SetActive(true);

        startTrm.DOLocalMoveX(-780f, 0.5f);
        dicTrm.DOLocalMoveX(-780f, 0.5f);
        exitTrm.DOLocalMoveX(-780f, 0.5f);

    }
    public void Delete()
    {
        dicObj.SetActive(true);
        itemObj.SetActive(false);   
        relicObj.SetActive(false);
        skillObj.SetActive(false);
        skillObj.transform.localPosition = new Vector3(0, 0, 0);
        relicObj.transform.localPosition = new Vector3(0, -150, 0);
        itemObj.transform.localPosition = new Vector3(0,150, 0);

        RectTransform[] cardchildList = skillParentTrm.GetComponentsInChildren<RectTransform>();
        foreach (var deletecard in cardchildList)
        {
            if (deletecard == skillParentTrm)
                continue;

            Destroy(deletecard.gameObject);
        }


        RectTransform[] relicchildList = relicParentTrm.GetComponentsInChildren<RectTransform>();
        foreach (var deletecard in relicchildList)
        {
            if (deletecard == relicParentTrm)
                continue;

            Destroy(deletecard.gameObject);
        }


        RectTransform[] potionchildList = itemPotionParentTrm.GetComponentsInChildren<RectTransform>();
        foreach (var deletecard in potionchildList)
        {
            if (deletecard == itemPotionParentTrm)
                continue;

            Destroy(deletecard.gameObject);
        }

        RectTransform[] equipchildList = itemEquipParentTrm.GetComponentsInChildren<RectTransform>();
        foreach (var deletecard in equipchildList)
        {
            if (deletecard == itemEquipParentTrm)
                continue;

            Destroy(deletecard.gameObject);
        }

    }
}
