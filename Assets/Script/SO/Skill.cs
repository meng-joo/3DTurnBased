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
    public UnityEvent<GameObject> skillFunction;

    private SkillFunc _attackSkill;

    private void OnEnable()
    {
        Type type = typeof(SkillFunc);
        MethodInfo method = type.GetMethod(skillInfo._methodName);

        skillFunction.AddListener((x) => method.Invoke(_attackSkill,null)); 
        //skillFunction.AddListener(() => _attackSkill.AttackSkill_Jap(null));
    }
}