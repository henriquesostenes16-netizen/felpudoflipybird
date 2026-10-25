using UnityEngine;

public class InimigoVoador : MonoBehaviour
{
    public static int ativos;
    public float velocidade = 3f;
    Vector2 direcao = Vector2.left;

    void Start() { ativos++; }
    void OnDestroy() { ativos--; }

    public void DefinirDirecao(Vector2 d) { direcao = d.normalized; }

    void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime, Space.World);
        if (Mathf.Abs(transform.position.x) > 14f || Mathf.Abs(transform.position.y) > 8f)
            Destroy(gameObject);
    }
}