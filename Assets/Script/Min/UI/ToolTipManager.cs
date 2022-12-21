using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public List<string> s;
    public MainModule mainModule;
    public InventoryManager im;

    void Start()
    {
        StartCoroutine(ToolTip());
    }

    IEnumerator ToolTip()
    {
        while (true)
        {
            if (mainModule.isBattle == false && mainModule.isTrophy == false && mainModule.canInven == true && mainModule.MapManager.isMiniMapUp == false && im.isInven  == false)
            {
                Debug.Log("A");
                int randTxt = Random.Range(0, s.Count);
                float rand = Random.Range(30f, 40f);
                DialogManager.Instance.ShowText(s[randTxt]);
                yield return new WaitForSeconds(rand);
                
            }
            yield return null;
            
        }
    }
}
