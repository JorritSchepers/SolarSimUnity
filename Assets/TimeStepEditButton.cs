using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeStepEditButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float value;

    private Button button;
    private UniverseSim universeSim;
    private bool buttonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        universeSim = GameObject.Find("UniverseSim").GetComponent<UniverseSim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed)
        {
            var x = value;

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                x *= 10;
            }

            foreach (var planet in universeSim.allPlanets)
            {
                var newTimeStep = planet.timeStep += x;

                if (newTimeStep < 0)
                {
                    newTimeStep = 0;
                }

                planet.SetTimeStep(newTimeStep);
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
