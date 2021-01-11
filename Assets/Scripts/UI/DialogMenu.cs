using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogMenu : MonoBehaviour
{
    [HideInInspector]
    DialogObject dialog;
    int iterator;

    public Image Avatar;
    public Text Name;
    public Text LineOfDialog;
    public Button Escape;
    public Button Next;

    private void Start()
    {
        Escape.onClick.AddListener(Pop);
        Next.onClick.AddListener(NextLine);
    }

    public void Push(DialogObject diag, Sprite ava, string name)
    {
        gameObject.SetActive(true);
        dialog = diag;
        Avatar.sprite = ava;
        Name.text = name;
        iterator = 0;
        LineOfDialog.text = dialog.Dialog[iterator];
        iterator = 1;
    }

    public void Pop()
    {
        gameObject.SetActive(false); 
        iterator = 0;
    }

    private void NextLine()
    {
        if (iterator >= dialog.Dialog.Count)
            Pop();
        LineOfDialog.text = dialog.Dialog[iterator];
        iterator += 1;
    }
}
