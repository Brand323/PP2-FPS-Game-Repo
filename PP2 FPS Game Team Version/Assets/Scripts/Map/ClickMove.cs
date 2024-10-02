using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ClickMove : MonoBehaviour
{
    public Camera MapCamera;
    [SerializeField] float movespeed = 5f;

    private void Start()
    {
        GameObject mapCameraObject = GameObject.FindGameObjectWithTag("MapCamera");
        MapCamera = mapCameraObject.GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MapCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Map"))
                {
                    StartCoroutine(MovePlayerOnMap(hit.point));
                }
            }
        }
    }

    private IEnumerator MovePlayerOnMap(Vector3 movePosition)
    {

        while (Vector3.Distance(transform.position, movePosition ) > 0.1f)
        {
            transform.position =  Vector3.MoveTowards(transform.position, movePosition, movespeed*Time.deltaTime);
        yield return null;
        }

    }
}
