using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{ 
    [Header("[UI Canvas]")]

    public bool isActiveContinue;
    public bool isupgrading = false;
    private bool isDisplayContinue = true;
    public bool IsDisplayContinue { get { return isDisplayContinue; } set { isDisplayContinue = value; } }

    Stack<IUserInterface> _popupStack = new Stack<IUserInterface>();

    private void Update()
    {

    }

    public void ActiveUI(GameObject targetUI)
    {
        IsDisplayContinue = true;

        if (_popupStack.Count > 0)
        {
            _popupStack.Peek().CloseUI();
        }

        _popupStack.Push(targetUI.GetComponent<IUserInterface>());
        _popupStack.Peek().OpenUI();
    }

    public void DeActiveUI()
    {
        if (_popupStack.Count > 1)
        {
            _popupStack.Pop().CloseUI();
            _popupStack.Peek().OpenUI();
        }
        else if (_popupStack.Count == 1)
        {
            _popupStack.Pop().CloseUI();
            if (isDisplayContinue)
            {
            }
            isActiveContinue = true;
        }
        else if (_popupStack.Count == 0)
        {
            if (isDisplayContinue)
            {
            }
        }
    }
}
