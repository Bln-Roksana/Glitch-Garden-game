using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSlime : MonoBehaviour
{
    [SerializeField] public int attackerHealth = 100;
    [SerializeField] GameObject deathVFX;

    public UnityEvent currentSlimeDies = new UnityEvent();


    public void DealDamage(int damage)
    {
        attackerHealth -= damage;
        if (attackerHealth <= 0)
        {
            TriggerDeathVFX();
            SlimeDies();
            DestroyObject();
        }

    }

    private void DestroyObject()
    {
        GetComponent<Animator>().SetTrigger("IsDead");
        if (FindObjectOfType<Ghost>())
        {
            Invoke("Die", 0.3f);
            GetComponent<Animator>().SetBool("avoidSlime", true);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void SlimeDies()
    {
        if (currentSlimeDies != null && FindObjectOfType<Slime>())
        {
            currentSlimeDies.Invoke();
        }
    }

    private void TriggerDeathVFX()
    {
        if (!deathVFX) { return; } // if no particles then nothing
        GameObject deathVFXObject = Instantiate(deathVFX, transform.position, transform.rotation); // we instatiate coz my attacker gets destroyed
        Destroy(deathVFXObject, 1f);
    }
}
