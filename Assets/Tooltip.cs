using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string text;

    private GameObject tooltip;
    private GameObject button;

    private float x;

    private bool start = false;

    void Start()
    {
        tooltip = GameObject.Find("Tooltip");
        button = gameObject;
    }

    void Update()
    {
        if (!start)
        {
            return;
        }

        if (Time.time > x + .5f)
        {
            showTooltip();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        start = true;
        x = Time.time;
    }

    private void showTooltip()
    {
        tooltip.transform.position = button.transform.position;
        tooltip.transform.position += new Vector3(0, Screen.height / 5, 0);
        tooltip.GetComponentInChildren<TextMeshProUGUI>().text = text;
        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        start = false;
        tooltip.SetActive(false);
    }
}
