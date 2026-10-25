using UnityEngine;

public class PrenderNaTela : MonoBehaviour
{
    public float margem = 0.5f;

    void LateUpdate()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        float z = -cam.transform.position.z;
        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0f, 0f, z));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1f, 1f, z));

        Vector3 p = transform.position;
        p.x = Mathf.Clamp(p.x, min.x + margem, max.x - margem);
        p.y = Mathf.Clamp(p.y, min.y + margem, max.y - margem);
        transform.position = p;
    }
}