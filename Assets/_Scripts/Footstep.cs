using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{

    public AudioClip[] stoneClips;
    public AudioClip[] woodClips;
    public AudioClip[] grassClips;

    public AudioSource audioSource;
    private float timer;
    RaycastHit hit;
    public Collider playerCollider;
    public CharacterController controller;
    private Vector3 mLastPosition;
    void Update()
    {
        float speed = (transform.position - this.mLastPosition).magnitude / Time.deltaTime;
        this.mLastPosition = transform.position;

        if (speed > 0)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5)
            {
                Step();
                timer = 0;
            }
           
        }
        else
        {
            timer = 0;
        }
        //Debug.Log(timer); 
        //Debug.Log(Time.deltaTime);
    }
    private void Awake()
    {
        Debug.Log("Awake");

        //audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        //Debug.Log("Played");
        checkHit();
        //Debug.Log("Hit " + hit);

        AudioClip clip = GetRandomClip();
       // Debug.Log("Played this" + clip);
        audioSource.PlayOneShot(clip);
    }

    bool checkHit()
    {
        //Debug.Log("Checked");
        return Physics.Raycast(transform.position, Vector3.down, out hit, playerCollider.bounds.extents.y + 0.5f);
    }

    private AudioClip GetRandomClip()
    {
        //Debug.Log(grassClips.Length);
        Debug.Log(hit.collider.tag);
        if (hit.collider != null && hit.collider.tag == "Stone")
            return stoneClips[UnityEngine.Random.Range(0, stoneClips.Length)];
        else if (hit.collider != null && hit.collider.tag == "Wood")
            return woodClips[UnityEngine.Random.Range(0, woodClips.Length)];
        else if (hit.collider != null && hit.collider.tag == "Dirt")
            return grassClips[UnityEngine.Random.Range(0, grassClips.Length)];
        else
            return grassClips[UnityEngine.Random.Range(0, grassClips.Length)];

    }

}

