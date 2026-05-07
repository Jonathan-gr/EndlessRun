using System.Collections;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Animator animator;

    [Header("Lane Settings")]
    [SerializeField] private float laneDistance = 0.5f; // distance between lanes
    [SerializeField] private float laneChangeSpeed = 10f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 0.5f;

    private bool isJumping = false;
    private int currentLane = 0; // -1 = left, 0 = center, 1 = right
    private float targetX;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetX = transform.position.x;
    }

    void Update()
    {
        // INPUT
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                StartCoroutine(JumpRoutine());
            }
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(-1);
            animator.SetTrigger("RunLeft");
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(1);
            animator.SetTrigger("RunRight");
        }

        // SMOOTH MOVEMENT
        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, targetX, laneChangeSpeed * Time.deltaTime);
        transform.position = pos;
    }

    void MoveLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
        targetX = currentLane * laneDistance;
    }
    IEnumerator JumpRoutine()
    {
        isJumping = true;

        animator.SetTrigger("Jump");

        float startY = transform.position.y;
        float elapsed = 0f;

        while (elapsed < jumpDuration)
        {
            elapsed += Time.deltaTime;

            // Creates smooth up/down arc
            float normalizedTime = elapsed / jumpDuration;
            float height = Mathf.Sin(normalizedTime * Mathf.PI) * jumpHeight;

            Vector3 pos = transform.position;
            pos.y = startY + height;
            transform.position = pos;

            yield return null;
        }

        // Ensure exact landing
        Vector3 finalPos = transform.position;
        finalPos.y = startY;
        transform.position = finalPos;

        isJumping = false;
    }
}