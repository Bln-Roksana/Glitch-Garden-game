using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random; //Will set Random to use UnityEngine's Random class


public class Attacker : MonoBehaviour
{
    [SerializeField] float currentSpeed = 1f;
    GameObject currentTarget;
    Animator attackerAnimation;
    float slowerAnimationSpeed = 0.2f;
    float frozenSeconds = 10f;
    bool frozen = false;
    [SerializeField] GameObject[] slimeParticle;
    [SerializeField] GameObject blobPlacement;
    [SerializeField] GameObject birdGoneVFX;
    int i = 0;
    Vector2 birdEndPosition;
    float fallingSpeed = 6f;


    private void Awake()
    {
        FindObjectOfType<LevelController>().AttackerSpawned();
    }

    void Start()
    {
        attackerAnimation = GetComponent<Animator>();
        int yEndPos = Random.Range(1, 5);
        birdEndPosition = new Vector2(transform.position.x, yEndPos);
        //birdEndPosition = new Vector2(2, 2);
    }

    void Update()
    {
        if (GetComponent<Bird>())
        {
            if (transform.position.y != birdEndPosition.y)
            {
                Vector2 newPos = Vector2.MoveTowards(transform.position, birdEndPosition, fallingSpeed * Time.deltaTime);
                transform.position = newPos;
            }
            else if (transform.position.y == birdEndPosition.y)
            {
                attackerAnimation.SetTrigger("inPosition");
                //instatiate puff to mark defender's death - do this in BIRD script so that this is only done if there is a defender in place (only if collided/trigger was triggered).
                GetComponent<Collider2D>().enabled = true;
                transform.position = transform.position + Vector3.right * 0.000001f;
                Invoke("PuffDisappear", 2.5f);
            }
        }
        else //any other attacker
        {
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
        UpdateAnimationState();
        }
    }

    private void PuffDisappear()
    {
        //instantiate moment the attacker disappear and delete the object
        GameObject goneVFXObject = Instantiate(birdGoneVFX, transform.position, transform.rotation); // we instatiate coz my attacker gets destroyed
        Destroy(goneVFXObject, 1f);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        if (levelController != null) { levelController.AttackerKilled(); }
    }


    private void UpdateAnimationState()
    {
        if (!currentTarget)
        {
            GetComponent<Animator>().SetBool("IsAttacking", false);
        }
    }

    public void SetMovementSpeedWalk(float speed)
    {
        if (frozen) return;
        currentSpeed = speed;
    }

    public void SetMovementSpeed(float speed)
    {
        currentSpeed = speed;
        Debug.Log(currentSpeed);
    }


    public void Attack(GameObject target)
    {
        //check if slime and if yes then set parameter
        GetComponent<Animator>().SetBool("IsAttacking", true);
        currentTarget = target;

        if (currentTarget.GetComponent<Slime>())
        {
            currentTarget.GetComponent<HealthSlime>().currentSlimeDies.AddListener(FreezeUponDeath);
        }
        
    }


    public void StrikeCurrentTarget(int damage)
    {
        if (!currentTarget) { return; } //if there is no current target,then "get out of here"  
        if (currentTarget.GetComponent<Slime>())
        {
            HealthSlime healthSlime = currentTarget.GetComponent<HealthSlime>();
            healthSlime.DealDamage(damage);
        }
        else
        {
            Health health = currentTarget.GetComponent<Health>();
            health.DealDamage(damage);
        }
        
    }

    public void FreezeUponDeath()
    {
        StartCoroutine(ManageAnimationSpeed());
    }

    IEnumerator ManageAnimationSpeed() //only for slime character
    {
        attackerAnimation.speed = slowerAnimationSpeed;
        currentSpeed=0.1f;
        frozen = true;
        //TODO instantiate the particles - can I loop through all 4 particles and do foreach but how to add some delay
        //StartCoroutine(InstantiateParticles()); ideally foreach loop with delay between
        Invoke("InstantiateBlobs", 3f); //0 particle
        Invoke("InstantiateBlobs", 6f); //1 particle
        Invoke("InstantiateBlobs", 8f); //2 particle
        Invoke("InstantiateBlobs", 10f); //3 particle
        yield return new WaitForSeconds(frozenSeconds);

        attackerAnimation.speed = 1f;
        if (GetComponent<Rhino>())
        {
            currentSpeed = 0f;
        }
        else
        {
            currentSpeed = 1f;
        }

        frozen = false;
    }

    private void InstantiateBlobs()
    {
        if (i == 4)
        {
            i = 0;
        } 
        GameObject slimeBlobs = Instantiate(slimeParticle[i], blobPlacement.transform.position, transform.rotation) as GameObject;
        Destroy(slimeBlobs, 6f);
        i++;
    }

    //IEnumerator InstantiateParticles()   
    //{
    //    foreach (slimeParticle in slimeParticles)

    //}

    /* public void Attack(GameObject target)
     {
         //check if slime and if yes then set parameter
         GetComponent<Animator>().SetBool("IsAttacking", true);
         currentTarget = target;

     }*/
}
