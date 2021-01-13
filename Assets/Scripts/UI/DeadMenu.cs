using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadMenu : MonoBehaviour
{
    public Button Revive;
    public GameObject Menu;

    //temp
    //public GameObject Level1;
    //public Button Back;

    void BackToMain()
    {

    }

    void Start()
    {
        Revive.onClick.AddListener(InputManager.Instance.Resurrect);
        //Back.onClick.AddListener(BackToMain);
    }

    public void Initialize()
    {
        StartCoroutine(MenuShowUpCountDown());
    }

    public void PopOut()
    {
        GetComponent<Image>().enabled = false; 
        Menu.SetActive(false);
    }

    private IEnumerator MenuShowUpCountDown()
    {
        GetComponent<Image>().enabled = true;
        
        float SkillTimer = 2f;
        while (SkillTimer > 0)
        {
            SkillTimer -= Time.deltaTime;
            yield return null;
        }
        SkillTimer = 0;
        Menu.SetActive(true);
    }
}
