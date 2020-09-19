using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeShooter : MonoBehaviour
{
    [SerializeField] GameObject bee_RightBullet, bee_LeftBullet, gunLeft, gunRight;
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
            if (dSqrToAttacker < closestDistanceSqr && IsInLane)
            {
                closestDistanceSqr = dSqrToAttacker;
                currentAttacker = attacker;
            }
        }
        if (!currentAttacker)
        {
            animator.SetBool("IsAttacking", false); 
            animator.SetBool("IsLeft", false);
            animator.SetBool("IsRight", false);
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
            animator.SetBool("IsLeft", false);
            animator.SetBool("IsRight", true);
        }
        else if (IsToLeft)
        {
            animator.SetBool("IsAttacking", true);
            animator.SetBool("IsRight", false);
            animator.SetBool("IsLeft", true);

        }
    }


    public void FireLeft()
    {
        GameObject newProjectile = Instantiate(bee_LeftBullet, gunLeft.transform.position, Quaternion.identity) as GameObject; // we need is as GO so that we can place it in hierarchy. If its only Instantiate, thats only "object" which gives us less control.
        newProjectile.transform.parent = bulletParent.transform;
    }

    public void FireRight()
    {
        GameObject newProjectile = Instantiate(bee_RightBullet, gunRight.transform.position, Quaternion.identity) as GameObject; // we need is as GO so that we can place it in hierarchy. If its only Instantiate, thats only "object" which gives us less control.
        newProjectile.transform.parent = bulletParent.transform;
    }

}
