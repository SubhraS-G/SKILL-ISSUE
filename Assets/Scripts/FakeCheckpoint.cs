using UnityEngine;
using System.Collections;

public class FakeCheckpoint : MonoBehaviour
{
    [Header("Settings")]
    public string trollMessage = "Checkpoint? LOL 😂 NOPE!";
    public float killDelay = 0.5f;

    private bool triggered = false;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            StartCoroutine(TrollPlayer(other.gameObject));
        }
    }

    IEnumerator TrollPlayer(GameObject player)
    {
        // Flash green — fake "saved" feeling
        if (sr != null) sr.color = Color.green;

        // Show fake save message first
        if (TrollMessageSystem.instance != null)
            TrollMessageSystem.instance
                .ShowCustomMessage("Checkpoint saved! ✅");

        // Wait — let them feel safe
        yield return new WaitForSeconds(killDelay);

        // Then kill them 😈
        if (TrollMessageSystem.instance != null)
            TrollMessageSystem.instance
                .ShowCustomMessage(trollMessage);

        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null) ph.KillPlayer();

        // Reset
        yield return new WaitForSeconds(2f);
        if (sr != null) sr.color = Color.white;
        triggered = false;
    }
}