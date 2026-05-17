using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI deathCountText;
    public TextMeshProUGUI bestDeathText;

    void Start()
    {
        // Load death count from PlayerPrefs
        int totalDeaths = PlayerPrefs.GetInt("TotalDeaths", 0);
        int bestRun = PlayerPrefs.GetInt("BestRun", 0);

        if (deathCountText != null)
            deathCountText.text =
                "Total Deaths: " + totalDeaths;

        if (bestDeathText != null)
            bestDeathText.text =
                "Best Run Deaths: " + bestRun;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetStats()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        if (deathCountText != null)
            deathCountText.text = "Total Deaths: 0";
        if (bestDeathText != null)
            bestDeathText.text = "Best Run Deaths: 0";
    }
}