using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] GameObject defenderDeathVFX;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D defenderThatCollidesWithIt)
    {
        Debug.Log("Triggerd");
        var health = defenderThatCollidesWithIt.GetComponent<Health>(); //<---refers to that script component called "Health"
        var defender = defenderThatCollidesWithIt.GetComponent<Defender>();

        if (defender && health)
        {
            health.DealDamage(damage);
            GameObject defenderGoneVFXObject = Instantiate(defenderDeathVFX, transform.position, transform.rotation); // we instatiate coz my attacker gets destroyed
            Destroy(defenderDeathVFX, 1f);
            //I destroy it in the Attacker script

        }
    }
}