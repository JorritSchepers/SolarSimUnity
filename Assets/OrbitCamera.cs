using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public int lookSpeed = 30;
    public int zoomSpeed = 3000;
    public int shiftFactor = 2;

    private CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Orbit Controls
        if (Input.GetMouseButton(0))
        {
            transform.RotateAround(cameraController.targetPlanet.transform.position, Vector3.up, Input.GetAxis("Mouse X") * lookSpeed);
            transform.Translate(Vector3.up * lookSpeed * Input.GetAxis("Mouse Y") * -1);
        }

        // Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime * shiftFactor);
            }
            transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
        }

        // Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime * shiftFactor);
            }
            transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
        }
    }
}
