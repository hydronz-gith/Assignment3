using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private CombatHandler health;

    [SerializeField]
    private float animationSpeed = 10f;

    [SerializeField]
    private RectTransform barRect;
    private RectTransform bottomBar;
    private RectTransform topBar;

    [SerializeField]
    private RectMask2D mask;

    [SerializeField]
    private TMP_Text hpIndicator;

    private float maxRightMask;
    private float initialRightMask;

    private float fullWidth;
    private float TargetWidth => health.PlayerHP * fullWidth / health.playerMaxHP; 
    private Coroutine adjustBarWidthCoroutine;


    private IEnumerator AdjustBarWidth(int amount)
    {
        var suddenChangeBar = amount >= 0 ? bottomBar : topBar;
        var slowChangeBar = amount >= 0 ? topBar : bottomBar;
        suddenChangeBar.SetWidth(TargetWidth);
        while (Mathf.Abs(suddenChangeBar.rect.width - slowChangeBar.rect.width) > 1f)
        {
            slowChangeBar.SetWidth(
                Mathf.Lerp(slowChangeBar.rect.width, TargetWidth, Time.deltaTime * animationSpeed));
            yield return null;
        }
        slowChangeBar.SetWidth(TargetWidth);
    }

    private void Start()
    {
        fullWidth = barRect.rect.width;
        
        maxRightMask = barRect.rect.width - mask.padding.x - mask.padding.z;
        hpIndicator.SetText($"{health.PlayerHP} / {health.playerMaxHP}");
        initialRightMask = mask.padding.z;
    }

    public void SetValue(int newValue)
    {
        var targetWidth = newValue * maxRightMask / health.playerMaxHP;
        var newRightMask = maxRightMask + initialRightMask - targetWidth;
        var padding = mask.padding;
        padding.z = newRightMask;
        mask.padding = padding;
        hpIndicator.SetText($"{newValue} / {health.playerMaxHP}");
    }

    public void Change(int amount)
    {
    //health.PlayerHP = Mathf.Clamp(health.PlayerHP + amount, 0, health.playerMaxHP);
    if (adjustBarWidthCoroutine != null)
        {
            StopCoroutine(adjustBarWidthCoroutine);
        }

        adjustBarWidthCoroutine = StartCoroutine(AdjustBarWidth(amount));
    }

    private void Update()
    {
        // this is where "when it gets damage, change it" will be.
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //health.PlayerHP = Mathf.Max(health.PlayerHP - 20);
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            
        }        
    }
}
