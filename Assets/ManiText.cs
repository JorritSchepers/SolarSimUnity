using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManiText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int minSize = 80;
    private int maxSize = 100;
    private bool directionUp = true;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("ManiText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Increase the size of text untill it reached max, then decrease it untill it reached min
        if (directionUp)
        {
            text.fontSize += 0.05f;
            if (text.fontSize > maxSize)
            {
                directionUp = false;
            }
        }
        else
        {
            text.fontSize -= 0.04f;
            if (text.fontSize < minSize)
            {
                directionUp = true;
            }
        }
    }
}
