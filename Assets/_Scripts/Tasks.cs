using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasks : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerController player;
    public GameObject Hayroll;

    static public bool didHayroll = false;
    bool[] taskList;
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        taskList = new bool[6];

        for(int i =0; i < 6;i++)
        {
            taskList[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Bring Axe to Farmer Task
        if(player.hitInfo.collider != null)
        {
            if(player.hitInfo.collider.tag == "Farmer")
            {
                if(Input.GetButtonDown("Interact") && player.currentItem.name == "Axe")
                {
                    Debug.Log("Respect Fam");
                    taskList[0] = true;
                    Destroy(player.currentItem);
                    player.currentlyHolding = false;
                    player.currentItem = new GameObject();
                }
            }
        }

        //Roll Hayroll to Spot *collision code in chekCollision.cs*
        if(didHayroll == true)
        {
            taskList[1] = true;
            Debug.Log("task complete");
            didHayroll = false;
        }

        //Bring bucket to Chicken Coop
        if(player.hitInfo.collider != null)
        {
            if(player.hitInfo.collider.name == "ChickenCoop")
            {
                if(Input.GetButtonDown("Interact") && player.currentItem.name == "bucket")
                {
                    Debug.Log("Fed the Chickens");
                    taskList[2] = true;
                    Destroy(player.currentItem);
                    player.currentlyHolding = false;
                    player.currentItem = new GameObject();
                }
            }
        }
    }
}
