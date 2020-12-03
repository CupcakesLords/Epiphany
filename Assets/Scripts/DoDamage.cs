using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    [HideInInspector]
    public int damage = 0;

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }
}
