using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{

    public GameObject spaceship;
    public int timeBetweenShots;
    public GameObject shootPrefab;
    public int shootPoolSize;
    private ObjectPool shootPool;
    private Transform cannonBarrel;
    private int remainingTime;

    // Start is called before the first frame update
    void Start()
    {
        this.cannonBarrel = this.transform.GetChild(0);
        this.shootPool = new ObjectPool(shootPrefab, shootPoolSize);
        this.remainingTime = this.timeBetweenShots;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 spaceshipPosition = this.spaceship.transform.position;
        Vector3 resultVector = spaceshipPosition - this.cannonBarrel.transform.position;
        resultVector.Normalize();

        float angle = Vector3.Angle(resultVector, this.transform.right);
        this.cannonBarrel.transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        if (this.remainingTime == 0)
        {
            GameObject newShoot = this.shootPool.GetFromPool();
            if (newShoot != null)
            {
                newShoot.transform.position = this.cannonBarrel.position;
                newShoot.GetComponent<CannonShootController>().direction = resultVector;
            }
            this.remainingTime = this.timeBetweenShots;
        }
        this.remainingTime--;
    }
}
