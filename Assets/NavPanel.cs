using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using TMPro;

public class NavPanel : MonoBehaviour
{
    public GameObject addPlanetButton;

    private int startY;
    private int currentY;

    // Start is called before the first frame updatex
    void Start()
    {
        startY = (int) (GetComponent<RectTransform>().rect.height / 2) - 40;
        currentY = startY;
        initStarterPlanets();
    }

    public void AddPlanet(GameObject planet)
    {
        // Create button
        GameObject newButton = Instantiate(addPlanetButton, transform);
        newButton.transform.localPosition = new Vector3(0, currentY, 0);
        newButton.gameObject.name = planet.name;
        currentY -= 60;

        // Set planet name
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = planet.name;
    }

    private void initStarterPlanets()
    {
        AddPlanet(GameObject.Find("Sun"));
        AddPlanet(GameObject.Find("Mercury"));
        AddPlanet(GameObject.Find("Venus"));
        AddPlanet(GameObject.Find("Earth"));
        AddPlanet(GameObject.Find("Mars"));
        AddPlanet(GameObject.Find("Jupiter"));
        AddPlanet(GameObject.Find("Saturn"));
        AddPlanet(GameObject.Find("Uranus"));
        AddPlanet(GameObject.Find("Neptune"));
    }

    public void ClearList()
    {
        currentY = startY;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void RemovePlanet(string planetName)
    {
        var x = 0f;

        // Remove the button
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == planetName)
            {
                x = child.localPosition.y;
                Destroy(child.gameObject);
            }
        }

        // Move all buttons below it up
        foreach (Transform child in transform)
        {
            if (child.localPosition.y < x)
            {
                child.localPosition = new Vector3(0, child.localPosition.y + 60, 0);
            }
        }

        currentY += 60;
    }
}
