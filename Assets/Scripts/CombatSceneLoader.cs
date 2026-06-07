using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CombatSceneLoader : MonoBehaviour
{
    public float returnDelay = 3f;
    public string fallbackScene = "Overworld";

    private void Start()
    {
        CombatHandler.Instance.OnCombatEnd.AddListener(OnCombatEnd);
    }

    private void OnCombatEnd(bool playerWon)
    {
        StartCoroutine(ReturnToOverworld());
    }

    private IEnumerator ReturnToOverworld()
    {
        yield return new WaitForSeconds(returnDelay);

        string targetScene = PlayerPrefs.GetString("OverworldScene", fallbackScene);
        SceneManager.LoadScene(targetScene);
    }
}
