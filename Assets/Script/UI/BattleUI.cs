using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUI : MonoBehaviour
{
    [Space]
    [Header("¿¸≈ı UIµÈ")]
    public Image skillPanel;
    public Image textBox;
    public Image behaveBox;

    public List<Button> behaveButtons;

    private void Start()
    {
        for (int i = 0; i < skillPanel.transform.childCount; i++)
        {
            behaveButtons.Add(skillPanel.transform.GetChild(i).GetComponent<Button>());
        }
    }

    public void SetBattleUI()
    {
        skillPanel.transform.DOLocalMoveX(700, 0.6f);

        StartCoroutine(MoveBehaveButtons(true));
    }

    IEnumerator MoveBehaveButtons(bool isOn, bool textBox = false)
    {
        int weight = isOn ? 0 : 1;

        for (int i = 0; i < behaveButtons.Count; ++i)
        {
            behaveButtons[i].transform.DOLocalMoveX(weight * 504, 0.4f);
            yield return new WaitForSeconds(0.17f);
        }
        if (textBox) SetTextBox();
    }

    private void SetTextBox()
    {
        textBox.transform.DOLocalMoveX(-256, 0.5f);
    }

    public void OnClickSkill()
    {

    }

    public void OnClickInfo()
    {
        
        StartCoroutine(MoveBehaveButtons(false, true));
    }

    public void OnClickItem()
    {
        StartCoroutine(MoveBehaveButtons(false, true));
    }

    public void OnClickRun()
    {
        StartCoroutine(MoveBehaveButtons(false));
    }
}
