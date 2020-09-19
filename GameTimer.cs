using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Level timer in second")]
    [SerializeField] float levelTime;
    //public bool timeFinished;
    bool triggeredLevelFinished = false;
    float maxDelay;

    private void Start()
    {
        maxDelay = FindObjectOfType<AttackerSpawner>().maxSpawnDelay;
    }
    


    // Update is called once per frame
    void Update()
    {
        if (triggeredLevelFinished) { return; } //If its true then exit this method
        GetComponent<Slider>().value = Time.timeSinceLevelLoad / levelTime; //so our slider value is 0-1. If time since loaded is 5 seconds and level time is 10, then outcome is 0.5 so slider should be half way

        bool timeFinished = (Time.timeSinceLevelLoad >= (levelTime+ maxDelay));
        if (timeFinished)
        {
            FindObjectOfType<LevelController>().LevelTimerFinished();
            triggeredLevelFinished = true;
        }

    }
}
