using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShakeButton : MonoBehaviour
{
    [Header("Shake Settings")]
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeStrength = 15f;
    [SerializeField] private float shakeSpeed = 25f;
    [SerializeField] private float pauseBetweenShakes = 2f;

    [Header("Flicker Settings")]
    [SerializeField] private Color[] flickerColors = { Color.yellow, Color.red, Color.white };
    [SerializeField] private float flickerSpeed = 0.1f;

    private Vector3 originalRotation;
    private Image buttonImage;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        originalRotation = transform.eulerAngles;
        buttonImage = GetComponent<Image>();
    }

    public void StartShaking()
    {
        if (shakeRoutine == null)
        {
            shakeRoutine = StartCoroutine(ShakeLoop());
        }
    }

    private IEnumerator ShakeLoop()
    {
        while (true)
        {
            float elapsed = 0f;
            float flickerTimer = 0f;
            int colorIndex = 0;

            // ---- SHAKE PHASE (Uses Unscaled Time) ----
            while (elapsed < shakeDuration)
            {
                // unscaledDeltaTime allows this to move while Time.timeScale is 0
                elapsed += Time.unscaledDeltaTime;
                flickerTimer += Time.unscaledDeltaTime;

                float dampen = 1f - Mathf.Clamp01(elapsed / shakeDuration);
                float angle = Mathf.Sin(elapsed * shakeSpeed) * shakeStrength * dampen;
                transform.eulerAngles = originalRotation + new Vector3(0, 0, angle);

                if (buttonImage != null && flickerTimer >= flickerSpeed)
                {
                    buttonImage.color = flickerColors[colorIndex % flickerColors.Length];
                    colorIndex++;
                    flickerTimer = 0f;
                }
                yield return null;
            }

            // ---- RESET TO NORMAL ----
            transform.eulerAngles = originalRotation;
            if (buttonImage != null) buttonImage.color = Color.white;

            // ---- PAUSE (Uses Realtime) ----
            yield return new WaitForSecondsRealtime(pauseBetweenShakes);
        }
    }
}
