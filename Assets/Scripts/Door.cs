using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite inactive;
    public Sprite active;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Close()
    {
        GetComponent<SpriteRenderer>().sprite = active;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Open()
    {
        GetComponent<SpriteRenderer>().sprite = inactive;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
