using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class EndingManager : MonoBehaviour
{
    [SerializeField]
    private Transform _lastPos = null;
    [SerializeField]
    private float _textSpeed = 30f;
    [SerializeField]
    private TextMeshProUGUI _tooltipText = null;
    private float _originSpeed = 0f;

    public bool _isEnding = false;

    public GameObject _text = null;

    public GameObject fadeObj;

    public PlayerDataSO data;
    private void Start()
    {
        _originSpeed = _textSpeed;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _textSpeed = _originSpeed * 3f;
        }
        else
        {
            _textSpeed = _originSpeed;
        }

        if (data.isClear == true)
        {
            _isEnding = true;
        }

        if (_isEnding)
        {
            fadeObj.SetActive(true);
            _text.transform.Translate(Vector3.up * _textSpeed * Time.deltaTime);

            if (_text.transform.position.y >= _lastPos.position.y)
            {
                fadeObj.GetComponent<Image>().DOFade(0f, 0.5f);
                _text.GetComponent<TextMeshProUGUI>().DOFade(0f, 0.3f);
                _tooltipText.GetComponent<TextMeshProUGUI>().DOFade(0f, 0.3f);
                fadeObj.GetComponent<Image>().raycastTarget = false;

                _isEnding = false;
            }
        }
    }
}
