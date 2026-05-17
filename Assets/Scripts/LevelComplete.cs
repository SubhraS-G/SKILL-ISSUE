using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelComplete : MonoBehaviour
{
    public static LevelComplete instance;

    [Header("UI Panel")]
    public GameObject levelCompletePanel;

    [Header("Text")]
    public TextMeshProUGUI deathCountText;
    public TextMeshProUGUI messageText;

    private string[] completionMessages = {
        "You actually did it. Impressive.",
        "Took you long enough!",
        "Even a broken clock is right twice a day.",
        "Congratulations... I guess.",
        "We're as surprised as you are.",
    };

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);
    }

    public void ShowLevelComplete()
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);

        // Show death count
        int deaths = DeathManager.instance != null ?
            DeathManager.instance.GetCurrentDeaths() : 0;

        if (deathCountText != null)
            deathCountText.text =
                "You died " + deaths + " times.\nSkill issue. 😄";

        // Random completion message
        if (messageText != null)
            messageText.text = completionMessages[
                Random.Range(0, completionMessages.Length)];

        // Pause game
        Time.timeScale = 0f;
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        // For now restart — add more levels later
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}