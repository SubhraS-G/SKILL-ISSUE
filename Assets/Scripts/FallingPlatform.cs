using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [Header("Settings")]
    public float shakeDelay = 0.5f;
    public float fallDelay = 0.3f;
    public float respawnTime = 3f;

    private Vector3 startPosition;
    private bool triggered = false;
    private SpriteRenderer sr;

    void Start()
    {
        startPosition = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")
            && !triggered)
        {
            triggered = true;
            StartCoroutine(ShakeAndFall());
        }
    }

    IEnumerator ShakeAndFall()
    {
        // Shake
        float elapsed = 0f;
        while (elapsed < shakeDelay)
        {
            transform.position = startPosition +
                new Vector3(
                    Random.Range(-0.08f, 0.08f),
                    0, 0);
            elapsed += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        transform.position = startPosition;

        // Flash red warning
        if (sr != null)
            sr.color = Color.red;

        yield return new WaitForSeconds(fallDelay);

        // Disable collider so player falls through
        GetComponent<Collider2D>().enabled = false;

        // Move platform down fast
        float fallSpeed = 0f;
        while (transform.position.y > startPosition.y - 10f)
        {
            fallSpeed += 0.5f;
            transform.position -= new Vector3(0, fallSpeed * Time.deltaTime, 0);
            yield return null;
        }

        // Respawn
        yield return new WaitForSeconds(respawnTime);
        transform.position = startPosition;
        GetComponent<Collider2D>().enabled = true;
        if (sr != null) sr.color = Color.white;
        triggered = false;
    }
}