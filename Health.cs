using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] public int attackerHealth = 100;
    [SerializeField] GameObject deathVFX;



    public void DealDamage(int damage)
    {
        attackerHealth -= damage;
        if (attackerHealth <= 0)
        {
            TriggerDeathVFX();
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


    private void TriggerDeathVFX()
    {
        if (!deathVFX) { return; } // if no particles then nothing
        GameObject deathVFXObject=Instantiate(deathVFX, transform.position, transform.rotation); // we instatiate coz my attacker gets destroyed
        Destroy(deathVFXObject, 1f);
    }
}
