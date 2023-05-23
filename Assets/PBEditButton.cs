using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PBEditButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string stat;
    public float value;

    private float factor = 2f;

    private Button button;
    private PlanetBuilder planetBuilder;
    private bool buttonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        planetBuilder = transform.parent.GetComponent<PlanetBuilder>();
        GetComponentInChildren<TextMeshProUGUI>().text = value > 0 ? "+" : "-";
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed)
        {
            if (stat == "Distance")
            {
                planetBuilder.EditDistance(value * factor);
            }
            else if (stat == "Velocity")
            {
                planetBuilder.EditVelocity(value * factor);
            }
            else if (stat == "Mass")
            {
                var x = 1;
                if (value < 0)
                {
                    x = -1;
                }
                planetBuilder.EditMass(planetBuilder.planetBehaviour.fMass * 0.005f * x * factor);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }
}
