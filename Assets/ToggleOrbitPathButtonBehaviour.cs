using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleOrbitPathButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(delegate () { this.ToggleOrbitPath(); });
    }

    public void ToggleOrbitPath()
    {
        var planetName = GameObject.Find("Title").GetComponent<TextMeshProUGUI>().text;
        var planet = GameObject.Find(planetName).GetComponent<PlanetBehaviour>();
        planet.ToggleOrbitPath();
        GetComponentInChildren<TextMeshProUGUI>().text = planet.GetOrbitPathVisibility() ? "Hide Orbit Path" : "Show Orbit Path";
    }
}
