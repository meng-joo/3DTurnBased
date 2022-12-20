using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoSingleton<UISoundManager>
{
    public UISoundData data;

    public void PlayClickClip()
    {
        AudioManager.PlayAudio(data.clickClip);
    }
   
 
}
