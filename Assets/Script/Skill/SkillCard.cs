using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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


    //Vector2 originPos;
    //Vector2 mousePoint;

    bool isfirst = false;
    bool m_IsButtonDowning = false;

    Color32 skillText;

    string target;

    LocalPoolManager poolLocalM;

    MainModule _mainModule;
    BattleUI _battleUI;

    PoolType _skillVFX;

    AnimationClip _motion;

    Skill currentSkill;

    int[] value;
    string typeText;

    AudioClip soundClip;
    private void OnEnable()
    {
        m_IsButtonDowning = false;
    }

    private void Init()
    {
        _battleUI = GameObject.Find("UIManager").GetComponent<BattleUI>();
        _mainModule = GameObject.Find("Player").GetComponent<MainModule>();
        poolLocalM = GameObject.Find("LocalPool : Card").GetComponent<LocalPoolManager>();
        //backGroundImage = GetComponent<Image>();
        //skillImage = transform.GetChild(0).GetComponent<Image>();
        //skillName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        //skillInfo = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        //fadeImage = transform.GetChild(3).GetComponent<Image>();
        //skillCost = transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>();
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

        soundClip = skillData.skillInfo.clip;

        _skillVFX = skillData.skillInfo._poolType;
        typeText = skillData.skillInfo.skilltypeText;
        value = skillData.skillInfo.value;
        skillText = skillData.skillInfo._skillEffectColor;
        target = skillData.skillInfo.target;
        skillEffect = skillData._SetSkill._Methods;
        _motion = skillData.skillInfo._animationClip;
        //skillData.SetFunc();
        //skillEffect = skillData._SetSkill.skillFunction;
    }

    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (m_IsButtonDowning)
        {
            //if (!isfirst)
            //{
            //    //originPos = transform.position;
            //    isfirst = !isfirst;
            //}

            //if (isfirst)
            //{
            Vector3 pos = Input.mousePosition;
            //pos.z = Camera.main.farClipPlane;

            //mousePoint = Camera.main.ScreenToWorldPoint(pos);
            //mousePoint = pos;
            //transform.position = mousePoint;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //Vector3 mPosition = pos;
                //Vector3 oPosition = _mainModule.dirObj.transform.position;

                //mPosition.z = oPosition.z - Camera.main.transform.position.z;

                //float dy = mPosition.y - oPosition.y;
                //float dx = mPosition.x - oPosition.x;

                //float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

                float angle = Mathf.Atan2(_mainModule.dirObj.transform.position.y - pos.y,
                    _mainModule.dirObj.transform.position.x - pos.x) * Mathf.Rad2Deg;

                //_mainModule.dirObj.transform.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                // _mainModule.dirObj.transform.rotation = Quaternion.Euler(0f, rotateDegree - 6f, 0);
                _mainModule.dirObj.transform.LookAt(hit.point);
                _mainModule.dirObj.transform.eulerAngles = new Vector3(0, _mainModule.dirObj.transform.eulerAngles.y, 0);


                if (hit.collider.CompareTag(target))
                {
                    if (fadeImage.color.a <= 1f)
                    {
                        //_mainModule.dirObj.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0, 0);
                        _mainModule.dirObj.SetActive(true);
                        Color color = fadeImage.color;
                        color.a += 0.08f;
                        fadeImage.color = color;
                        return;
                    }
                }
                else
                {
                    _mainModule.dirObj.SetActive(false);
                    //_mainModule.dirObj.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1);

                }
                //}
            }
        }
        if (fadeImage.color.a >= 0)
        {
            Color color = fadeImage.color;
            color.a -= 0.08f;
            fadeImage.color = color;
        }
    }

    public void PointerDown()
    {
        m_IsButtonDowning = true;
    }

    public void PointerUp()
    {
        _mainModule.dirObj.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int cost = Int32.Parse(skillCost.text);
        int count = 0;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.CompareTag(target) && _battleUI.cost >= cost)
            {
                AudioManager.PlayAudio(soundClip);
                foreach (var method in skillEffect)
                {
                    method.Invoke(null, new object[] { hit.collider.gameObject, value[count], skillText });
                    //_battleUI.SpawnSkillEffectText(value[count].ToString(), skillText, transform.position);
                    count++;
                }
                if (_mainModule.playerDataSO.isLetterOpener)
                {
                    _mainModule.playerDataSO.threeCnt++;
                }
                if (_mainModule.playerDataSO.isLetterOpener && _mainModule.playerDataSO.threeCnt % 3 == 0)
                {
                    BattleManager battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

                    for (int i = 0; i < battleManager.fieldEnemies.Count; i++)
                    {
                        HpModule hp = battleManager.fieldEnemies[i].GetComponent<HpModule>();
                        hp.GetHit(5, Color.red);
                    }
                }

                _battleUI.cost -= cost;
                _battleUI.costTxt.text = $"{_battleUI.cost + "/" + _battleUI.maxCost}";

                _battleUI.SetCost(cost);

                hit.collider.gameObject.GetComponent<HpModule>();

                //CardEffect();
                //if(_mainModule._animation.GetClip(_motion.name) == null)
                _mainModule._animatorOverride["Motion"] = _motion;
                _mainModule._animator.Play("Motion", 0);



                GameObject vfx = PoolManager.Instance.Pop(_skillVFX).gameObject;
                vfx.transform.position = hit.point;
                //_mainModule._animation.PlayQueued(_motion.name);

                _mainModule._BattleModule.StartCoroutine("ShakeBattleCam", 1f);

                //skillEffect.Invoke(hit.collider.gameObject);

                _battleUI.currentSkillCard.Remove(gameObject);
                _battleUI.currentSkill.Remove(currentSkill);

                _battleUI.cemeteryCardDeck.Add(currentSkill);

                _battleUI.cardCount--;
                transform.SetParent(poolLocalM.transform);
                PoolManager.Instance.Push(PoolType.Card, gameObject);

                _battleUI.SpawnSkillEffectText(typeText, Color.white, hit.point);//transform.position + new Vector3(0, 35, 0));
                return;
            }
        }
        //transform.position = originPos;
        _mainModule.dirObj.SetActive(false);
        m_IsButtonDowning = false;
    }

    public override void Init_Pop()
    {
    }

    public override void Init_Push()
    {
    }
}