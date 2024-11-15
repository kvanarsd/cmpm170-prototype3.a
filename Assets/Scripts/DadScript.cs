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
        StartCoroutine(angerFlash(1));
    }

    // triggered with OnCollisionEnter on DadHead
    public void headHitByThrowable()
    {
        // trigger angerFlash coroutine
        StartCoroutine(angerFlash(3));
        StartCoroutine(spawnThrowableCoroutine(5));
    }

    // triggered with OnCollisionEnter on DadBody
    public void bodyHitByThrowable()
    {
        // trigger angerFlash coroutine
        StartCoroutine(angerFlash(1));
        StartCoroutine(spawnThrowableCoroutine(5));
    }

    IEnumerator angerFlash(int increment)
    {
        anger += increment;
        // make dad red
        head.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
        body.GetComponent<Renderer>().material.color = Color.red;
        // make dad invincible
        //head.GetComponent<Collider>().enabled = false;
        body.GetComponent<Collider>().enabled = false;
        // trigger headGrow coroutine
        StartCoroutine(headGrow(increment));
        yield return new WaitForSeconds(INVULN_TIME);
        // make dad normal
        head.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
        body.GetComponent<Renderer>().material.color = Color.white;
        //head.GetComponent<Collider>().enabled = true;
        body.GetComponent<Collider>().enabled = true;
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
        Vector3 spawnLocation = new Vector3(Random.Range(-3.0f, 3.0f), 3.0f, Random.Range(-3.0f, 3.0f));
        Instantiate(throwablePrefab, spawnLocation, Quaternion.identity);
    }
}
