using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using UnityEngine;

public class DadScript : MonoBehaviour
{
    private int anger = 0;
    private Vector3 scale = new Vector3(0.002f, 0.002f, 0.002f);
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject body;

    [SerializeField] private GameObject throwablePrefab;

    private const float INVULN_TIME = 0.7f;
    private bool isInvuln = false;

    //sounds
    [SerializeField] private AudioClip[] audios;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // make a static instance singleton
    public static DadScript instance;
    public static DadScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DadScript>();
            }
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            head.transform.localScale += scale;
        }
    }

    // Triggered by player kicking dad.
    public void getKicked()
    {
        if (isInvuln)
            return;
        // play the audios[0] sound
        AudioSource.PlayClipAtPoint(audios[0], transform.position);
        StartCoroutine(angerFlash(1));
    }

    // triggered with OnCollisionEnter on DadHead
    public void headHitByThrowable(GameObject obj)
    {
        if (isInvuln)
            return;
        UnityEngine.Debug.Log("head hit by " + obj.name + " " + obj.tag);
        AudioSource.PlayClipAtPoint(audios[0], transform.position);
        StartCoroutine(angerFlash(2));
        StartCoroutine(replaceThrowableCoroutine(1, obj));
    }

    // triggered with OnCollisionEnter on DadBody
    public void bodyHitByThrowable(GameObject obj)
    {
        if (isInvuln)
            return;
        UnityEngine.Debug.Log("body hit by " + obj.name + " " + obj.tag);
        AudioSource.PlayClipAtPoint(audios[0], transform.position);
        StartCoroutine(angerFlash(1));
        StartCoroutine(replaceThrowableCoroutine(1, obj));
    }

    IEnumerator angerFlash(int increment)
    {
        anger += increment;
        // make dad red
        head.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
        body.GetComponent<Renderer>().material.color = Color.red;
        // make dad invincible
        isInvuln = true;
        // trigger headGrow coroutine
        StartCoroutine(headGrow(increment));
        yield return new WaitForSeconds(INVULN_TIME);
        // make dad normal
        head.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
        body.GetComponent<Renderer>().material.color = Color.white;

        isInvuln = false;
    }

    IEnumerator headGrow(int increment)
    {
        // make dad's head grow
        for (int i = 0; i < 100; i++)
        {
            //head.transform.localScale += scale;
            // make the head transform scale multiplicatively
            for (int j = 0; j < increment; j++)
            {
                head.transform.localScale = new Vector3(head.transform.localScale.x + scale.x,
                                                        head.transform.localScale.y + scale.y,
                                                        head.transform.localScale.z + scale.z);
            }
            yield return new WaitForSeconds(INVULN_TIME / 100.0f);
        }
    }

    IEnumerator spawnThrowableCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        float x = UnityEngine.Random.Range(-2.0f, 2.0f);
        if (x < 0) x--; else x++;
        float z = UnityEngine.Random.Range(-2.0f, 2.0f);
        if (z < 0) z--; else z++;
        Vector3 spawnLocation = new Vector3(x, 1.0f, z);
        Instantiate(throwablePrefab, spawnLocation, Quaternion.identity);
    }
    IEnumerator replaceThrowableCoroutine(float delay, GameObject throwable)
    {
        yield return new WaitForSeconds(delay);
        float x = UnityEngine.Random.Range(-2.0f, 2.0f);
        if (x < 0) x--; else x++;
        float z = UnityEngine.Random.Range(-2.0f, 2.0f);
        if (z < 0) z--; else z++;
        Vector3 spawnLocation = new Vector3(x, 1.0f, z);
        // set throwable velocity to 0 and place at spawnLocation
        throwable.GetComponent<Rigidbody>().velocity = Vector3.zero;
        // stop all rotations
        throwable.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        throwable.transform.position = spawnLocation;
    }
}
