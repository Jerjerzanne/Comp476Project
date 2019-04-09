using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOnPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Vector3 offset;
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;
    private bool onPlay = true;
    private bool onFree = false;
    float speed = 5.0f;
    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    void Start()
    {
        offset = transform.position - player.transform.position;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P) || onPlay)
        {
            onPlay = true;
            onFree = false;
            OnplayerCamera();
        }
        if (Input.GetKey(KeyCode.L) || onFree) {
            onPlay = false;
            onFree = true;
            FreeCamera();
            
        }
    }

    public void OnplayerCamera()
    {
        Quaternion resetRotation = Quaternion.Euler(90, 0, 0.0f);
        transform.rotation = resetRotation;
        transform.position = player.transform.position + offset;
    }

    public void FreeCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;

        if (Input.GetKey(KeyCode.K))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.H))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.J))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.U))
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
    }
}
