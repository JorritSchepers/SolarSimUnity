using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpaceshipBehaviour : MonoBehaviour
{
    public bool spaceshipMode = false;
    public float speed = 0.1f;

    private float velocity = 50;
    private TextMeshProUGUI speedDisplay;
    private GameObject crosshair;
    private GameObject normalUI;
    private GameObject spaceshipUI;
    private GameObject exitButton;

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

        // Set the camera to the back of the spaceship facing the front
        Camera.main.transform.position = transform.position + transform.forward * 1;
        Camera.main.transform.rotation = transform.rotation;

        // Move the spaceship
        if (Input.GetKey(KeyCode.O))
        {
            velocity += speed;
        }
        if (Input.GetKey(KeyCode.L))
        {
            if (velocity > speed)
            {
                velocity -= speed;
            }
            else
            {
                velocity = 0;
            }
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

        speedDisplay.text = "Speed: " + velocity;
    }

    public void ActivateSpaceShip()
    {
        transform.position = Camera.main.transform.position;
        spaceshipMode = true;
        Camera.main.fieldOfView = 90;
        crosshair.gameObject.SetActive(true);
        speedDisplay.gameObject.SetActive(true);
        transform.LookAt(new Vector3(0, 0, 0));
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
