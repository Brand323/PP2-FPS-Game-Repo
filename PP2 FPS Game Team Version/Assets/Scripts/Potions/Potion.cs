using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public enum potionType {health, stamina};
    public potionType type;

    public Vector3 rotationSpeed = new Vector3(0, 100, 0);

    void Start()
    {
        //transform.rotation = Quaternion.Euler(90, 0, 0);
    }


    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
