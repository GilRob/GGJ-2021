using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasks : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerController player;
    public GameObject Hayroll;
    public GameObject Egg;
    static public bool didHayroll = false;
    bool[] taskList;

    bool gotEggs = false;

//toilet variables
    float startTime = 0f;
    float holdTime = 3f;

    float timer = 0f;
    public Material cleanToilet;
///////////////////////////////////

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
////////////////////////////////////////////////////////////////////////////////////
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
                    player.currentItem.name = "Empty";
                }
            }
        }
////////////////////////////////////////////////////////////////////////////////////
        //Roll Hayroll to Spot *collision code in chekCollision.cs*
        if(Vector3.Distance(this.transform.position, Hayroll.transform.position) <= 3f && taskList[1] == false)
        {
            Hayroll.GetComponent<Rigidbody>().AddForce(this.transform.forward * 0.5f);
        }

        if(didHayroll == true)
        {
            taskList[1] = true;
            Debug.Log("task complete");
            didHayroll = false;
        }
//////////////////////////////////////////////////////////////////////////////////
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
                    player.currentItem.name = "Empty";
                }
            }
        }
/////////////////////////////////////////////////////////////////////////////////////////////
        //Collect Chicken Eggs
        if(player.hitInfo.collider != null)
        {
            if(player.hitInfo.collider.name == "ChickenCoop" && taskList[3] == false)
            {
                if(Input.GetButtonDown("Interact") && player.currentItem.name == "Empty")
                {
                    player.currentItem = Instantiate(Egg, player.holdPosition.position, player.holdPosition.rotation);
                    player.currentlyHolding = true;
                    gotEggs = true;
                }
            }

             if(player.hitInfo.collider.name == "EggBasket" && taskList[3] == false)
            {
                if(Input.GetButtonDown("Interact") && player.currentItem.name == "Egg(Clone)" && gotEggs == true)
                {
                    taskList[3] = true;
                    Destroy(player.currentItem);
                    player.currentlyHolding = false;
                    player.currentItem = new GameObject();
                    player.currentItem.name = "Empty";
                    Debug.Log("Got The Eggs");
                }
            }

        }    
////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //clean toilet
        if(player.hitInfo.collider != null)
        {
            if(player.hitInfo.collider.name == "toilet" && taskList[4] == false)
            {
                 if(Input.GetButtonDown("Interact") && player.currentItem.name == "toiletBrush")
                {
                    startTime = Time.deltaTime;
                }

                if(Input.GetButton("Interact") && player.currentItem.name == "toiletBrush")
                {
                    timer += Time.deltaTime;

                   // Debug.Log(Time.deltaTime);
                    if (timer >= (startTime + holdTime))
                    {
                        taskList[4] = true;
                        timer = 0;
                        player.hitInfo.collider.GetComponent<Renderer>().material = cleanToilet;
                        Destroy(player.currentItem);
                        player.currentlyHolding = false;
                        player.currentItem = new GameObject();
                        player.currentItem.name = "Empty";
                        Debug.Log("Toilet Cleaned");
                        
                    }
                }
                else
                {
                    timer = 0;
                }
            }
        }
///////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    //clean poop in barn
    if(player.hitInfo.collider != null)
        {
            if(player.hitInfo.collider.name == "poop" && taskList[5] == false)
            {
                if(Input.GetButtonDown("Interact") && player.currentItem.name == "pitchFork")
                {
                    startTime = Time.deltaTime;
                }

                if(Input.GetButton("Interact") && player.currentItem.name == "pitchFork")
                {
                    timer += Time.deltaTime;

                   // Debug.Log(Time.deltaTime);
                    if (timer >= (startTime + holdTime))
                    {
                        taskList[5] = true;
                        timer = 0;
                        Destroy(player.hitInfo.collider.gameObject);
                        Destroy(player.currentItem);
                        player.currentlyHolding = false;
                        player.currentItem = new GameObject();
                        player.currentItem.name = "Empty";
                        Debug.Log("Poop Cleaned");
                        
                    }
                }
                else
                {
                    timer = 0;
                }
            }
        }
/////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}
