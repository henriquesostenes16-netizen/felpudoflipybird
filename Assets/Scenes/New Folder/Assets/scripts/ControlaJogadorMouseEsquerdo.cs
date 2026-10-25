using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ControlaJogadorMouseEsquerdo : MonoBehaviour
{
    bool comecou;
    Rigidbody2D corpoJogador;
    float horizontal;

    public float forcaImpulso = 500f;
    public float velocidadeX = 5f;
    public float limiteX = 8f;
    public float limiteY = 4.5f;

    void Awake()
    {
        corpoJogador = GetComponent<Rigidbody2D>();
        if (corpoJogador == null)
            corpoJogador = gameObject.AddComponent<Rigidbody2D>();

        // fica parado até o primeiro clique
        corpoJogador.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Fire1"))
        {
            if (!comecou)
            {
                comecou = true;
                corpoJogador.bodyType = RigidbodyType2D.Dynamic;
            }
            corpoJogador.linearVelocity = new Vector2(corpoJogador.linearVelocity.x, 0);
            corpoJogador.AddForce(new Vector2(0, forcaImpulso));
        }

        // mantém o personagem dentro da tela
        Vector3 p = transform.position;
        p.x = Mathf.Clamp(p.x, -limiteX, limiteX);
        p.y = Mathf.Clamp(p.y, -limiteY, limiteY);
        transform.position = p;
    }

    void FixedUpdate()
    {
        if (!comecou) return;
        corpoJogador.linearVelocity = new Vector2(horizontal * velocidadeX, corpoJogador.linearVelocity.y);
    }
}