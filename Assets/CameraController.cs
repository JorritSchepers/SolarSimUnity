using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public UniverseSim universeSim;
    public PlanetBehaviour targetPlanet;

    public Vector3 targetPos;
    public Vector3 posTarget;

    private Rigidbody rb;

    public bool cameraMoving = false;

    public float XX = 0.9f;

    public bool moveCameraPosition = true;

    private SpaceshipBehaviour spaceshipBehaviour;

    int speed = 1000;

    private GameObject ui;

    // private int cameraFlightSpeed = 250;
    // private int targetFlightSpeed = 350;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        universeSim = FindObjectOfType<UniverseSim>();
        SetTarget("Sun");
        ui = GameObject.Find("UICanvas");

        spaceshipBehaviour = FindObjectOfType<SpaceshipBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        FocusPlanetShortKeys();

        // if (cameraMoving)
        // {
        //     targetPos = Vector3.MoveTowards(targetPos, targetPlanet.transform.position, targetFlightSpeed * Time.deltaTime);
        //     // if (moveCameraPosition) 
        //     // {
        //     //     transform.position = Vector3.MoveTowards(transform.position, posTarget, cameraFlightSpeed * Time.deltaTime);
        //     // }
        //     posTarget = (targetPlanet.transform.position - new Vector3(0, 0, 0)) * XX;
        // }

        if (Input.GetKeyDown(KeyCode.U))
        {
            ui.SetActive(!ui.activeSelf);
        }

        targetPos = targetPlanet.transform.position;

        if (!spaceshipBehaviour.spaceshipMode)
        {
            transform.LookAt(targetPos);

            if (Input.GetKey(KeyCode.E))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.Translate(Vector3.back * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                // transform.Translate(Vector3.left * Time.deltaTime * speed);
                transform.RotateAround(targetPlanet.transform.position, Vector3.up, speed * Time.deltaTime / 6 / 3);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                // transform.Translate(Vector3.right * Time.deltaTime * speed);
                transform.RotateAround(targetPlanet.transform.position, Vector3.down, speed * Time.deltaTime / 6 / 3);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed / 3);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed / 3);
                // transform.RotateAround(targetPlanet.transform.position, Vector3.right, speed * Time.deltaTime / 6);
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ScreenCapture.CaptureScreenshot("test.png", 1);
        }
    }

    private void FocusPlanetShortKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            this.targetPlanet = GameObject.Find("Sun").GetComponent<PlanetBehaviour>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.targetPlanet = GameObject.Find("Mercury").GetComponent<PlanetBehaviour>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.targetPlanet = GameObject.Find("Venus").GetComponent<PlanetBehaviour>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            this.targetPlanet = GameObject.Find("Earth").GetComponent<PlanetBehaviour>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            this.targetPlanet = GameObject.Find("Mars").GetComponent<PlanetBehaviour>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            this.targetPlanet = GameObject.Find("Jupiter").GetComponent<PlanetBehaviour>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            this.targetPlanet = GameObject.Find("Saturn").GetComponent<PlanetBehaviour>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            this.targetPlanet = GameObject.Find("Uranus").GetComponent<PlanetBehaviour>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            this.targetPlanet = GameObject.Find("Neptune").GetComponent<PlanetBehaviour>();
        }
    }

    public void FlyToPlanet(string planetName)
    {
        // if (planetName == "Sun")
        // {
        //     cameraMoving = false;
        //     targetPos = new Vector3(0, 0, 0);
        //     transform.position = new Vector3(0, 520, -2);

        //     return;
        // }

        var planet = SetTarget(planetName);
        cameraMoving = true;

        var dist = planet.transform.localScale.x * 2;

        posTarget = (targetPlanet.transform.position - new Vector3(0, 0, 0)) * XX;
    }

    public PlanetBehaviour SetTarget(string planetName)
    {
        PlanetBehaviour[] allPlanets = FindObjectsOfType<PlanetBehaviour>();
        foreach (var planet in allPlanets)
        {
            if (planet.name == planetName)
            {
                targetPlanet = planet;
                return planet;
            }
        }
        return null;
    }
}
