using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audeinceJump : MonoBehaviour
{


    public float jumpHeight = 2f;     
    public float jumpSpeed = 2f;      

    private Vector3 originalPosition;
    private float randomOffset;

    // Start is called before the first frame update

    void Start()
    { 
        originalPosition = transform.position;
        randomOffset = Random.Range(0f, Mathf.PI * 2);
    }



    // Update is called once per frame

    void Update()
    {
        float newY = Mathf.Sin(Time.time * jumpSpeed + randomOffset) * jumpHeight;
        transform.position = new Vector3(originalPosition.x, originalPosition.y + newY, originalPosition.z);
    }
}





