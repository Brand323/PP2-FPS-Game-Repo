using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    [Header("Panning Settings")]
    public float panSpeed = 20f;
    public float panBorderThickness = 100f;
    public Vector2 panLimit;

    [Header("Zoom Settings")]
    public float scrollSpeed = 20f;
    public float minY = 20f;   // Min Zoom
    public float maxY = 120f;  // Max Zoom

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        // Camera panning using the mouse on edge of screen
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        // Limits camera movement
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        // Camera zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);  // Clamps zoom between minY and maxY

        transform.position = pos;
    }
}

