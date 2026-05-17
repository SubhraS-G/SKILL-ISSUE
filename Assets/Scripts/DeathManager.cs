using UnityEngine;
using TMPro;

public class DeathManager : MonoBehaviour
{
    public static DeathManager instance;

    [Header("UI")]
    public TextMeshProUGUI deathCountText;

    private int currentRunDeaths = 0;

    void Awake()
    {
        instance = this;
        
    }

    public void AddDeath()
    {
        
        currentRunDeaths++;

        int totalDeaths = PlayerPrefs.GetInt("TotalDeaths", 0);
        totalDeaths++;
        PlayerPrefs.SetInt("TotalDeaths", totalDeaths);

        int bestRun = PlayerPrefs.GetInt("BestRun", 999);
        if (currentRunDeaths < bestRun)
            PlayerPrefs.SetInt("BestRun", currentRunDeaths);

        PlayerPrefs.Save();
        UpdateUI();
    }

    public int GetCurrentDeaths()
    {
        return currentRunDeaths;
    }

    public void ResetCurrentRun()
    {
        currentRunDeaths = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (deathCountText != null)
            deathCountText.text = "Deaths: " + currentRunDeaths;
    }
}