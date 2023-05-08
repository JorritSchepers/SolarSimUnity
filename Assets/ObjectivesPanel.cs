using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectivesPanel : MonoBehaviour
{
    private List<Objective> objectives = new List<Objective>();
    private Button nextButton;
    private Button prevButton;
    private int currentObjective = 0;
    private Button completeButton;

    // Start is called before the first frame update
    void Start()
    {
        var openButton = GameObject.Find("OpenObjectivesPanel").GetComponent<Button>();
        openButton.onClick.AddListener(delegate () { gameObject.SetActive(true); });

        nextButton = transform.Find("NextButton").GetComponent<Button>();
        nextButton.onClick.AddListener(delegate () { NextObjective(); });

        prevButton = transform.Find("PrevButton").GetComponent<Button>();
        prevButton.onClick.AddListener(delegate () { PrevObjective(); });
        prevButton.gameObject.SetActive(false);

        completeButton = transform.Find("CompleteButton").GetComponent<Button>();
        completeButton.onClick.AddListener(delegate () { CompleteCurrentObjective(completeButton); });

        gameObject.SetActive(false);

        initObjectives();

        transform.Find("ObjectiveText").GetComponent<TextMeshProUGUI>().text = objectives[0].text;
    }

    private void initObjectives() 
    {
        // Create a list of objectives
        // AddObjective("Double the size of Mars");  
        // AddObjective("Create a planet with a mass of 2.5e+23 kg");   
        AddObjective("Create a planet with enough mass to make the sun orbit the planet.");
        AddObjective("Create a planet with an orbital period of around 200 days.");
        AddObjective("Make an collision happen between 2 or more planets.");
    }

    private void AddObjective(string text)
    {
        objectives.Add(new Objective(text));
    }

    public void NextObjective() 
    {
        if (currentObjective == objectives.Count - 1)
        {
            return;
        }
        currentObjective++;
        transform.Find("ObjectiveText").GetComponent<TextMeshProUGUI>().text = objectives[currentObjective].text;

        if (currentObjective == objectives.Count -1)
        {
            nextButton.gameObject.SetActive(false);
        }

        prevButton.gameObject.SetActive(true);

        if (objectives[currentObjective].completed)
        {
            completeButton.gameObject.SetActive(false);
        }
        else 
        {
            completeButton.gameObject.SetActive(true);
        }
    }

    public void PrevObjective() 
    {
        if (currentObjective == 0)
        {
            return;
        }
        currentObjective--;
        transform.Find("ObjectiveText").GetComponent<TextMeshProUGUI>().text = objectives[currentObjective].text;

        // If we returned to the first objective, hide button
        if (currentObjective == 0)
        {
            prevButton.gameObject.SetActive(false);
        }

        nextButton.gameObject.SetActive(true);

        if (objectives[currentObjective].completed)
        {
            completeButton.gameObject.SetActive(false);
        }
        else 
        {
            completeButton.gameObject.SetActive(true);
        }
    }

    private void CompleteCurrentObjective(Button button) 
    {
        objectives[currentObjective].Complete();
        transform.Find("ObjectiveText").GetComponent<TextMeshProUGUI>().text = objectives[currentObjective].text;
        button.gameObject.SetActive(false);
    }
}

public class Objective
{
    public string text;
    public bool completed = false;

    public Objective(string text)
    {
        this.text = text;
    }

    public void Complete()
    {
        if (completed) 
        {
            return;
        }
        completed = true;
        text = "Completed: " + text;
    }
}
