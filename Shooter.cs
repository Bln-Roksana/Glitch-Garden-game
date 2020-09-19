using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject trunk_Bullet,gun;
    //AttackerSpawner myLaneSpawner;
    Animator animator;
    GameObject bulletParent;
    Attacker currentAttacker;

    const string BULLET_PARENT_NAME = "Bullets";

    private void Start()
    {
        //SetLaneSpawner();
        animator = GetComponent<Animator>();
        CreateBulletParent();
    }

    //not important
    private void CreateBulletParent()
    {
        bulletParent = GameObject.Find(BULLET_PARENT_NAME);
        if (!bulletParent)
        {
            bulletParent = new GameObject(BULLET_PARENT_NAME);
        }
    }

    private void Update()
    {
        SetCurrentAttacker();
    }



    private void SetCurrentAttacker()
    {
        currentAttacker = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        Attacker[] attackers = FindObjectsOfType<Attacker>();
        foreach (Attacker attacker in attackers)
        {
            bool IsInLane = (Mathf.Abs(attacker.transform.position.y - transform.position.y) <= Mathf.Epsilon);
            Vector3 directionToAttacker = attacker.transform.position - currentPosition;
            float dSqrToAttacker = directionToAttacker.sqrMagnitude;
            if (dSqrToAttacker < closestDistanceSqr&& IsInLane)  
            {
                closestDistanceSqr = dSqrToAttacker;
                currentAttacker = attacker;
            }
        }
        if(!currentAttacker)
        {
            animator.SetBool("IsAttacking", false);
            return;
        }
        else
        {
            SetAnimationState();
        }

    }

    private void SetAnimationState()
    {
        bool IsToRight = transform.position.x < currentAttacker.transform.position.x;
        bool IsToLeft = transform.position.x > currentAttacker.transform.position.x;

            if (IsToRight)
            {
                animator.SetBool("IsAttacking", true);
            }
            else if(IsToLeft)
            {
                animator.SetBool("IsAttacking", false);
            }
    }


    public void Fire()
    {
        GameObject newProjectile=Instantiate(trunk_Bullet, gun.transform.position, Quaternion.identity) as GameObject; // we need is as GO so that we can place it in hierarchy. If its only Instantiate, thats only "object" which gives us less control.
        newProjectile.transform.parent = bulletParent.transform;
    }

    /*private void SetLaneSpawner()
{
    AttackerSpawner[] spawners= FindObjectsOfType<AttackerSpawner>();
    foreach (AttackerSpawner spawner in spawners)
    {
        bool IsCloseEnough= (Mathf.Abs(spawner.transform.position.y-transform.position.y) <= Mathf.Epsilon); //spawner position - defender(shooter) position : should be rougly 0 so in lane

        if (IsCloseEnough)
        {
            myLaneSpawner = spawner; //looping through all 5 spawners and find the one thats on the defenders lane
        }
    }
}*/



/*        Attacker[] attackers = FindObjectsOfType<Attacker>();
    foreach (Attacker attacker in attackers)
    {
        bool IsToRight = transform.position.x > attacker.transform.position.x;
bool IsToLeft = transform.position.x < attacker.transform.position.x;

        if (IsCloseEnough && IsToRight)
        {
            animator.SetBool("IsAttacking", true);
        }
        else if(IsCloseEnough && IsToLeft)
        {
            animator.SetBool("IsAttacking", false);
        }

    }


/*    private void Update()
{
    if (IsAttackerInLane())
    {
        //change anim to shooting one
        animator.SetBool("IsAttacking", true);
    }
    else
    {
        //change animation state to idle
        animator.SetBool("IsAttacking", false);
    }
}*/

    /*private bool IsAttackerInLane()
    {
        //if my lane spawner child count in less than or equal to 0, then return false
        if (myLaneSpawner.transform.childCount <= 0) // counting attackers in lane
        {
            return false; // no attackers in lane
        }
        else
        { //attackers in lane, now check where the attacker is. This returns IsAttackerInLane(true) regardless
            return true;
        }
    }*/


    /*private bool IsAttackerInLane()
    {
        //if my lane spawner child count in less than or equal to 0, then return false
        if (myLaneSpawner.transform.childCount <= 0) // counting attackers in lane
        {
            return false;
        }
        else
        {
            return true;
        }
    }*/
}
