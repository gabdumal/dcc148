using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBola : MonoBehaviour
{
    public float speed = 5;
    public float gravity = 20;
    public float impulse = 18;
    
    private float vy = 3;
    private const float MIN_Y = -4;
    private const float MAX_X = 8;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        
        float dx = Input.GetAxis("Horizontal");        
        pos.x += dx * Time.deltaTime * speed;
        
        if(pos.x > MAX_X)
    	{
	    pos.x = MAX_X;
    	}
    	else if (pos.x < -MAX_X)
    	{
    	    pos.x = -MAX_X;
    	}
        
        vy -= gravity * Time.deltaTime;
        pos.y += vy * Time.deltaTime;
        
        if(pos.y < MIN_Y){
            vy = impulse;
            pos.y = MIN_Y;
        }
        
        transform.position = pos;
    }
}
