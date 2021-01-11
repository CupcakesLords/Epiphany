using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cherry : MonoBehaviour
{
    public Sprite Avatar;

    Sprite temp;

    private int HealingValue;

    private void Start()
    {
        temp = InputManager.Instance.Interaction.GetComponent<Image>().sprite;
        HealingValue = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        InputManager.Instance.Interaction.SetActive(true);
        InputManager.Instance.Interaction.GetComponent<Image>().sprite = Avatar;
        InputManager.Instance.Interaction.GetComponent<Button>().onClick.RemoveAllListeners();
        HeroHealth temp = collision.GetComponent<HeroHealth>();
        InputManager.Instance.Interaction.GetComponent<Button>().onClick.AddListener(() => Heal(temp));
    }

    private void Heal(HeroHealth obj)
    {
        //heal
        if (obj == null)
            return;
        obj.Heal(HealingValue);
        Destroy(gameObject);
        //
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
