using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; //Will set Random to use UnityEngine's Random class
using UnityEngine.SceneManagement;

public class AttackerSpawner : MonoBehaviour
{
    bool spawn = true;
    bool start = true;
    float timer = 0;

    [SerializeField] float minSpawnDelay=1f;
    public float maxSpawnDelay=6f;
    float adjustedDelay;
    //Attacker currentAttacker;
    [SerializeField] Attacker[] attackerPrefabArray;


    IEnumerator Start()
    {
        adjustedDelay = maxSpawnDelay - PlayerPrefsController.GetDifficulty();
        Debug.Log("Playing at difficulty" + adjustedDelay);
        var startTime = Time.time;
        while (spawn)
        {//while spawn is true
            if (timer < 15)
            {
                timer = Time.time - startTime;
            }
            
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, adjustedDelay));
            //SpawnAttacker();
            //Debug.Log("scene index is"+ SceneManager.GetActiveScene().buildIndex);
            if (timer < 15 && SceneManager.GetActiveScene().buildIndex >= 26)
            {
                //Debug.Log($"Timer: {timer}. RealTime: {Time.time - startTime}. Don't spawn. ");
                continue;
            }
            //else if (timer < 25 && SceneManager.GetActiveScene().buildIndex == 27)
            //{
            //    Debug.Log($"Timer: {timer}. RealTime: {Time.time - startTime}. Don't spawn. ");
            //    continue;
            //}

            SpawnAttacker();

        }
    }


    public void StopSpawning()
    {
        spawn = false;
    }

    private void SpawnAttacker()
    {
        var attackerIndex = Random.Range(0, attackerPrefabArray.Length);
        Spawn(attackerPrefabArray[attackerIndex]);
    }

    private void Spawn(Attacker myAttacker) 
    {
        //if it has a bird script on it then...
        if (myAttacker.GetComponent<Bird>())
        {
            int xBegPos = Random.Range(1, 7);
            Vector2 birdBegPosition = new Vector2(xBegPos, 7);
            Attacker newAttacker = Instantiate(myAttacker, birdBegPosition, transform.rotation) as Attacker;
            newAttacker.transform.parent = transform; // me having my identity... assignining that new attacker to that parent
        }
           
        else
        {
            Attacker newAttacker = Instantiate(myAttacker, transform.position, transform.rotation) as Attacker;
            newAttacker.transform.parent = transform; // me having my identity... assignining that new attacker to that parent
        }

    }

    //private void SpawnTheShield()
    //{
    //    Vector2 position = new Vector2(Random.Range(0.5f, 11.5f), 12.5f);
    //    Instantiate(shield1, position, Quaternion.identity);
    //}




}
