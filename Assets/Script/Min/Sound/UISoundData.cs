using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Sound")]
public class UISoundData : ScriptableObject
{
    [Header("버튼 클릭")]
    public AudioClip clickClip;

    public AudioClip foucusClip;


    public AudioClip clickAndSlideClip;

    public AudioClip tiperrorClip;

    public AudioClip sanhojacyoung;
}
