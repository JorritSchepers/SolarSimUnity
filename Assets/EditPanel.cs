using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditPanel : MonoBehaviour
{
    private PlanetBehaviour planet;
    private TextMeshProUGUI velocityText;
    private TextMeshProUGUI title;

    // Start is called before the first frame update
    void Start()
    {
        velocityText = transform.Find("VelocityValue").GetComponent<TextMeshProUGUI>();
        title = transform.Find("Title").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        planet = GameObject.Find(title.text).GetComponent<PlanetBehaviour>();
        velocityText.text = planet.GetVelocity() + " km/s";
    }
}
