using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public PlayerHP health;
    [SerializeField] private RectTransform barRect;
    [SerializeField] private RectMask2D mask;
    [SerializeField] private TMP_Text hpIndicator;

    private float maxRightMask;
    private float initialRightMask;

    private void Start()
    {        
        maxRightMask = barRect.rect.width - mask.padding.x - mask.padding.z;
        hpIndicator.SetText($"{health.currentHealth.Value} / {health.maxHealth}");
        initialRightMask = mask.padding.z;
    }

    public void SetValue(int newValue)
    {
        var targetWidth = newValue * maxRightMask / health.maxHealth;
        var newRightMask = maxRightMask + initialRightMask - targetWidth;
        var padding = mask.padding;
        padding.z = newRightMask;
        mask.padding = padding;
        hpIndicator.SetText($"{newValue} / {health.maxHealth}");
    }
}
