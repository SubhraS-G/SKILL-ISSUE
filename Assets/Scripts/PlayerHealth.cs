using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")]
    public float fallDeathY = -10f;
    public float respawnDelay = 1f;

    [Header("Respawn Point")]
    public Transform respawnPoint;

    private bool isDead = false;
    private SpriteRenderer sr;
    private PlayerController pc;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        pc = GetComponent<PlayerController>();
    }

    void Update()
    {
        // Fall death
        if (transform.position.y < fallDeathY && !isDead)
            StartCoroutine(Die());
    }

    public void KillPlayer()
    {
        if (!isDead)
            StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        Debug.Log("Die() called!");
        isDead = true;

        // Find DeathManager directly — guaranteed to work
        DeathManager dm = FindAnyObjectByType<DeathManager>();
        if (DeathManager.instance != null)
        {
            DeathManager.instance.AddDeath();
        }
        else
        {
            Debug.Log("Cant find DeathManager!");
        }

        // Show troll message
        if (TrollMessageSystem.instance != null)
            TrollMessageSystem.instance.ShowDeathMessage();

        // Disable movement immediately
        pc.enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        // Instant red flash
        sr.color = Color.red;

        yield return new WaitForSeconds(0.15f);

        sr.color = Color.white;

        // Respawn
        if (respawnPoint != null)
            transform.position = respawnPoint.position;
        else
            transform.position = new Vector3(0, 2, 0);

        pc.enabled = true;
        isDead = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trap"))
            KillPlayer();
    }
}