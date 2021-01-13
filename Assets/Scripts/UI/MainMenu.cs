using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button StartGame;
    public Button Bio;
    public Button Settings;
    public Text SettingConfig;

    //temp
    public GameObject Level1;
    public GameObject Knight;
    private Vector3 Spawn;
    //

    private void Start()
    {
        Spawn = new Vector3(1.56f, -8.37f, 0);
        StartGame.onClick.AddListener(StartTheGame);
        Settings.onClick.AddListener(ChangeMusic);
        Bio.onClick.AddListener(QuitGame);
    }
    void QuitGame()
    {
        Application.Quit();

    }

    void StartTheGame()
    {
        GameObject hero = Instantiate(Knight, Spawn, Quaternion.identity);
       
        Level1.SetActive(true);
        gameObject.SetActive(false);

        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i].GetComponent<Room>().SetPlayer(hero.transform);
        }

        SoundManager.instance.TurnToForestMusic();
    }

    void ChangeMusic()
    {
        if (SoundManager.instance.soundOn)
        {
            SettingConfig.text = "Music: Off";
        }
        else
        {
            SettingConfig.text = "Music: On";
        }
        SoundManager.instance.Sound();
    }
}
