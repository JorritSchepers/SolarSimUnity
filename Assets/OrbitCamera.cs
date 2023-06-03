using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public int lookSpeed = 30;
    public int zoomSpeed = 3000;
    public int shiftFactor = 2;

    private CameraController cameraController;
    private float screenHeightFactor;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();

        var screenWidth = Screen.width;
        var screenHeight = Screen.height;
        screenHeightFactor = screenWidth / screenHeight;
    }

    // Update is called once per frame
    void Update()
    {
        // Orbit Controls
        if (Input.GetMouseButton(0))
        {
            transform.RotateAround(cameraController.targetPlanet.transform.position, Vector3.up, Input.GetAxis("Mouse X") * lookSpeed);
            transform.Translate(Vector3.up * Time.deltaTime * lookSpeed * Input.GetAxis("Mouse Y") * -3000);
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

        // On a touchscreen, if two fingers are zooming out, zoom out the camera
        if (Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            var prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            var currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            var difference = currentMagnitude - prevMagnitude;

            if (difference < 0)
            {
                transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
            }
            else if (difference > 0)
            {
                transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
            }
        }
    }
}
