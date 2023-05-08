using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditRemovePlanetButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(delegate () { this.RemovePlanet(); });
    }

    private void RemovePlanet() 
    {
        var planetName = GameObject.Find("Title").GetComponent<TextMeshProUGUI>().text;
        var planet = GameObject.Find(planetName).GetComponent<PlanetBehaviour>();
        
        GameObject.Find("UICanvas").transform.Find("EditPanel").gameObject.SetActive(false);
        
        planet.Kill();
    }
}
