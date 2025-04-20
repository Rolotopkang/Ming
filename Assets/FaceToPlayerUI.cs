using UnityEngine;

public class FaceToPlayerUI : MonoBehaviour
{
    void Update()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        Vector3 direction = cam.transform.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }
}
