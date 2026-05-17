using UnityEngine;
using System.Collections;

public class FakeFinishLine : MonoBehaviour
{
    [Header("Settings")]
    public float moveDistance = 5f;
    public float moveSpeed = 10f;
    public int fakesBeforeReal = 3;
    public string[] trollMessages = {
        "SO CLOSE! 😂",
        "Almost there! ...not really 😄",
        "Keep trying! 💀",
        "YOU WISH! 😂"
    };

    private int touchCount = 0;
    private bool isMoving = false;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            touchCount++;

            if (touchCount >= fakesBeforeReal)
            {
                // Real finish!
                StartCoroutine(RealFinish());
            }
            else
            {
                // Fake — run away! 😈
                StartCoroutine(RunAway());
            }
        }
    }

    IEnumerator RunAway()
    {
        isMoving = true;

        // Show troll message
        if (TrollMessageSystem.instance != null)
            TrollMessageSystem.instance.ShowCustomMessage(
                trollMessages[
                    Random.Range(0, trollMessages.Length)]);

        // Move away from player
        Vector3 targetPos = transform.position +
            new Vector3(moveDistance, 0, 0);

        while (Vector3.Distance(
            transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        isMoving = false;
    }

    IEnumerator RealFinish()
    {
        isMoving = true;

        if (TrollMessageSystem.instance != null)
            TrollMessageSystem.instance
                .ShowCustomMessage("YOU ACTUALLY DID IT!! GG!");

        yield return new WaitForSeconds(1.5f);

        // Show level complete screen
        if (LevelComplete.instance != null)
            LevelComplete.instance.ShowLevelComplete();
    }
}