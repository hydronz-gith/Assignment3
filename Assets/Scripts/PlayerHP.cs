using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100;
    [SerializeField] public FloatValueSO currentHealth;
    //[SerializeField] public float flashTime = 0.2f;

    public void Start()
    {
        currentHealth.Value = maxHealth;
        //may need to make this 100 if current is dropping.
    }

    public void Reduce(int damage)
    {
        currentHealth.Value -= damage / maxHealth;
        //CreateHitFeedback();
        if (currentHealth.Value <= 0)
        {
            Die();
        }
    }

    //private void CreateHitFeedback()
    //{
    //   // Instantiate(bloodParticle, transform.position, Quaternion.identity);
    //    StartCoroutine(FlashFeedback());
    //}

    //private IEnumerator FlashFeedback()
    //{
    //    renderer.material.SetInt("_Flash", 1);
    //    yield return new WaitForSeconds(flashTime);
    //    renderer.material.SetInt("_Flash", 0);
    //}

    private void Die()
    {
        Debug.Log("Died");
        currentHealth.Value = 1;
    }
}
