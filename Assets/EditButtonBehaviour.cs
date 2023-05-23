using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EditButtonBehaviour : MonoBehaviour, IPointerExitHandler
{
    private string planetName;
    private Transform panel;

    // Start is called before the first frame update
    void Start()
    {
        // Init
        Button button = GetComponent<Button>();

        // Get panel in UICanvas called EditPanel
        panel = GameObject.Find("UICanvas").transform.Find("EditPanel");
        panel.gameObject.SetActive(false);

        // Get planet name from parent parent
        planetName = transform.parent.name;

        // Add onClick listener to the button
        button.onClick.AddListener(delegate () { this.EditPlanet(); });
    }

    public void EditPlanet()
    {
        var planet = GameObject.Find(planetName).GetComponent<PlanetBehaviour>();

        panel.Find("Title").GetComponent<TextMeshProUGUI>().text = planetName;
        panel.Find("MassValue").GetComponent<TextMeshProUGUI>().text = planet.GetMass() + " kg";
        panel.Find("SizeValue").GetComponent<TextMeshProUGUI>().text = planet.transform.localScale.x.ToString();
        panel.Find("ToggleOrbitPathButton")
            .GetComponentInChildren<TextMeshProUGUI>()
            .text = planet.GetOrbitPathVisibility() ? "O" : "Ø";
        panel.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        gameObject.SetActive(false);
    }
}
