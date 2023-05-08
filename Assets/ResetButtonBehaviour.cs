using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButtonBehaviour : MonoBehaviour
{
    public string type;

    private UniverseSim universeSim;

    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        if (type == "Reset")
        {
            button.onClick.AddListener(delegate () { this.Reset(); });
        }
        else if (type == "Remove")
        {
            button.onClick.AddListener(delegate () { this.RemoveAll(); });
        }

        universeSim = GameObject.Find("UniverseSim").GetComponent<UniverseSim>();
    }

    private void Reset()
    {
        foreach (PlanetBehaviour planet in universeSim.allPlanets)
        {
            if (planet != null) 
            {
                planet.Reset();
            }
        }
    }

    private void RemoveAll()
    {
        var oldList = universeSim.allPlanets;

        var newList = new PlanetBehaviour[1];
        newList[0] = universeSim.allPlanets[0];
        universeSim.allPlanets = newList;

        // Remove from navlist
        var navList = GameObject.Find("UICanvas").transform.Find("NavPanel").GetComponent<NavPanel>();
        navList.ClearList();

        foreach (PlanetBehaviour planet in oldList)
        {
            if (planet.name != "Sun")
            {
                Destroy(planet.gameObject);
            }
            else 
            {
                planet.transform.position = new Vector3(0, 0, 0);
                planet.velocity = new Vector3(0, 0, 0);
                planet.trailRenderer.Clear();
                navList.AddPlanet(planet.gameObject);
            }
        }
    }
}
