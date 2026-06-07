using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class CombatTrigger : MonoBehaviour
{
    [Header("Scene Settings")]
    public string combatSceneName = "CombatScene";
 
    [Header("Transition")]
    public float fadeDuration = 0.5f;
 
    [Header("Enemy Info")]
    public string enemyID = "DefaultEnemy";
    public int enemyHP = 80;
 
    private bool _triggered = false;
 
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) BeginTransition();
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) BeginTransition();
    }
 
 
    private void BeginTransition()
    {
        if (_triggered) return;
        _triggered = true;
 
        GetComponent<Collider>()?.enabled   = false;
        GetComponent<Collider2D>()?.enabled = false;

        PlayerPrefs.SetString("OverworldScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString("EnemyID",        enemyID);
        PlayerPrefs.SetInt   ("EnemyHP",         enemyHP);
        PlayerPrefs.Save();
 
        StartCoroutine(FadeAndLoad());
    }
 
    private IEnumerator FadeAndLoad()
    {
        GameObject fadeObj  = new GameObject("FadeOverlay");
        Canvas canvas       = fadeObj.AddComponent<Canvas>();
        canvas.renderMode   = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;
 
        DontDestroyOnLoad(fadeObj);
 
        UnityEngine.UI.Image panel = fadeObj.AddComponent<UnityEngine.UI.Image>();
        panel.color = new Color(0, 0, 0, 0);
 
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            panel.color = new Color(0, 0, 0, Mathf.Clamp01(elapsed / fadeDuration));
            yield return null;
        }
 
        panel.color = Color.black;
 
        AsyncOperation load = SceneManager.LoadSceneAsync(combatSceneName);
        load.allowSceneActivation = false;
 
        while (load.progress < 0.9f)
            yield return null;
 
        load.allowSceneActivation = true;
 
        yield return null;
 
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            panel.color = new Color(0, 0, 0, 1f - Mathf.Clamp01(elapsed / fadeDuration));
            yield return null;
        }
 
        Destroy(fadeObj);
    }
}