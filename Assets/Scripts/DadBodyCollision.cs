using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadBodyCollision : MonoBehaviour
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
            UnityEngine.Debug.Log("Dad body COLLISION by throwable");
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
