using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float eyeSpeed;
    private Quaternion baseOrientation;
    private float mouseH;
    private float mouseV;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.baseOrientation = this.transform.localRotation;
        this.mouseV = 0;
        this.mouseH = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.mouseH += Input.GetAxis("Mouse X");
        this.mouseV += Input.GetAxis("Mouse Y");

        Quaternion rotX, rotY;
        float angleY = this.mouseH * eyeSpeed;
        float angleX = this.mouseV * eyeSpeed;
        rotY = Quaternion.AngleAxis(angleY, Vector3.up);
        rotX = Quaternion.AngleAxis(angleX, Vector3.left);
        this.transform.localRotation = this.baseOrientation * rotY * rotX;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
