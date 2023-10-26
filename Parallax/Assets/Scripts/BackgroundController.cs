using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private float skySpeed;
    private Transform skyObj;
    private Material skyMaterial;
    private float offset = 0;


    // Start is called before the first frame update
    void Start()
    {
        skyObj = transform.GetChild(0);
        MeshRenderer mr = skyObj.GetComponent<MeshRenderer>();
        skyMaterial = mr.material;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 skyPos = Camera.main.transform.position;
        skyPos.z = 0;
        skyObj.position = skyPos;
        offset += skySpeed * Time.deltaTime;
        Vector2 voff = new Vector2(offset, 0);
        skyMaterial.SetTextureOffset("_MainTex", voff);
    }

}
