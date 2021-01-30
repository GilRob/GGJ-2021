using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider col) 
    {
        if(col.gameObject.name == "hayRoll")
        {
            Tasks.didHayroll = true;
            col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            col.GetComponent<Rigidbody>().useGravity = false;
            Destroy(this.gameObject);
        }
    }
}
