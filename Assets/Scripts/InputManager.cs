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
    public Button Ultimate;
    [HideInInspector]
    public Image Skill1Mask;
    [HideInInspector]
    public Image UltimateMask;

    [HideInInspector]
    public Image HP_Bar;

    [HideInInspector]
    public Hero CurrentHero;
    [HideInInspector]
    public Transform CurrentTransform;
    [HideInInspector]
    public HeroObject CurrentData;

    private float SkillTimer = 0;
    private float UltimateTimer = 0;
    private bool onPause = false;

    public GameObject Ded; //temporary
    public GameObject Siliciter; //temporary
    private float timer = 10f;

    private void Update() //temporary
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }
        //Instantiate(Siliciter, new Vector3(0, 0, 0), Quaternion.identity);
        timer = 10f;
    }

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
        if (CurrentHero == null)
        {
            CurrentHero = hero; CurrentTransform = trans; CurrentData = data;
        }
        Attack.onClick.AddListener(() => hero.Auto()); 
        Skill1.onClick.AddListener(() => hero.Skill());
        Ultimate.onClick.AddListener(() => hero.Ultimate()); 
    }

    public void SetBackToOneHero()
    {
        Attack.onClick.RemoveAllListeners();
        Skill1.onClick.RemoveAllListeners();
        Ultimate.onClick.RemoveAllListeners();

        Attack.onClick.AddListener(() => CurrentHero.Auto());
        Skill1.onClick.AddListener(() => CurrentHero.Skill());
        Ultimate.onClick.AddListener(() => CurrentHero.Ultimate());
    }

    public void SetBackToNoHero()
    {
        CurrentHero = null; CurrentTransform = null; CurrentData = null;
        Attack.onClick.RemoveAllListeners();
        Skill1.onClick.RemoveAllListeners();
        Ultimate.onClick.RemoveAllListeners();
    }

    public void AttackClick()
    {

    }

    public void SkillClick()
    {
        StartCoroutine(SkillTimerCountDown());
    }

    bool SkillOnCD = false;

    private IEnumerator SkillTimerCountDown()
    {
        SkillOnCD = true;

        float originalSize;
        originalSize = Skill1.GetComponent<RectTransform>().rect.height;

        SkillTimer = CurrentData.SkillCDR;
        Skill1Mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize);
        Skill1.interactable = false;

        while (SkillTimer > 0)
        {
            if (onPause == false)
            {
                SkillTimer -= Time.deltaTime;
                Skill1Mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize * (SkillTimer / CurrentData.SkillCDR));
            }
            yield return null;
        }

        SkillTimer = 0;
        Skill1Mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        Skill1.interactable = true;

        SkillOnCD = false;
    }

    public void UltimateClick()
    {
        StartCoroutine(UltimateTimerCountDown());
    }

    bool UltimateOnCD = false;

    private IEnumerator UltimateTimerCountDown()
    {
        UltimateOnCD = true;

        float originalSize;
        originalSize = Ultimate.GetComponent<RectTransform>().rect.height;

        UltimateTimer = CurrentData.UltimateCDR;
        UltimateMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize);
        Ultimate.interactable = false;

        while (UltimateTimer > 0)
        {
            if (onPause == false)
            {
                UltimateTimer -= Time.deltaTime;
                UltimateMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize * (UltimateTimer / CurrentData.UltimateCDR));
            }
            yield return null;
        }

        UltimateTimer = 0;
        UltimateMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        Ultimate.interactable = true;

        UltimateOnCD = false;
    }

    public void EnableUI(bool on)
    {
        Attack.interactable = on;
        if(!SkillOnCD)
            Skill1.interactable = on;
        if(!UltimateOnCD)
            Ultimate.interactable = on;
    }

    public void Resurrect()
    {
        PauseUI(false); Ded.GetComponent<DeadMenu>().PopOut();
        CurrentHero.Resurrect();
    }

    public void PauseUI(bool on)
    {
        EnableUI(!on);
        onPause = on;
    }
}
