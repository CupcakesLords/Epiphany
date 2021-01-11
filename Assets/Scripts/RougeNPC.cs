using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RougeNPC : MonoBehaviour
{
    public Sprite Avatar;
    public GameObject Rouge;

    Sprite temp;

    Collider2D tempCollision;

    private void Start()
    {
        temp = InputManager.Instance.Interaction.GetComponent<Image>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        //if (collision.GetComponent<Rouge>() != null) //if rouge return
        //    return;
        if (collision.GetComponent<Hero>().ReturnName() == Rouge.GetComponent<Hero>().ReturnName())
            return;

        tempCollision = collision;

        InputManager.Instance.Interaction.SetActive(true);
        InputManager.Instance.Interaction.GetComponent<Image>().sprite = Avatar;
        InputManager.Instance.Interaction.GetComponent<Button>().onClick.RemoveAllListeners();
        InputManager.Instance.Interaction.GetComponent<Button>().onClick.AddListener(SpawnRouge);
    }

    void SpawnRouge()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        tempCollision.GetComponent<Animator>().Play("Base Layer.Die", 0, 0);

        float AutoTimer = 1f;
        while (AutoTimer > 0)
        {
            AutoTimer -= Time.deltaTime;
            yield return null;
        }

        Vector3 tempPos = tempCollision.gameObject.transform.position;
        Destroy(tempCollision.gameObject);
        InputManager.Instance.SetBackToNoHero();
        GameObject shadow = Instantiate(Rouge, tempPos, Quaternion.identity);

        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        for(int i = 0; i < rooms.Length; i++)
        {
            rooms[i].GetComponent<Room>().SetPlayer(shadow.transform);
        }

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
