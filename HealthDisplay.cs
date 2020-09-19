using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] int lives = 5;
    [SerializeField] int damage=1;
    Text liveText;

    // Start is called before the first frame update
    void Start()
    {
        liveText = GetComponent<Text>();
        UpdateDisplay();

    }

    private void UpdateDisplay()
    {
        liveText.text = lives.ToString();
    }

//Attack
    public void TakeLife() //in that bracket I could pass different amount so that bigger enemy takes more life
    {
        lives -= damage;
        if (lives >= 0)
        {
            UpdateDisplay();
        }
        else if (lives <= 0)
        {
            //FindObjectOfType<LevelLoader>().LoadGameOver();
            FindObjectOfType<LevelController>().HandleLooseCondition();
        }

    }
}
