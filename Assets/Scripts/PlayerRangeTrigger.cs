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

    public void removeObjectIfExists(GameObject obj)
    {
        // check if object exists in the list
        if (objectsInRange.Contains(obj))
        {
            // remove object from the list
            objectsInRange.Remove(obj);
            obj.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        objectsInRange.Add(other.gameObject);
        turnBlueIfTagThrowable(other.gameObject);
    }

    private void turnBlueIfTagThrowable(GameObject obj)
    {
        if (obj.tag == "Throwable")
        {
            UnityEngine.Debug.Log("throwable obj in range");
            obj.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        removeObjectIfExists(other.gameObject);

    }

    public List<GameObject> getObjectsInRange()
    {
        return objectsInRange;
    }
}

