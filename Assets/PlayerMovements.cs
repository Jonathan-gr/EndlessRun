using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float jumpDuration = 0.8f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(JumpRoutine());
        }
    }

    IEnumerator JumpRoutine()
    {
        animator.SetBool("isJumping", true);

        yield return new WaitForSeconds(jumpDuration);

        animator.SetBool("isJumping", false);
    }
}