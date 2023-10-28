using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{

    public float gravity;
    public float yImpulse;
    public float xSpeed;
    public float xMax;
    public float height;
    [SerializeField] private float ySpeed;
    [SerializeField] private bool jumping;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.jumping = true;
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInputOffset = Input.GetAxis("Horizontal");

        if (this.jumping)
        {
            this.ySpeed -= this.gravity * Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.jumping = true;
                this.ySpeed += this.yImpulse;
            }
        }

        float xDisplacement = xInputOffset * this.xSpeed * Time.deltaTime;
        bool run = Mathf.Abs(xDisplacement) > 0;
        this.animator.SetBool("Running", run);
        float yDisplacement = this.ySpeed * Time.deltaTime;

        Vector3 newPosition = new Vector3(
            Math.Clamp(this.transform.position.x + xDisplacement, -this.xMax, this.xMax),
            this.transform.position.y + yDisplacement,
            0);
        this.transform.position = newPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Transform colliderObject = collision.collider.gameObject.transform;
            this.ySpeed = 0;
            this.transform.position = new Vector3(
                this.transform.position.x,
                colliderObject.position.y + this.height / 2,
                this.transform.position.z);
            this.jumping = false;
        }
    }

}
