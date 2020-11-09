using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatMenu : MonoBehaviour
{
    public Joystick joystick;
    public Button Attack;
    public Button Skill1;
    public Image SkillMask;

    void Start()
    {
        InputManager.Instance.joystick = joystick;
        InputManager.Instance.Attack = Attack;
        InputManager.Instance.Skill1 = Skill1;
        InputManager.Instance.Skill1Mask = SkillMask;
    }

    void Update()
    {
        
    }
}
