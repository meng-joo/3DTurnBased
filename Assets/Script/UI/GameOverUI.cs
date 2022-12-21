using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public MapManager map;
    public Image gameOverImage;
    public TextMeshProUGUI gameOverText;
    public Text _text;
    BattleUI battleUI;

    public PlayerDataSO platerDataSO;

    public RelicDataSO playerRelic;

    public SkillIInvenObj deckSkill;
    public SkillIInvenObj addSkill;

    public SkillIInvenObj initSkill;

    public InventoryObj quickInven;
    public InventoryObj equipInven;
    public InventoryObj itemInven;
    private void Start()
    {
        battleUI = GetComponent<BattleUI>();
    }

    public void GameOverScreen()
    {
        StartCoroutine(GameOverButton());
    }

    IEnumerator GameOverButton()
    {
        battleUI.GameEnd("");
        battleUI.mainModule.battleCam.Priority -= 10;
        battleUI.mainModule.battleCam.m_Lens.OrthographicSize = 3;

        Init();

        yield return new WaitForSeconds(1);

        gameOverImage.DOFade(1, 1);
        gameOverText.transform.DOLocalMoveY(20, 1.5f).SetLoops(-1, LoopType.Yoyo);
        gameOverText.DOFade(1, 1.5f);

        _text.DOText("당신은 탑을 클리어 하지 못하였습니다.", 1.3f);

        yield return new WaitForSeconds(10);
        map.StartInit(0);
    }
    public void Init()
    {
        platerDataSO.canBattle = false;
        
        platerDataSO.Health = 78;   
        platerDataSO.Ad = 5;   
        platerDataSO.Def = 2;   
        platerDataSO.Speed = 20;

        playerRelic.relics.Clear();

        platerDataSO.cost = 3;
        platerDataSO.stage = 1;
        platerDataSO.killEnemy = 0;

        deckSkill.cards.Clear();
        addSkill.cards.Clear();
        platerDataSO._skills.Clear();

        platerDataSO._skills.Clear();

        for (int i = 0; i < initSkill.cards.Count; i++)
        {
            platerDataSO._skills.Add(initSkill.cards[i]);
            deckSkill.cards.Add(initSkill.cards[i]);
        }


        quickInven?.Clear();
        itemInven?.Clear();
        equipInven?.Clear();

    }
}
