using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeInText : MonoBehaviour
{
    public bool start = false;

    private bool setTime = false;
    private TextMeshProUGUI text;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Fade out after 10 seconds
        if (start)
        {
            if (!setTime)
            {
                time = Time.time;
                setTime = true;
            }
            if (Time.time > time + 7 && start)
            {
                text.CrossFadeAlpha(0, .3f, false);
            }
        }
    }
}
