using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fox : MonoBehaviour
{
    public Sprite Avatar;

    Sprite temp;

    private void Start()
    {
        temp = InputManager.Instance.Interaction.GetComponent<Image>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InputManager.Instance.Interaction.SetActive(true);
        InputManager.Instance.Interaction.GetComponent<Image>().sprite = Avatar;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InputManager.Instance.Interaction.SetActive(false);
        InputManager.Instance.Interaction.GetComponent<Image>().sprite = temp;
    }
}
