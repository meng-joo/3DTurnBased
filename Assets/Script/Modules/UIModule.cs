using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIModule : MonoBehaviour
{
    [Header("메인모듈")]
    private MainModule _mainModule;

    [Space]
    [Header("메니져 받아오기")]
    public UIManager _uiManager;

    BattleUI _battleUI;

    Canvas _playerCanvas;
    Image _interationkeyImage;
    TextMeshProUGUI _behaveText;
    TextMeshProUGUI _keyText;

    private string _keyName;
    public string KeyName => _keyName;

    private string _funcName;
    public string FuncName => _funcName;

    public bool canInteration => _interationkeyImage.gameObject.activeSelf;

    private TrophyUIManager _trophyUIManager;
    public TrophyUIManager TrophyUIManager => _trophyUIManager;
    private void Start()
    {
        _playerCanvas = transform.GetComponentInChildren<Canvas>();
        _interationkeyImage = _playerCanvas.transform.Find("InterationKeyImage")?.GetComponent<Image>();
        _keyText = _interationkeyImage?.transform.Find("KeyText")?.GetComponent<TextMeshProUGUI>();
        _behaveText = _interationkeyImage?.transform.Find("BehaviorText")?.GetComponent<TextMeshProUGUI>();
        _interationkeyImage.gameObject?.SetActive(false);
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _battleUI = _uiManager.GetComponent<BattleUI>();
        _mainModule = GetComponent<MainModule>();
        _keyName = "f";

        _trophyUIManager = GameObject.Find("TrophyManager").GetComponent<TrophyUIManager>();
    }

    public void DamageUI(int damage)
    {
        Sequence seq = DOTween.Sequence();
        GameObject player = _mainModule.gameObject;
        GameObject target = player.transform.Find("Target").gameObject;

        seq.AppendCallback(() =>
        {
            target.transform.parent = null;
            Vector2 playerPos = _mainModule.playerCam.WorldToScreenPoint(gameObject.transform.position + new Vector3(0, 1, 0));
            _battleUI.SpawnSkillEffectText(damage.ToString(), Color.white, playerPos);
        });

        seq.AppendInterval(1f);

        seq.AppendCallback(() => target.transform.SetParent(player.transform));
    }

    public void OnInteractionKeyImage(bool isOn = false, string _behave = "", string _key = "f", string _func = "")
    {
        Debug.Log(isOn);

        _keyName = _key;

        _keyText.text = _key;
        _behaveText.text = _behave;
        _funcName = _func;
        _interationkeyImage.gameObject.SetActive(isOn);
    }
}
