using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIModule : MonoBehaviour
{
    [Header("메니져 받아오기")]
    [SerializeField] public UIManager _uiManager;

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
        _uiManager = FindObjectOfType<UIManager>();
        _keyName = "f";

        _trophyUIManager = FindObjectOfType<TrophyUIManager>();
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
