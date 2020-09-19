using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MakeAppear : MonoBehaviour
{
    float duration_long = 5f;
    float duration_short = 2f;
    TextMeshProUGUI gameTitle_text;
    Color myColor;
    float delayOnThisScreen;

    //cache
    LevelLoader levelLoader;

    void Start()
    {
        gameTitle_text = GetComponent<TextMeshProUGUI>();
        myColor = gameTitle_text.color;
        myColor.a = 0f;
        levelLoader = FindObjectOfType<LevelLoader>();
        delayOnThisScreen = levelLoader.noOfSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            if (myColor.a <= 0.8f)
            {
                float ratio_long = Time.time / duration_long;
                myColor.a = Mathf.Lerp(0, 1, ratio_long);
            }
            else if (myColor.a > 0.8f)
            {
                float ratio_short= Time.time / duration_short;
                myColor.a = Mathf.Lerp(0, 1, ratio_short);
            }
        }
        else if (currentSceneIndex == 1)
        {
            float ratio_short = (Time.time- delayOnThisScreen) / duration_short;
            //float ratio_short = (Time.time - 5f) / duration_short;
            myColor.a = Mathf.Lerp(0, 1, ratio_short);
        }

        
        gameTitle_text.color = myColor;
    }
}
