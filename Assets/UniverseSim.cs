using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniverseSim : MonoBehaviour
{
    public PlanetBehaviour[] allPlanets;

    private GameObject objectivesButton;

    private GameObject navPanel;

    // Start is called before the first frame update
    void Start()
    {
        GetAllPlanets();
        objectivesButton = GameObject.Find("OpenObjectivesPanel");
        navPanel = GameObject.Find("NavPanel");
        GameObject.Find("EditPanel").SetActive(false);

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

        if (Input.GetKeyDown(KeyCode.O))
        {
            GameObject.Find("UICanvas").SetActive(false);
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
