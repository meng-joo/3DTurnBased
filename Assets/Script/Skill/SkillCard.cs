using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class SkillCard : PoolAbleObject
{
    [Header("스킬 카드 요소")]
    public Image backGroundImage;
    public Image fadeImage;
    public Image skillImage;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillInfo;

    public List<MethodInfo> skillEffect = new List<MethodInfo>();

    Vector2 originPos;
    Vector2 mousePoint;

    bool isfirst = false;
    bool m_IsButtonDowning = false;

    Color32 skillText;

    string target;

    LocalPoolManager poolLocalM;

    MainModule _mainModule;
    BattleUI _battleUI;

    int value;

    private void Init()
    {
        _battleUI = GameObject.Find("UIManager").GetComponent<BattleUI>();
        _mainModule = GameObject.Find("Player").GetComponent<MainModule>();
        poolLocalM = GameObject.Find("LocalPool : Card").GetComponent<LocalPoolManager>();
        backGroundImage = GetComponent<Image>();
        skillImage = transform.GetChild(0).GetComponent<Image>();
        skillName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        skillInfo = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        fadeImage = transform.GetChild(3).GetComponent<Image>();
    }

    public void SetSkillCard(Skill skillData)
    {
        Init();
        backGroundImage.color = skillData.skillInfo._skillCardColor;
        skillImage.sprite = skillData.skillInfo._skillImage;
        skillName.text = skillData.skillInfo._skillName;
        skillInfo.text = skillData.skillInfo._skillExplanation;

        value = skillData.skillInfo.value;
        skillText = skillData.skillInfo._skillEffectColor;
        target = skillData.skillInfo.target;
        skillEffect = skillData._SetSkill._Methods;
        //skillData.SetFunc();
        //skillEffect = skillData._SetSkill.skillFunction;
    }

    void Update()
    {
        if (m_IsButtonDowning)
        {
            if (!isfirst)
            {
                originPos = transform.position;
                isfirst = !isfirst;
            }

            if (isfirst)
            {
                Vector3 pos = Input.mousePosition;
                //pos.z = Camera.main.farClipPlane;

                //mousePoint = Camera.main.ScreenToWorldPoint(pos);
                mousePoint = pos;
                transform.position = mousePoint;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.CompareTag(target))
                    {
                        if (fadeImage.color.a <= 1)
                        {
                            Color color = fadeImage.color;
                            color.a += 0.08f;
                            fadeImage.color = color;
                        }
                    }
                    else
                    {
                        if (fadeImage.color.a >= 0)
                        {
                            Color color = fadeImage.color;
                            color.a -= 0.08f;
                            fadeImage.color = color;
                        }
                    }
                }

                
            }
        }
    }

    public void PointerDown()
    {
        m_IsButtonDowning = true;
    }

    public void PointerUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.CompareTag(target))
            {
                foreach (var method in skillEffect)
                {
                    method.Invoke(null, new object[] { hit.collider.gameObject, value });
                }

                //CardEffect();
                _battleUI.SpawnSkillEffectText(value, skillText, transform.position);

                //skillEffect.Invoke(hit.collider.gameObject);
                transform.SetParent(poolLocalM.transform);
                PoolManager.Instance.Push(PoolType.Card, gameObject);
                return;
            }
        }
        transform.position = originPos;

        m_IsButtonDowning = false;
    }

    public override void Init_Pop()
    {
    }

    public override void Init_Push()
    {
    }
}
