using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance { get { return _instance; } }

    [HideInInspector]
    public Joystick joystick;
    [HideInInspector]
    public Button Attack;
    [HideInInspector]
    public Button Skill1;
    [HideInInspector]
    public Image Skill1Mask;

    [HideInInspector]
    public Hero CurrentHero;
    [HideInInspector]
    public Transform CurrentTransform;
    [HideInInspector]
    public HeroObject CurrentData;

    private float SkillTimer = 0;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
    }

    public void SetHero(Hero hero, Transform trans, HeroObject data)
    {
        CurrentHero = hero; CurrentTransform = trans; CurrentData = data;
        Attack.onClick.AddListener(() => hero.Auto());
        Skill1.onClick.AddListener(() => hero.Skill()); 
    }

    public void AttackClick()
    {

    }

    public void SkillClick()
    {
        StartCoroutine(SkillTimerCountDown());
    }

    private IEnumerator SkillTimerCountDown()
    {
        float originalSize;
        originalSize = Skill1.GetComponent<RectTransform>().rect.height;

        SkillTimer = CurrentData.SkillCDR;
        Skill1Mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize);
        Skill1.interactable = false;

        while (SkillTimer > 0)
        {
            SkillTimer -= Time.deltaTime;
            Skill1Mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize * (SkillTimer/CurrentData.SkillCDR));
            yield return null;
        }

        SkillTimer = 0;
        Skill1Mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        Skill1.interactable = true;
    }

    void Update()
    {
        
    }
}
