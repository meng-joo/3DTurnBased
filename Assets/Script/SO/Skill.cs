using System;
using System.Reflection; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/Skill")]
public class Skill : ScriptableObject
{
    public SkillData skillInfo;

    private SetSkill setSkill = new SetSkill();
    public SetSkill _SetSkill => setSkill;

    private void OnEnable()
    {
        SetSkill(skillInfo);
    }

    void SetSkill(SkillData _skillInfo)
    {
        setSkill.AddEvent(_skillInfo);
    }
}

public class SetSkill
{
    private SkillFunc _attackSkill;
    private List<MethodInfo> methods = new List<MethodInfo>();

    public List<MethodInfo> _Methods => methods;

    public void AddEvent(SkillData skillInfo)
    {
        Type type = typeof(SkillFunc);
        for (int i = 0; i < skillInfo._methodName.Length; i++)
        {
            MethodInfo method = type.GetMethod(skillInfo._methodName[i]);
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