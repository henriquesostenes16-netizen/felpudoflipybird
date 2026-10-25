using UnityEngine;

public class MoveFundo : MonoBehaviour
{
    public float velocidade = 2f;
    public int copias = 4;
    public bool original = true;
    float largura;

    void Start()
    {
        largura = GetComponent<SpriteRenderer>().bounds.size.x;
        if (!original) return;

        for (int i = -1; i < copias - 1; i++)
        {
            if (i == 0) continue;
            GameObject c = Instantiate(gameObject,
                transform.position + Vector3.right * largura * i,
                Quaternion.identity);
            c.GetComponent<MoveFundo>().original = false;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.left * velocidade * Time.deltaTime);
        if (transform.position.x < -largura * 2f)
            transform.position += Vector3.right * largura * copias;
    }
}