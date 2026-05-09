using UnityEngine;
using System.Collections;

public class PlayerInvincibility : MonoBehaviour
{
    public float invincibleDuration = 7f;
    public float flashSpeed = 0.15f;
    public float shieldDownWarning = 2f; // how many seconds before end to play warning

    public bool IsInvincible { get; private set; }

    private Renderer[] playerRenderers;
    private bool warningSounded = false;

    private void Start()
    {
        playerRenderers = GetComponentsInChildren<Renderer>();
    }

    public void ActivateShield()
    {
        StopAllCoroutines();
        warningSounded = false;
        StartCoroutine(InvincibilityTimer());
    }

    private IEnumerator InvincibilityTimer()
    {
        IsInvincible = true;
        float elapsed = 0f;
        bool visible = true;

        while (elapsed < invincibleDuration)
        {
            // play warning sound when X seconds remain
            if (!warningSounded && elapsed >= invincibleDuration - shieldDownWarning)
            {
                warningSounded = true;
                PlayerAudio.Instance.PlayShieldDownSound();
            }

            visible = !visible;
            SetPlayerVisible(visible);
            yield return new WaitForSeconds(flashSpeed);
            elapsed += flashSpeed;
        }

        SetPlayerVisible(true);
        IsInvincible = false;
    }

    private void SetPlayerVisible(bool visible)
    {
        foreach (Renderer r in playerRenderers)
            r.enabled = visible;
    }
}