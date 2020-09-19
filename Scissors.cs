using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : MonoBehaviour
{
    [SerializeField] GameObject scissorsVFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Vector3.right * 0.000001f;
    }


    private void OnTriggerEnter2D(Collider2D objectToDelete)
    {
        Debug.Log("I triggered");
        GameObject objectToBeDeleted = objectToDelete.gameObject;

        if (objectToBeDeleted.GetComponent<Defender>())
        {
            GameObject VFXscissorsVFX=Instantiate(scissorsVFX,transform.position,transform.rotation);//instantiate particles
            Destroy(objectToBeDeleted.gameObject,0.2f);
            Destroy(gameObject, 1f);
            Destroy(VFXscissorsVFX, 2f);
        }

    }
}
