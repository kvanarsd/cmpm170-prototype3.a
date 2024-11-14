using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DadHeadCollision : MonoBehaviour
{
    [SerializeField] private GameObject dadParentObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Throwable")
        {
            UnityEngine.Debug.Log("Dad head COLLISION by throwable");
            dadParentObj.GetComponent<DadScript>().headHitByThrowable();
            destroyObjectAfterDelay(collision.gameObject);
        }
    }

    IEnumerator destroyObjectAfterDelay(GameObject throwableObj)
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(throwableObj);
    }
}
