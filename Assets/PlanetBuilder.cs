using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanetBuilder : MonoBehaviour
{
    public Material planetMaterial;
    public Transform panel;

    private UniverseSim universeSim;
    public int distance;
    public float mass;
    private GameObject planet;
    public PlanetBehaviour planetBehaviour;
    private GameObject arrow;

    private TextMeshProUGUI distanceText;
    private TextMeshProUGUI velocityText;
    private TextMeshProUGUI massText;
    public Vector3 vel;

    // Start is called before the first frame update
    void Start()
    {
        Button openButton = GameObject.Find("OpenPlanetBuilderButton").GetComponent<Button>();
        Button button = GameObject.Find("PlanetBuilderCreateButton").GetComponent<Button>();

        openButton.onClick.AddListener(delegate () { this.CreatePlanet(); });
        button.onClick.AddListener(delegate () { this.AddPlanet(); });

        panel = GameObject.Find("UICanvas").transform.Find("PlanetBuilderPanel");
        panel.gameObject.SetActive(false);

        distanceText = panel.transform.Find("PBDistanceValue").GetComponent<TextMeshProUGUI>();
        velocityText = panel.transform.Find("PBVelocityValue").GetComponent<TextMeshProUGUI>();
        massText = panel.transform.Find("PBMassValue").GetComponent<TextMeshProUGUI>();

        universeSim = GameObject.Find("UniverseSim").GetComponent<UniverseSim>();

        distance = Random.Range(50, 200);
        mass = 2e+20f;
        vel = new Vector3(0, 0, 5000000);

        // Add RemovePlanet to CloseButton
        panel.Find("CloseButton").GetComponent<Button>().onClick.AddListener(delegate () { this.RemovePlanet(); });
    }

    public void EditDistance(float value)
    {
        planet.transform.position = new Vector3(
            planet.transform.position.x + value,
            planet.transform.position.y,
            planet.transform.position.z
        );
        UpdateDistanceText(planet.transform.position.x * 1e+6f);
        UpdateVelocityArrow();
    }

    public void EditVelocity(float value)
    {
        planetBehaviour.velocity += new Vector3(0, 0, value);
        UpdateVelocityText(planetBehaviour.velocity.z / 180000);
        UpdateVelocityArrow();
    }

    public void EditMass(float value)
    {
        planetBehaviour.fMass += value;
        UpdateMassText(planetBehaviour.fMass);
        UpdateScale();
    }

    private void RemovePlanet()
    {
        Destroy(planet);
        Destroy(arrow);
    }

    public void CreatePlanet()
    {
        if (panel.gameObject.activeSelf)
        {
            panel.gameObject.SetActive(false);
            this.RemovePlanet();
            return;
        }

        planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        planet.transform.position = new Vector3(distance, 0, 0); ;
        planet.transform.localScale = new Vector3(7, 7, 7);

        planet.name = "Planet " + Random.Range(0, 1000);

        planet.GetComponent<Collider>().enabled = false;

        planet.AddComponent<LineRenderer>();
        planet.GetComponent<LineRenderer>().widthMultiplier = 0;

        planet.AddComponent<TrailRenderer>();

        planet.AddComponent<Rigidbody>();
        planet.GetComponent<Rigidbody>().useGravity = false;

        planet.AddComponent<PlanetBehaviour>();
        planetBehaviour = planet.GetComponent<PlanetBehaviour>();
        planetBehaviour.fMass = mass * Mathf.Sqrt(planetBehaviour.factor);
        planetBehaviour.fDistance = distance * planetBehaviour.factor;
        planetBehaviour.initVelocity = vel;
        planetBehaviour.velocity = vel;
        // planetBehaviour.CalculateTrailTime();

        UpdateDistanceText(planet.transform.position.x * 1e+6f);
        UpdateVelocityText(planetBehaviour.velocity.z / 180000);
        UpdateMassText(mass);

        CreateVelocityArrow();
        UpdateVelocityArrow();

        panel.gameObject.SetActive(true);

        distance = Random.Range(50, 200);
    }

    public void AddPlanet()
    {
        Destroy(arrow);
        universeSim.GetAllPlanets();
        panel.gameObject.SetActive(false);

        GameObject.Find("UICanvas").transform.Find("NavPanel").GetComponent<NavPanel>().AddPlanet(planet);
    }

    private void UpdateDistanceText(float dist)
    {
        distanceText.text = GetText(dist, 2) + " km";
    }

    private void UpdateVelocityText(float vel)
    {
        velocityText.text = Mathf.Round(vel) + " km/s";
    }

    private void UpdateMassText(float m)
    {
        massText.text = GetText(m, 2) + " kg";
    }

    private string GetText(float number, int decimals)
    {
        var s = number.ToString();

        if (s.Length < 2 + decimals + 4)
        {
            decimals = s.Length - 4 - 2;
        }

        var power = s.Substring(s.Length - 2, 2);

        return s.Substring(0, decimals + 2) + " e" + power;
    }

    private void CreateVelocityArrow()
    {
        arrow = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        arrow.GetComponent<Collider>().enabled = false;
        arrow.gameObject.name = "TEMP - VelocityArrow of " + planet.name;
        arrow.transform.localScale = new Vector3(3, planetBehaviour.velocity.z / 200000, 3);
        arrow.transform.position = new Vector3(
            planet.transform.position.x,
            planet.transform.position.y,
            planet.transform.position.z
        );
        arrow.transform.Rotate(90, 0, 0);
    }

    private void UpdateVelocityArrow()
    {
        arrow.transform.localScale = new Vector3(3, planetBehaviour.velocity.z / 200000, 3);
        arrow.transform.position = new Vector3(
            planetBehaviour.transform.position.x,
            0,
            arrow.transform.localScale.y
        );
    }

    private void UpdateScale()
    {
        var scale = 5 + (planetBehaviour.fMass / 2e+23f * 2);
        planet.transform.localScale = new Vector3(scale, scale, scale);
    }
}
