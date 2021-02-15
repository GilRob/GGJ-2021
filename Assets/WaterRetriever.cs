using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRetriever : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            Debug.Log("water retrieved");
            other.gameObject.transform.position = new Vector3(-81.4f, 1.0f, -236.58f);
        }
    }
}
