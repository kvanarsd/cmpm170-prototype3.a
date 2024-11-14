using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class DadScript : MonoBehaviour
{
    private int anger = 0;
    private Vector3 scale = new Vector3(0.01f, 0.01f, 0.01f);
    [SerializeField] private GameObject head;
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
}
