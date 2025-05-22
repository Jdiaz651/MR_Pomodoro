using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskButton : MonoBehaviour
{
    public TextMeshProUGUI label;
    public int points;
    private bool completed = false;
    private System.Action<int> onClickCallback;

    public void Initialize(string taskName, int score, System.Action<int> onClick)
    {
        points = score;
        label.text = $"{taskName} +{score}";
        completed = false;
        onClickCallback = onClick;

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (completed) return;

        completed = true;

        // Strike through the label
        label.text = $"<s>{label.text}</s>";

        // Give points once
        onClickCallback?.Invoke(points);
    }
}
