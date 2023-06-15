using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UniverseSim : MonoBehaviour
{
    public PlanetBehaviour[] allPlanets;

    private GameObject objectivesButton;

    private GameObject navPanel;

    private Scene scene;

    private GameObject devTools;
    private PlanetBuilder pb;
    private GameObject maniText;

    // Start is called before the first frame update
    void Start()
    {
        GetAllPlanets();
        objectivesButton = GameObject.Find("OpenObjectivesPanel");
        navPanel = GameObject.Find("NavPanel");
        GameObject.Find("EditPanel").SetActive(false);
        scene = SceneManager.GetActiveScene();
        devTools = GameObject.Find("DevToolsText");
        pb = GameObject.Find("PlanetBuilderPanel").GetComponent<PlanetBuilder>();
        maniText = GameObject.Find("ManiText");
        maniText.SetActive(false);

        // GameObject.Find("GoogleFormsButton").GetComponent<Button>().onClick.AddListener(delegate () { Application.OpenURL("https://forms.gle/b8Xw7XaDapb8g6bV7"); });

        GameObject.Find("OpenNavButton").GetComponent<Button>().onClick.AddListener(delegate ()
        {
            OpenNavPanel();
        });
    }

    // Update is called once per frame
    void Update()
    {
        MovePlanets();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(scene.buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            devTools.SetActive(!devTools.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            pb.distance = Random.Range(50, 400);
            pb.mass = Random.Range(2e+18f, 2e+20f);
            var x = Random.Range(2000000, 5000000);
            pb.vel = new Vector3(0, 0, x);

            pb.CreatePlanet();
            pb.AddPlanet();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            maniText.SetActive(!maniText.activeSelf);
        }
    }

    void OpenNavPanel()
    {
        navPanel.SetActive(!navPanel.activeSelf);
    }

    void MovePlanets()
    {
        foreach (var planet in allPlanets)
        {
            planet.CalculateVelocity(allPlanets);
        }

        foreach (var planet in allPlanets)
        {
            planet.MovePlanet();
        }
    }

    public void GetAllPlanets()
    {
        allPlanets = FindObjectsOfType<PlanetBehaviour>();
    }

    public void RemovePlanetFromList(string planetName)
    {
        var newList = new PlanetBehaviour[allPlanets.Length - 1];

        var j = 0;

        for (int i = 0; i < allPlanets.Length; i++)
        {
            if (allPlanets[i].name != planetName)
            {
                newList[j] = allPlanets[i];
                j++;
            }
        }

        allPlanets = newList;
    }
}
