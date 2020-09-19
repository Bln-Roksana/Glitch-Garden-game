using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D defenderItIsFighting)
    {
        GameObject objectItIsFighting = defenderItIsFighting.gameObject;

        if (objectItIsFighting.GetComponent<TurtleHit>())
        {
            //do nothing
        }

        else if (objectItIsFighting.GetComponent<Defender>())
        {
            GetComponent<Attacker>().Attack(objectItIsFighting); // get MINE component called attacker
        }
    }
}
