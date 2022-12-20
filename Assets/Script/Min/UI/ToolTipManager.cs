using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public List<string> s;
    public MainModule mainModule;

    void Start()
    {
        StartCoroutine(ToolTip());
    }

    IEnumerator ToolTip()
    {
        while(true)
        {
            if (mainModule.isBattle || mainModule.isTrophy || mainModule.canInven == false)
            {
                yield break;
            }
            int randTxt = Random.Range(0, s.Count);
            float rand = Random.Range(1f, 5f);
            yield return new WaitForSeconds(rand);
            DialogManager.Instance.ShowText(s[randTxt]);
        }
    }
}
