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
        _interationkeyImage = transform.Find("InterationKeyImage").GetComponent<Image>();
    }

    public void OnInteractionKeyImage(bool isOn)
    {
        _interationkeyImage.enabled = isOn;
    }
}
