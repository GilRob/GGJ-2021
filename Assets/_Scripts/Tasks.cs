using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasks : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerController player;
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.hitInfo.collider != null)
        {
            if(player.hitInfo.collider.tag == "Farmer")
            {
                if(Input.GetButtonDown("Interact") && player.currentItem.name == "Axe")
                {
                    Debug.Log("Respect Fam");
                    Destroy(player.currentItem);
                    player.currentlyHolding = false;
                    player.currentItem = new GameObject();
                }
            }
        }
    }
}
