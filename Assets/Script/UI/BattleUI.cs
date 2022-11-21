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
    public Image skillPanel;
    public Image textBox;
    public Image skillBox;
    public Image behaveBox;
    public Image behaveTextBox;

    [Space]
    public GameObject skillCard;

    [Space]
    public GameObject[] currentSkillCard = new GameObject[5];

    [Space]
    public List<Button> behaveButtons;
    public Image[] turnImage = new Image[2];
    public TextMeshProUGUI behaveText;

    [Space]
    [Header("적을 알기 위한 배틀 메니져")]
    [SerializeField] private BattleManager battleManager;


    private void Start()
    {
        for (int i = 0; i < skillPanel.transform.childCount; i++)
        {
            behaveButtons.Add(skillPanel.transform.GetChild(i).GetComponent<Button>());
        }

        behaveText = behaveTextBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        turnImage[0] = behaveTextBox.transform.GetChild(1).GetComponent<Image>();
        turnImage[1] = behaveTextBox.transform.GetChild(2).GetComponent<Image>();
    }

    public void SetBattleUI()
    {
        SetActiveButton(false);
        skillPanel.transform.DOLocalMoveX(700, 0.6f);
        behaveTextBox.transform.DOLocalMoveY(430, 0.7f);
        StartCoroutine(MoveBehaveButtons(true));

        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(2f);
        seq.Append(turnImage[0].DOFade(1, 0.6f));
        seq.Join(turnImage[1].DOFade(1, 0.6f));
        seq.AppendInterval(0.8f);
        seq.AppendCallback(() => SetChangeTurn(battleManager.SetTurn()));
    }

    public void SetChangeTurn(bool isPlayerTurn)
    {
        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() => SetActiveButton(false));
        if (isPlayerTurn)
        {
            turnImage[0].DOFade(1, 2.4f);
            turnImage[0].transform.DOScale(1.18f, 1.2f);
            turnImage[1].DOFade(0.13f, 2.4f);
            turnImage[1].transform.DOScale(0.7f, 1.2f);

            seq.Append(behaveText.transform.DOLocalMoveY(170, 0.4f));
            seq.AppendCallback(() =>
            {
                behaveText.color = Color.yellow;
                behaveText.fontSize = 100;
                behaveText.text = "Player Turn";
            });
            seq.Append(behaveText.transform.DOLocalMoveY(0, 0.4f));
        }

        else
        {
            turnImage[1].DOFade(1, 2.4f);
            turnImage[1].transform.DOScale(1.18f, 1.2f);
            turnImage[0].DOFade(0.13f, 2.4f);
            turnImage[0].transform.DOScale(0.7f, 1.2f);

            seq.Append(behaveText.transform.DOLocalMoveY(170, 0.4f));
            seq.AppendCallback(() =>
            {
                behaveText.color = Color.red;
                behaveText.fontSize = 100;
                behaveText.text = "Enemy Turn";
            });
            seq.Append(behaveText.transform.DOLocalMoveY(0, 0.4f));
        }

        seq.AppendInterval(1f);
        seq.AppendCallback(() => SetActiveButton(true));
    }

    IEnumerator MoveBehaveButtons(bool isOn, bool istextBox = false)
    {
        int weight = isOn ? 0 : 1;

        for (int i = 0; i < behaveButtons.Count; ++i)
        {
            behaveButtons[i].transform.DOLocalMoveX(weight * 504, 0.4f);
            yield return new WaitForSeconds(0.17f);
        }

        yield return new WaitForSeconds(0.6f);
        if (istextBox) SetUIBox(textBox, istextBox);
    }

    private void SetActiveButton(bool isActive)
    {
        for (int i = 0; i < behaveButtons.Count; ++i)
        {
            behaveButtons[i].interactable = isActive;
        }
    }

    private void SetUIBox(Image _boxui, bool isActive)
    {
        if (isActive) _boxui.transform.DOLocalMoveX(-256, 0.5f);
        else _boxui.transform.DOLocalMoveX(1686, 0.5f);
    }

    public void OnClickSkill()
    {
        StartCoroutine(MoveBehaveButtons(true, false));
        SetUIBox(textBox, false);
        SpawnCard();
    }

    public void OnClickInfo()
    {
        ClearCard(1);
        
        StartCoroutine(MoveBehaveButtons(true, true));
    }

    public void OnClickItem()
    {
        ClearCard(2);
        SetUIBox(textBox, false);
        //StartCoroutine(MoveBehaveButtons(true, false));
    }

    public void OnClickRun()
    {
        SetActiveButton(false);
        SetUIBox(skillBox, false);
        StartCoroutine(MoveBehaveButtons(false));
    }

    private void ClearCard(int num)
    {
        Sequence seq = DOTween.Sequence();

        SetActiveButton(false);
        for (int i = 0; i < 5; i++)
        {
            seq.Append(currentSkillCard[i].transform.DOLocalMoveY(-400, 0.2f));
            seq.AppendCallback(() => Destroy(currentSkillCard[i]));
        }

        seq.AppendCallback(() => { SetUIBox(skillBox, false); SetActiveButton(true); behaveButtons[num].interactable = false; });
    }

    private void SpawnCard()
    {
        Sequence seq = DOTween.Sequence();

        SetActiveButton(false);

        for (int i = 0; i < 5; i++)
        {
            float xpos = -550 + i * 275;
            float ypos = -400;

            GameObject card = skillCard;

            currentSkillCard[i] = Instantiate(card, skillBox.transform);//new Vector3(xpos, ypos, 0f), Quaternion.identity);
            currentSkillCard[i].transform.localPosition = new Vector3(xpos, ypos, 0);
        }

        seq.AppendCallback(() => SetUIBox(skillBox, true));
        seq.AppendInterval(1f);

        for (int i = 0; i < 5; i++)
        {
            seq.Append(currentSkillCard[i].transform.DOLocalMoveY(0, 0.3f));
        }

        seq.AppendCallback(() =>
        {
            SetActiveButton(true);
            behaveButtons[0].interactable = false;
        });
    }

    public void SetCardInfo(int i)
    {

        //currentSkillCard[i].GetComponent<SkillCard>().SetSkillCard();
    }
}
