using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadScript : MonoBehaviour
{
    private int anger = 0;
    [Serialize] private GameObject head;
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
            anger += 10;
            head.transform.localScale = anger * 100;
        }
    }
}
