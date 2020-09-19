using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    public Defender defender;
    GameObject defenderParent;

    const string DEFENDER_PARENT_NAME = "Defenders";

    private void Start()
    {
        CreateDefenderParent();
    }

    private void CreateDefenderParent()
    {
        defenderParent = GameObject.Find(DEFENDER_PARENT_NAME);
        if (!defenderParent)
        {
            defenderParent = new GameObject(DEFENDER_PARENT_NAME);
        } 
    }

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquareClicked()); //we are calling a method which will return the position that will feed into first called method
    }

    public void SetSelectedDeferender(Defender defenderToSelect)
    {
        defender = defenderToSelect;
    }

    private void AttemptToPlaceDefenderAt(Vector2 gridPos)
    {
        var SunDisplay = FindObjectOfType<PointDisplay>();
        var defenderCost = defender.GetSunCost();
        //if we have enough suns then we can spawn defender and spend the suns
        //do we have enough?
        if (SunDisplay.HaveEnoughStars(defenderCost))
        {
            SpawnDefender(gridPos);
            SunDisplay.SpendSuns(defenderCost);
        }

    }

    private Vector2 GetSquareClicked() //whenever we call this method it will return world position
    {
        Vector2 clickPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //screen to world point
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(clickPosition);
        Vector2 gridPosition = SnapToGrid(worldPosition);
        return gridPosition;
    }
    private Vector2 SnapToGrid(Vector2 rawWorldPosition)
    {
        float newX = Mathf.RoundToInt(rawWorldPosition.x);
        float newY = Mathf.RoundToInt(rawWorldPosition.y);
        return new Vector2(newX, newY);
    }

   private void SpawnDefender(Vector2 roundedPosition)
    {
        //Vector3 rotationVector = new Vector3(0, -180, 0);
        //GameObject newDefender = Instantiate(defender, roundedPosition, Quaternion.Euler(rotationVector)) as GameObject; //as GO so that we can see it in our hierarchy
        if (defender.GetComponent<Scissors>()) {
            { return; }
        }
        else
        {
            Defender newDefender = Instantiate(defender, roundedPosition, transform.rotation) as Defender; //as GO so that we can see it in our hierarchy
            newDefender.transform.parent = defenderParent.transform;
            Debug.Log("I spawned");
        }

        //Debug.Log(roundedPosition);
    }


   /* private void SpawnDefender(Vector2 roundedPosition)
    {
        if (defender.GetComponent<Scissors>())
        {
            Defender newDefender = Instantiate(defender, roundedPosition, transform.rotation) as Defender; //as GO so that we can see it in our hierarchy
            newDefender.transform.parent = defenderParent.transform;
        }
        else
        {
            Defender newDefender = Instantiate(defender, roundedPosition,transform.rotation) as Defender; //as GO so that we can see it in our hierarchy
            newDefender.transform.parent = defenderParent.transform;
        }*/

        //Debug.Log(roundedPosition);
    


    
}
