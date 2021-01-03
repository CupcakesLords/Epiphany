using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatMenu : MonoBehaviour
{
    public Joystick joystick;
    public Button Attack;
    public Button Skill1;
    public Button Ult;
    public Image SkillMask;
    public Image UltimateMask;
    public Image HPBar;
    public GameObject Interaction;

    void Start()
    {
        InputManager.Instance.joystick = joystick;
        InputManager.Instance.Attack = Attack;
        InputManager.Instance.Skill1 = Skill1;
        InputManager.Instance.Skill1Mask = SkillMask;
        InputManager.Instance.UltimateMask = UltimateMask;
        InputManager.Instance.HP_Bar = HPBar;
        InputManager.Instance.Ultimate = Ult;
        InputManager.Instance.Interaction = Interaction;
    }
}
