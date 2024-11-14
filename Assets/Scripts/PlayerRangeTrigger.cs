using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerRangeTrigger : MonoBehaviour
{
    public GameObject playerRef;
    // make a list of gameobjects
    private List<GameObject> objectsInRange = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // on entering the trigger, add the enemy to the list
    private void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log("Adding object: " + other.gameObject.name);
        objectsInRange.Add(other.gameObject);
    }

    // on exiting the trigger, remove the enemy from the list
    private void OnTriggerExit(Collider other)
    {

        objectsInRange.Remove(other.gameObject);

    }

    public List<GameObject> getObjectsInRange()
    {
        return objectsInRange;
    }
}

