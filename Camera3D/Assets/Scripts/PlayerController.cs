using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int bulletPoolSize;
    [SerializeField] private float eyeSpeed;
    private Quaternion baseOrientation;
    private float mouseH;
    private float mouseV;
    private ObjectPool bulletPool;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.baseOrientation = this.transform.localRotation;
        this.mouseV = 0;
        this.mouseH = 0;
        this.bulletPool = new ObjectPool(bulletPrefab, bulletPoolSize);
    }

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

        if (Input.GetButtonDown("Fire1"))
        {
            this.Shoot();
        }
    }

    private void Shoot()
    {
        GameObject newShoot = this.bulletPool.GetFromPool();
        if (newShoot != null)
        {
            newShoot.transform.position = this.transform.position;
            newShoot.GetComponent<BulletController>().direction = this.transform.forward;
        }
    }
}
