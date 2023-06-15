using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpaceshipBehaviour : MonoBehaviour
{
    public bool spaceshipMode = false;
    public float speedStep = 1f;

    public float velocity = 40;
    private TextMeshProUGUI speedDisplay;
    private GameObject crosshair;
    private GameObject normalUI;
    private GameObject spaceshipUI;
    private GameObject exitButton;
    public int maxSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        speedDisplay = GameObject.Find("SpaceShipSpeed").GetComponent<TextMeshProUGUI>();
        speedDisplay.gameObject.SetActive(false);

        crosshair = GameObject.Find("Crosshair");
        crosshair.gameObject.SetActive(false);

        GameObject.Find("SpaceshipButton").GetComponent<Button>().onClick.AddListener(delegate () { this.ActivateSpaceShip(); });
        GameObject.Find("ExitSpaceshipButton").GetComponent<Button>().onClick.AddListener(delegate () { this.DisableSpaceShip(); });

        normalUI = GameObject.Find("UICanvas");
        spaceshipUI = GameObject.Find("SpaceshipUI");
        spaceshipUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!spaceshipMode)
        {
            return;
        }

        // if (transform.rotation.x > 0)
        // {
        //     transform.Rotate(Vector3.right * -.05f * 6, Space.World);
        // }
        // else
        // {
        //     transform.Rotate(Vector3.forward * -0.1f * 6, Space.World);
        // }

        Camera.main.transform.position = transform.position;
        Camera.main.transform.rotation = transform.rotation;

        // Move the spaceship
        if (Input.GetKey(KeyCode.O))
        {
            if (velocity < maxSpeed)
            {
                velocity += speedStep;
            }
        }
        if (Input.GetKey(KeyCode.L))
        {
            if (velocity > speedStep)
            {
                velocity -= speedStep;
            }
            else
            {
                velocity = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            spaceshipUI.SetActive(!spaceshipUI.activeSelf);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * 100);

        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {

            transform.Rotate(Vector3.back * Time.deltaTime * 100);
        }

        // Rotate the spaceship
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.down * Time.deltaTime * 50);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 50);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.left * Time.deltaTime * 100);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.right * Time.deltaTime * 100);
        }

        transform.Translate(Vector3.forward * Time.deltaTime * velocity);

        UpdateSpeedText();
    }

    private void UpdateSpeedText()
    {
        var bars = velocity / 3;
        var s = "";

        for (var i = 0; i < bars; i++)
        {
            s += "|";
        }
        speedDisplay.text = s;
    }

    public void ActivateSpaceShip()
    {
        transform.position = Camera.main.transform.position;
        spaceshipMode = true;
        Camera.main.fieldOfView = 90;
        crosshair.gameObject.SetActive(true);
        speedDisplay.gameObject.SetActive(true);
        transform.LookAt(
            Camera.main.GetComponent<CameraController>().targetPlanet.transform.position
        );
        normalUI.SetActive(false);
        spaceshipUI.SetActive(true);
    }

    public void DisableSpaceShip()
    {
        spaceshipMode = false;
        Camera.main.fieldOfView = 50;
        crosshair.gameObject.SetActive(false);
        speedDisplay.gameObject.SetActive(false);
        normalUI.SetActive(true);
        spaceshipUI.SetActive(false);
    }
}
