using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    public Sprite Avatar;

    Sprite temp;

    //temp
    public GameObject level1;
    public GameObject MainMenu;

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
        SoundManager.instance.TurnToMainMusic();

        level1.SetActive(false);
        MainMenu.SetActive(true);
        GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i< a.Length; i++)
        {
            Destroy(a[i]);
        }
        GameObject[] b = GameObject.FindGameObjectsWithTag("Drop");
        for (int i = 0; i < b.Length; i++)
        {
            Destroy(b[i]);
        }
        InputManager.Instance.SetBackToNoHero();
        InputManager.Instance.Interaction.SetActive(false);
        InputManager.Instance.Interaction.GetComponent<Image>().sprite = temp;
        InputManager.Instance.Interaction.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InputManager.Instance.Interaction.SetActive(false);
        InputManager.Instance.Interaction.GetComponent<Image>().sprite = temp;
        InputManager.Instance.Interaction.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
