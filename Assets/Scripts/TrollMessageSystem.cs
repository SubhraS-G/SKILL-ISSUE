using UnityEngine;
using TMPro;
using System.Collections;

public class TrollMessageSystem : MonoBehaviour
{
    public static TrollMessageSystem instance;

    [Header("UI")]
    public TextMeshProUGUI messageText;

    [Header("Messages")]
    public string[] deathMessages = {
        "SKILL ISSUE",
        "Nice try!",
        "Almost! ...not really",
        "Try harder!",
        "LOL",
        "That was embarrassing",
        "My grandma plays better",
        "Have you tried not dying?",
        "F in the chat",
        "Just walk. How hard can it be?"
    };

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        if (messageText != null)
            messageText.text = "";
    }

    public void ShowDeathMessage()
    {
        string msg = deathMessages[
            Random.Range(0, deathMessages.Length)];
        StartCoroutine(ShowMessage(msg));
    }

    public void ShowCustomMessage(string msg)
    {
        StartCoroutine(ShowMessage(msg));
    }

    IEnumerator ShowMessage(string msg)
    {
        if (messageText == null) yield break;

        messageText.text = msg;

        // Punch scale effect
        float elapsed = 0f;
        while (elapsed < 0.2f)
        {
            float scale = Mathf.Lerp(1.5f, 1f, elapsed / 0.2f);
            messageText.transform.localScale =
                Vector3.one * scale;
            elapsed += Time.deltaTime;
            yield return null;
        }

        messageText.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(2f);

        // Fade out
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * 2f;
            messageText.color = new Color(
                messageText.color.r,
                messageText.color.g,
                messageText.color.b,
                alpha
            );
            yield return null;
        }

        messageText.text = "";
        messageText.color = new Color(
            messageText.color.r,
            messageText.color.g,
            messageText.color.b,
            1f
        );
    }
}