using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public GameObject buttonPrefab; // Assign TaskButton prefab here
    public Transform buttonContainer; // A vertical layout group container
    public TextMeshProUGUI scoreText;

    private int score = 0;

    void Start()
    {
        dropdown.onValueChanged.RemoveAllListeners(); // prevent duplicate listeners
        dropdown.ClearOptions(); // clear old options

        // Add options (placeholder + tasks)
        dropdown.AddOptions(new List<string>
        {
            "Add Task",      // index 0 - placeholder
            "Commit Code",   // index 1
            "Push Code",     // index 2
            "Merge Code"     // index 3
        });

        dropdown.onValueChanged.AddListener(OnDropdownChanged); // safe to add now
    }


    void OnDropdownChanged(int index)
    {
        if (index == 0)
            return; // Ignore the placeholder

        string label = dropdown.options[index].text;
        int points = GetPointsForLabel(label);
        CreateTaskButton(label, points);

        dropdown.value = 0; // Reset to "Add Task"
    }


    void CreateTaskButton(string label, int points)
    {
        GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
        TaskButton taskBtn = newButton.GetComponent<TaskButton>();
        taskBtn.Initialize(label, points, AddScore);
    }

    void AddScore(int amount)
    {
        score += amount;
        scoreText.text = $"Score: {score}";
    }

    int GetPointsForLabel(string label)
    {
        switch (label)
        {
            case "Commit Code": return 10;
            case "Push Code": return 25;
            case "Merge Code": return 50;
            default: return 0;
        }
    }
}
