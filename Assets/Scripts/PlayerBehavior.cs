using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public GameObject interactHitboxVolume;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private const float mouseSensitivity = 500f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void moveInDirection(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * 5;
    }
    void kick()
    {
        List<GameObject> objectsInRange = interactHitboxVolume.GetComponent<PlayerRangeTrigger>().getObjectsInRange();
        if (objectsInRange == null)
        {
            UnityEngine.Debug.Log("objectsInRange is null.");
            return;
        }
        foreach (GameObject obj in objectsInRange)
        {
            UnityEngine.Debug.Log("Kicking object: " + obj.name);
            // apply force to object in direction between player and object
            Vector3 direction = obj.transform.position - transform.position;
            direction.Normalize();
            Rigidbody targetRb = obj.GetComponent<Rigidbody>();
            if (targetRb != null)
            {
                targetRb.AddForce(direction * 1000);
            }
        }

    }

    void FixedUpdate()
    {
        float forwardSpeed = 0.0f;
        float rightSpeed = 0.0f;
        if (Input.GetKey(KeyCode.W))
        {
            forwardSpeed++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rightSpeed--;
        }
        if (Input.GetKey(KeyCode.S))
        {
            forwardSpeed--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rightSpeed++;
        }
        Vector3 direction = (transform.forward * forwardSpeed + transform.right * rightSpeed);
        direction.y = 0;
        direction.Normalize();
        moveInDirection(direction);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            kick();
        }
        rotatePlayer();


    }
    void rotatePlayer()
    {
        //Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        ////// Adjust vertical rotation and clamp it to prevent over-rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;


        // Apply rotations:
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
