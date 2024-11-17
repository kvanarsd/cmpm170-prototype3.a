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

    private GameObject heldObject;
    private const float THROW_FORCE = 1000f;

    private bool isSpacePressed = false;
    private float forwardSpeed = 0.0f;
    private float rightSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // lock mouse cursor to screen center
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        forwardSpeed = 0.0f;
        rightSpeed = 0.0f;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpacePressed = true;
        }
        Vector3 direction = (transform.forward * forwardSpeed + transform.right * rightSpeed);
        direction.y = 0;
        direction.Normalize();
        moveInDirection(direction);

        rotatePlayer();
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
    }

    void FixedUpdate()
    {
        if (isSpacePressed)
        {
            handleInteractEvent();
            isSpacePressed = false;
        }
    }

    void handleInteractEvent()
    {
        if (heldObject)
        {
            // throw the object. make it active. spawn it in front of the player. and apply force in direction of player facing direction
            heldObject.SetActive(true);
            heldObject.transform.position = transform.position + transform.forward * 2;
            Rigidbody targetRb = heldObject.GetComponent<Rigidbody>();
            if (targetRb != null)
            {
                targetRb.AddForce(transform.forward * THROW_FORCE);
            }
            interactHitboxVolume.GetComponent<PlayerRangeTrigger>().removeObjectIfExists(heldObject);
            heldObject = null;
        }
        else
        {
            List<GameObject> objectsInRange = interactHitboxVolume.GetComponent<PlayerRangeTrigger>().getObjectsInRange();
            if (objectsInRange == null)
            {
                UnityEngine.Debug.Log("objectsInRange is null.");
                return;
            }
            foreach (GameObject obj in objectsInRange)
            {
                if (obj.tag == "Throwable")
                {
                    heldObject = obj;
                    obj.SetActive(false);
                    return;
                }
            }
            // wonderfully efficient code. i know. 
            foreach (GameObject obj in objectsInRange)
            {
                if (obj.tag == "Dad")
                {
                    DadScript.instance.getKicked();
                }
            }
        }
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
