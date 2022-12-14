using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Relic")]
public class RelicSO : ScriptableObject
{
    public string relicName;
    public string relicExplain;

    public Sprite relicImage;

    public string[] _methodName;

    private SetRelic setRelic = new SetRelic();
    public SetRelic _SetRelick => setRelic;

    public RelicSO _relicInfo;

    private void OnEnable()
    {
        SetSkill(_relicInfo);
    }

    void SetSkill(RelicSO _relicInfo)
    {
        setRelic.AddEvent(_relicInfo);
    }
}

public class SetRelic
{
    private RelicSO _relicSkill;
    private List<MethodInfo> methods = new List<MethodInfo>();

    public List<MethodInfo> _Methods => methods;

    public void AddEvent(RelicSO relicInfo)
    {
        Type type = typeof(RelicFunc);
        for (int i = 0; i < relicInfo._methodName.Length; i++)
        {
            MethodInfo method = type.GetMethod(relicInfo._methodName[i]);
            methods.Add(method);
        }
    }

    void CallEvent(GameObject enemy)
    {
        foreach (var method in methods)
        {
            method.Invoke(null, new object[] { enemy });
        }
    }
}

