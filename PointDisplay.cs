using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointDisplay : MonoBehaviour
{
    [SerializeField] int suns = 100;
    Text pointText;

    // Start is called before the first frame update
    void Start()
    {
        pointText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        pointText.text = suns.ToString();
    }

    public bool HaveEnoughStars(int amount)
    {
        return suns >= amount;
    }

    public void AddSuns(int amount)
    {
        suns += amount;
        UpdateDisplay();
    }

    public void SpendSuns(int amount)
    {
        if (suns >= amount)
        {
            suns -= amount;
            UpdateDisplay();
        }

    }

}
