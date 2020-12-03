using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadMenu : MonoBehaviour
{
    public Button Revive;

    void Start()
    {
        Revive.onClick.AddListener(InputManager.Instance.Resurrect);
    }

    void Update()
    {
        
    }
}
