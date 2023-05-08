using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class NavButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CameraController cameraController;
    private string planetName;
    private EditButtonBehaviour editButtonBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        // Init
        Button button = GetComponent<Button>();
        cameraController = FindObjectOfType<CameraController>();
        editButtonBehaviour = transform.parent.GetComponentInChildren<EditButtonBehaviour>();

        // Hide Edit button after retrieving object
        editButtonBehaviour.gameObject.SetActive(false);

        // Get planet name from parent parent
        planetName = transform.parent.name; 

        // Add onClick listener to the button
        button.onClick.AddListener(delegate () { this.SetCameraTarget(); });

        // Set the text of the button to the name of the planet
        // button.GetComponentInChildren<TextMeshProUGUI>().text = planetName;
    }

    public void SetCameraTarget()
    {
        cameraController.FlyToPlanet(planetName);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        editButtonBehaviour.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Vector2 mousePos = Input.mousePosition;
        if (!RectTransformUtility.RectangleContainsScreenPoint(editButtonBehaviour.GetComponent<RectTransform>(), mousePos)) {
            editButtonBehaviour.gameObject.SetActive(false);
        }
    }
}
