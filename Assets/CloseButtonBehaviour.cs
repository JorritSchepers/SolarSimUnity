using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();

        button.onClick.AddListener(delegate () { this.ClosePanel(); });
    }

    public void ClosePanel()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
