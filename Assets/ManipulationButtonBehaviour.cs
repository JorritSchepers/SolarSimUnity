using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManipulationButtonBehaviour : MonoBehaviour
{
    private string stat;
    public int percentage = 0;
    private TextMeshProUGUI text;
    private Transform panel;

    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        panel = GameObject.Find("UICanvas").transform.Find("EditPanel");

        stat = transform.parent.name;

        button.onClick.AddListener(delegate () { this.Manipulate(stat, percentage); });
    }


    public void Manipulate(string stat, int percentage)
    {
        var planetName = GameObject.Find("Title").GetComponent<TextMeshProUGUI>().text;
        var planet = GameObject.Find(planetName).GetComponent<PlanetBehaviour>();

        switch (stat)
        {
            case "Mass":
                ManipulateMass(planet, percentage);
                break;
            case "Size":
                ManipulateSize(planet, percentage);
                break;
        }
    }

    public void ManipulateMass(PlanetBehaviour planet, int percentage)
    {
        var factor = GetFactor(percentage);

        planet.fMass *= factor;

        panel.Find("MassValue").GetComponent<TextMeshProUGUI>().text = planet.GetMass();
    }

    public void ManipulateSize(PlanetBehaviour planet, int percentage)
    {
        var factor = GetFactor(percentage);

        planet.transform.localScale = new Vector3(
            planet.transform.localScale.x * factor,
            planet.transform.localScale.y * factor,
            planet.transform.localScale.z * factor
        );

        panel.Find("SizeValue").GetComponent<TextMeshProUGUI>().text = planet.transform.localScale.x.ToString();
    }

    private float GetFactor(int percentage)
    {
        var factor = percentage / 100f;

        if (factor < 0)
        {
            return 1 / (1 - factor);
        }
        else
        {
            return 1 + factor;
        }
    }
}
