using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collidingObject) //brackets empty because we dont need to know what bumped into us
    {
        FindObjectOfType<HealthDisplay>().TakeLife();
        Destroy(collidingObject.gameObject);
        Debug.Log("I am here");
    }
}
