using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float up, down, left, right;

    public List<GameObject> Doors;

    bool isActive; Transform player; bool playerIsIn; List<GameObject> EnemyInstances;

    public List<GameObject> Enemies;

    public List<Vector3> SpawnPosition;

    void Start()
    {
        isActive = false; playerIsIn = false; EnemyInstances = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SetPlayer(Transform user)
    {
        player = user;
    }

    void CreateEnemies()
    {
        for(int i = 0; i < SpawnPosition.Count; i++)
        {
            int j = Random.Range(0, Enemies.Count);
            GameObject temp = Instantiate(Enemies[j], SpawnPosition[i], Quaternion.identity);
            EnemyInstances.Add(temp);
        }
    }

    private void Update()
    {
        if(isActive) //as room is in combat, doesnt care about whether or not play is in or out
        {
            bool combatOver = true;
            for(int i = 0; i < EnemyInstances.Count; i++)
            {
                if (!EnemyInstances[i]) { } //this enemy destroyed
                else { combatOver = false; } //this enemy not destroyed
            }
            if(combatOver)
            {
                EnemyInstances.Clear();
                for (int i = 0; i < Doors.Count; i++)
                {
                    Doors[i].GetComponent<Door>().Open();
                }
                isActive = false;
            }
            else
            {

            }
            return;
        }

        if(player.position.x > left && player.position.x < right && player.position.y < up && player.position.y > down)
        {
            if(playerIsIn == false)
            {
                PlayerGoingIn();
            }
            return;
        }
        else
        {
            if(playerIsIn == true)
            {
                PlayerGoingOut();
            }
            return;
        }
    }

    private void PlayerGoingIn()
    {
        //Debug.Log("going in");
        playerIsIn = true;

        if (isActive) //room is already in combat
            return;

        for (int i = 0; i < Doors.Count; i++)
        {
            Doors[i].GetComponent<Door>().Close();
        }

        CreateEnemies();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            Transform temp = players[i].transform;
            if (!(temp.position.x > left && temp.position.x < right && temp.position.y < up && temp.position.y > down))
            {
                players[i].transform.position = new Vector3(left + ((right - left) / 2), down + ((up - down) / 2) + i, 0);
            }
        }

        isActive = true;
    }

    private void PlayerGoingOut()
    {
        //Debug.Log("going out");
        playerIsIn = false;
    }
}
