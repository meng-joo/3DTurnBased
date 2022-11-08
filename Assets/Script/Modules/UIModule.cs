using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIModule : MonoBehaviour
{
    Canvas _canvas;
    Image _interationkeyImage;

    private void Start()
    {
        _canvas = transform.GetComponentInChildren<Canvas>();
        _interationkeyImage = _canvas.transform.Find("InterationKeyImage").GetComponent<Image>();
        _interationkeyImage.enabled = false;
    }

    public void OnInteractionKeyImage(bool isOn)
    {
        Debug.Log(isOn);
        _interationkeyImage.enabled = isOn;
    }
}
