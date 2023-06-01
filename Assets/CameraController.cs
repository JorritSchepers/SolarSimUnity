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

    // private int cameraFlightSpeed = 250;
    // private int targetFlightSpeed = 350;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        universeSim = FindObjectOfType<UniverseSim>();
        SetTarget("Sun");

        spaceshipBehaviour = FindObjectOfType<SpaceshipBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (cameraMoving)
        // {
        //     targetPos = Vector3.MoveTowards(targetPos, targetPlanet.transform.position, targetFlightSpeed * Time.deltaTime);
        //     // if (moveCameraPosition) 
        //     // {
        //     //     transform.position = Vector3.MoveTowards(transform.position, posTarget, cameraFlightSpeed * Time.deltaTime);
        //     // }
        //     posTarget = (targetPlanet.transform.position - new Vector3(0, 0, 0)) * XX;
        // }

        // If I press H, disable the UICanvas
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H pressed");
            GameObject.Find("UICanvas").SetActive(false);
        }

        if (targetPlanet.name == "Sun")
        {
            targetPos = new Vector3(0, 0, 0);
        }
        else
        {
            targetPos = targetPlanet.transform.position;
        }

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
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
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
