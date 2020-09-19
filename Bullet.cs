using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D attackerThatCollidesWithIt)
    {
        var health = attackerThatCollidesWithIt.GetComponent<Health>(); //<---refers to that script component called "Health"
        var attacker = attackerThatCollidesWithIt.GetComponent<Attacker>(); //<-- to know that it is an attacker

        if (attacker && health)
        {
            health.DealDamage(damage);
            Destroy(gameObject);
        }

        //reduce health

    }
}
 