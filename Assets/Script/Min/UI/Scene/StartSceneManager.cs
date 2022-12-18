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

    #region ������
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
        //ž���� ���� ��
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

            startTrm.DOMoveX(-120f, 0.5f);
            dicTrm.DOMoveX(-120f, 0.5f);
            exitTrm.DOMoveX(-120f, 0.5f);
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
            skillObj.transform.Find("Image").GetComponent<Image>().sprite = all._allSkills[i].skillInfo._skillImage;
            skillObj.transform.Find("NameBackground").GetComponentInChildren<TextMeshProUGUI>().text = all._allSkills[i].skillInfo._skillName;
            skillObj.transform.Find("CostImage").GetComponentInChildren<TextMeshProUGUI>().text = $"{all._allSkills[i].skillInfo._skillCost}";
            skillObj.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = all._allSkills[i].skillInfo._skillExplanation;
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

        startTrm.DOMoveX(120f, 0.5f);
        dicTrm.DOMoveX(120f, 0.5f);
        exitTrm.DOMoveX(120f, 0.5f);

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
