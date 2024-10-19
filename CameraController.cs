using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float maxXboundary;
    [SerializeField] private float minXboundary;
    [SerializeField] private float maxZboundary;
    [SerializeField] private float minZboundary;
    [SerializeField] private float cameraSpeed;
    private void Update()
    {
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            movement += new Vector3(-1, 0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            movement += new Vector3(1, 0, -1);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movement += new Vector3(1, 0, 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movement += new Vector3(-1, 0, -1);
        }
        Vector3 pos = transform.position;
        pos += movement * cameraSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minXboundary, maxXboundary);
        pos.z = Mathf.Clamp(pos.z, minZboundary, maxZboundary);
        transform.position = pos;
    }
}
