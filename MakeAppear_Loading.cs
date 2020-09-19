using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakeAppear_Loading : MonoBehaviour
{
    float duration_long = 3f;
    TextMeshProUGUI loading_text;
    Color myColor;
    float timer=0;

    void Start()
    {
        loading_text = GetComponent<TextMeshProUGUI>();
        myColor = loading_text.color;
        myColor.a = 0f;
        loading_text.color = myColor;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2f&& myColor.a<1)
        {
            float ratio_long = (Time.time-2f) / duration_long;
            myColor.a = Mathf.Lerp(0, 1, ratio_long);
            loading_text.color = myColor;
        }
       // StartCoroutine(MakeItAppear());
    }

    //IEnumerator MakeItAppear()
    //{
    //    yield return new WaitForSeconds(2f);
    //    Debug.Log("I am here");
    //    float ratio_long = (Time.time-2f) / duration_long;
    //    myColor.a = Mathf.Lerp(0, 1, ratio_long);
    //    Debug.Log("..and here");
    //    loading_text.color = myColor;
    //}

}
