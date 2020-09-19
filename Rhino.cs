using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : MonoBehaviour
{

    Animator animator;
    Defender currentDefender;


    // Start is called before the first frame update


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetCurrentDefender();
    }

    private void SetCurrentDefender()
    {
        currentDefender = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        Defender[] defenders = FindObjectsOfType<Defender>();
        foreach (Defender defender in defenders)
        {
            bool IsInLane = (Mathf.Abs(defender.transform.position.y - transform.position.y) <= 0.1); //<= Mathf.Epsilon
            if (IsInLane == false)
            {
            Debug.Log(defender.transform.position.y - transform.position.y);
            }

            Vector3 directionToDefender = defender.transform.position - currentPosition;
            float dSqrToDefender = directionToDefender.sqrMagnitude;
            if (dSqrToDefender < closestDistanceSqr && IsInLane)
            {
                closestDistanceSqr = dSqrToDefender;
                currentDefender = defender;
            }
        }
        if (!currentDefender)
        {
            //animator.SetBool("enemyOnPath", false);
            return;
        }
        else
        {
            SetAnimationState();
        }

    }

    private void SetAnimationState()
    {
        bool IsToRight = transform.position.x < currentDefender.transform.position.x;
        bool IsToLeft = transform.position.x > currentDefender.transform.position.x;

        if (IsToRight)
        {
            Debug.Log("Iam right");
            //animator.SetBool("enemyOnPath", false);
        }
        else if (IsToLeft)
        {
            Debug.Log("Iam left");
            //animator.SetBool("enemyOnPath", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D defenderItIsFighting)
    {
        GameObject objectItIsFighting = defenderItIsFighting.gameObject;

        if (objectItIsFighting.GetComponent<Defender>())
        {
            GetComponent<Attacker>().Attack(objectItIsFighting); // get MINE component called attacker
            Invoke("DoNotAttack", 1f);
        }

    }

    private void DoNotAttack()
    {
        animator.SetBool("IsAttacking", false);
    }

    //TODO if no attackers left continue towards Base Damage collider
}
