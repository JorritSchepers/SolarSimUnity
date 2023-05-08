using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartPanel : MonoBehaviour
{
    public GameObject wasdText;

    // Start is called before the first frame update
    void Start()
    {
        var startGameButton = GameObject.Find("StartButton").GetComponent<Button>();
        startGameButton.onClick.AddListener(delegate () { StartGame(); });
    }

    private void StartGame()
    {
        gameObject.SetActive(false);
        wasdText.GetComponent<FadeInText>().start = true;
    }
}
