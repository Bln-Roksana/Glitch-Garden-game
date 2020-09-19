using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] int sunCost = 100;
 

    public void AddSuns (int amount)
    {
        FindObjectOfType<PointDisplay>().AddSuns(amount);
    }
    public int GetSunCost()
    {
        return sunCost;
    }

    private void OnMouseDown()
    {
        Defender selectedDefender = FindObjectOfType<DefenderSpawner>().defender;
        if (selectedDefender.GetComponent<Scissors>())
        {
            Defender scissors=Instantiate(selectedDefender, transform.position, transform.rotation) as Defender; //we are calling a method which will return the position that will feed into first called method
        }
        

    }
}
