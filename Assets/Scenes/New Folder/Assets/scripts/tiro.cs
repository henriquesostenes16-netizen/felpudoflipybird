using UnityEngine;

public class Tiro : MonoBehaviour
{
    public float velocidade = 12f;
    public int dano = 1;

    void Update()
    {
        transform.position += Vector3.right * velocidade * Time.deltaTime;
        if (transform.position.x > 12f) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        Vida v = outro.GetComponent<Vida>();
        if (v == null) return;
        v.Dano(dano);
        Destroy(gameObject);
    }
}