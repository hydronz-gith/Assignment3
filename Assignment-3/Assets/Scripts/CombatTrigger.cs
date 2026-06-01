using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatTrigger : MonoBehaviour
{
    public string combatSceneName = "Combat";
    public bool disableAfterTrigger = true;
    private bool _triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        HandleTrigger(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleTrigger(other.gameObject);
    }

    private void HandleTrigger(GameObject other)
    {
        if(_triggered)
        return;
        if(!other.CompareTag("Player"))
        return;

        _triggered = true;

        PlayerPrefs.SetString("OverworldScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        if(disableAfterTrigger) gameObject.SetActive(false);
        {
            SceneManager.LoadScene(combatSceneName);
        }
    }
}