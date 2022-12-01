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
    public TextMeshProUGUI skillCost;

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

    AnimationClip _motion;
    PoolType _skillVFX;
    int value;
    string typeText;

    Skill currentSkill;

    private void Init()
    {
        _battleUI = GameObject.Find("UIManager").GetComponent<BattleUI>();
        _mainModule = GameObject.Find("Player").GetComponent<MainModule>();
        poolLocalM = GameObject.Find("LocalPool : Card").GetComponent<LocalPoolManager>();
        //backGroundImage = GetComponent<Image>();
        //skillCost = transform.Find("CostText").GetComponent<TextMeshProUGUI>();
        //skillImage = transform.Find("SkillImage").GetComponent<Image>();
        //skillName = transform.Find("SkillName").GetComponent<TextMeshProUGUI>();
        //skillInfo = transform.Find("SkillInfo").GetComponent<TextMeshProUGUI>();
        //fadeImage = transform.Find("FadeImage").GetComponent<Image>();
    }

    public void SetSkillCard(Skill skillData)
    {
        Init();
        backGroundImage.color = skillData.skillInfo._skillCardColor;
        skillImage.sprite = skillData.skillInfo._skillImage;
        skillName.text = skillData.skillInfo._skillName;
        skillInfo.text = skillData.skillInfo._skillExplanation;
        skillCost.text = skillData.skillInfo._skillCost.ToString();
        currentSkill = skillData;


        typeText = skillData.skillInfo.skilltypeText;
        value = skillData.skillInfo.value;
        skillText = skillData.skillInfo._skillEffectColor;
        target = skillData.skillInfo.target;
        skillEffect = skillData._SetSkill._Methods;
        _motion = skillData.skillInfo._animationClip;
        _skillVFX = skillData.skillInfo._poolType;
        //skillData.SetFunc();
        //skillEffect = skillData._SetSkill.skillFunction;
    }

    void FixedUpdate()
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
                        if (fadeImage.color.a <= 0.4f)
                        {
                            Color color = fadeImage.color;
                            color.a += 0.05f;
                            fadeImage.color = color;
                        }
                    }
                    else
                    {
                        if (fadeImage.color.a >= 0)
                        {
                            Color color = fadeImage.color;
                            color.a -= 0.05f;
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
                _battleUI.SpawnSkillEffectText(value.ToString(), skillText, transform.position);
                _battleUI.SpawnSkillEffectText(typeText, Color.white, transform.position + new Vector3(0, 30, 0));

                //if(_mainModule._animation.GetClip(_motion.name) == null)
                _mainModule._animatorOverride["Motion"] = _motion;
                _mainModule._animator.Play("Motion");
                //_mainModule._animation.PlayQueued(_motion.name);

                GameObject vfx = PoolManager.Instance.Pop(_skillVFX).gameObject;
                vfx.transform.position = hit.point;

                _mainModule._BattleModule.StartCoroutine("ShakeBattleCam", 1f);

                //skillEffect.Invoke(hit.collider.gameObject);
                _battleUI.currentSkillCard.Remove(gameObject);
                _battleUI.currentSkill.Remove(currentSkill);
                _battleUI.cardCount--;

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
