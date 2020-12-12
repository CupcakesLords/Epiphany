using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image mask;
    public Text HealthInfo;
    float originalSize;

    private void Awake()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetHealth(int current, int health)
    {
        float temp = (float)current / (float)health;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * temp);
        HealthInfo.text = current + " / " + health;
    }
}
