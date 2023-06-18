using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanetBehaviour : MonoBehaviour
{
    public UniverseSim universeSim;
    public GameObject navPanel;

    private float prevVel = 0f;
    private bool x = false;

    public float currentVel = 0f;
    public int yearCount = 0;
    public int actYearCount = 0;

    // Not used
    public float fDistance = 1f;

    public float fMass = 1f;
    public float inclination = 0f;

    public Rigidbody rb;
    public int mass;
    public Vector3 initVelocity;
    public Vector3 velocity;

    private int initYVelocity;

    public float initTimeStep = 0.00000002f;
    public float timeStep = 0.00000002f;
    public int factor = 1000000000;

    float initTime = 2;

    PlanetBehaviour[] allBodies;

    LineRenderer lineRenderer;

    public TrailRenderer trailRenderer;

    int i;
    int j;

    int fps = 120;
    float fpsFactor = 3.3f;

    // Start is called before the first frame update
    void Start()
    {
        initTimeStep = Time.fixedDeltaTime / 20000000 * 10 * 4;

        // initTimeStep = 0;

        timeStep = initTimeStep;

        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();

        var y = inclination / 90 * transform.position.x;
        y = 0;

        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        fDistance = transform.position.x;
        rb.mass = mass;
        velocity = initVelocity;
        allBodies = FindObjectsOfType<PlanetBehaviour>();

        lineRenderer.positionCount = 0;

        initTime = 2 / ((initVelocity.z / 8500000) * (initVelocity.z / 8500000) * (initVelocity.z / 8500000)); ;

        universeSim = FindObjectOfType<UniverseSim>();
        navPanel = GameObject.Find("NavPanel");

        // fDistance /= factor;
        fMass = fMass / Mathf.Sqrt(factor);

        lineRenderer.widthMultiplier = 0;
        CalculateTrailTime();

        UpdateTimeStepText();
    }

    // Update is called once per frame
    void Update()
    {
        fps = universeSim.GetComponent<FPSCounter>()._currentAveraged;
        if (fps > 0)
        {
            fpsFactor = 400 / fps;
        }

        DrawFlightLine();

        prevVel = currentVel;
        currentVel = velocity.sqrMagnitude;

        if (x)
        {
            if (currentVel < prevVel)
            {
                yearCount++;
                x = !x;
            }
        }
        else
        {
            if (currentVel > prevVel)
            {
                yearCount++;
                x = !x;
            }
        }

        actYearCount = yearCount / 2;

        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleOrbitPath();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggTrailRenderer();
        }
    }

    public void CalculateTrailTime()
    {
        var x = 6000000; // 6500000

        if (trailRenderer == null)
        {
            trailRenderer = GetComponent<TrailRenderer>();
        }

        trailRenderer.time = 2 / ((initVelocity.z / x) * (initVelocity.z / x) * (initVelocity.z / x)) * fpsFactor;
    }

    void DrawFlightLine()
    {
        if (i % 10 == 0)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(j, transform.position);
            j++;
        }
        i++;
    }

    public void MovePlanet()
    {
        if (rb != null)
        {
            rb.MovePosition(rb.position + velocity * timeStep);
        }
    }

    public void CalculateVelocity(PlanetBehaviour[] allBodies)
    {
        foreach (var otherBody in allBodies)
        {
            if (otherBody == this)
            {
                continue;
            }

            float g = 0.000000000066743f;
            float r = (otherBody.rb.position - rb.position).sqrMagnitude;

            Vector3 forceDir = (otherBody.rb.position - rb.position).normalized;
            Vector3 acceleration = forceDir * g * otherBody.fMass / r;

            velocity += acceleration * timeStep;
        }
    }

    public string GetMass()
    {
        var s = fMass.ToString();
        var power = s.Substring(s.Length - 2, 2);

        return s.Substring(0, 4) + " e" + power;
    }

    public void SetTimeStep(float value)
    {
        timeStep = value;
        trailRenderer.time = initTime / (timeStep / initTimeStep);
        UpdateTimeStepText();
    }

    private void UpdateTimeStepText()
    {
        var timeStepText = GameObject.Find("TimeStepValue");
        var roundedTimeStep = Mathf.Round(timeStep * 1000000000 / fpsFactor);

        timeStepText.GetComponent<TextMeshProUGUI>().text = roundedTimeStep + " d/s";
    }

    public void ToggleOrbitPath()
    {
        if (lineRenderer.widthMultiplier == 0)
        {
            lineRenderer.widthMultiplier = 1f;
        }
        else
        {
            lineRenderer.widthMultiplier = 0;
        }
    }

    public void ToggTrailRenderer()
    {
        if (trailRenderer.widthMultiplier == 0)
        {
            trailRenderer.widthMultiplier = 1f;
        }
        else
        {
            trailRenderer.widthMultiplier = 0;
        }
    }

    public bool GetOrbitPathVisibility()
    {
        return lineRenderer.widthMultiplier > 0;
    }

    public void Reset()
    {
        transform.position = new Vector3(fDistance, 0, 0);
        velocity = initVelocity;
        i = 0;
        j = 0;
        lineRenderer.positionCount = 0;
        SetTimeStep(initTimeStep);
        trailRenderer.Clear();
    }

    public void Kill()
    {
        universeSim.RemovePlanetFromList(gameObject.name);
        navPanel.GetComponent<NavPanel>().RemovePlanet(gameObject.name);
        Destroy(gameObject);
    }

    public string GetVelocity()
    {
        // return "" + Mathf.Round(velocity.z / 18000) / 10;

        var x = Mathf.Abs(velocity.x * velocity.x);
        var y = Mathf.Abs(velocity.y * velocity.y);
        var z = Mathf.Abs(velocity.z * velocity.z);

        // var xy = Mathf.Sqrt(x + y);
        // var xyz = Mathf.Sqrt(xy + z);
        var xz = Mathf.Sqrt(x + z);

        return "" + Mathf.Round(xz / 180000);
    }
}
