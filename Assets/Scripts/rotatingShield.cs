using UnityEngine;

public class Shield : MonoBehaviour
{
    public float rotateSpeed = 180f;

    void Update()
    {
        transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
    }
}