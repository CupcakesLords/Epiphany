using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fox : MonoBehaviour
{
    public Sprite Avatar;

    Sprite temp;

    public DialogObject diag_obj;

    private void Start()
    {
        temp = InputManager.Instance.Interaction.GetComponent<Image>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        InputManager.Instance.Interaction.SetActive(true);
        InputManager.Instance.Interaction.GetComponent<Image>().sprite = Avatar;
        InputManager.Instance.Interaction.GetComponent<Button>().onClick.RemoveAllListeners();
        InputManager.Instance.Interaction.GetComponent<Button>().onClick.AddListener(AddDialog);
    }

    private void AddDialog()
    {
        InputManager.Instance.Dialog.GetComponent<DialogMenu>().Push(diag_obj, Avatar, "Fox");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InputManager.Instance.Interaction.SetActive(false);
        InputManager.Instance.Interaction.GetComponent<Image>().sprite = temp;
        InputManager.Instance.Interaction.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
