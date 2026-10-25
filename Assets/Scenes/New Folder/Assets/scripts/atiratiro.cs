using UnityEngine;

public class AtiraTiro : MonoBehaviour
{
    public Sprite spriteTiro;      // opcional: sem imagem, vira um retângulo amarelo
    public float intervalo = 0.25f;
    float proximo;
    static Sprite quadrado;

    void Update()
    {
        if (Time.time < proximo) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
        {
            proximo = Time.time + intervalo;
            Atirar();
        }
    }

    void Atirar()
    {
        GameObject go = new GameObject("Tiro");
        go.transform.position = transform.position + new Vector3(0.8f, 0f, 0f);

        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        if (spriteTiro != null)
        {
            sr.sprite = spriteTiro;
        }
        else
        {
            sr.sprite = Quadrado();
            sr.color = Color.yellow;
            go.transform.localScale = new Vector3(0.5f, 0.15f, 1f);
        }
        sr.sortingOrder = 6;

        BoxCollider2D col = go.AddComponent<BoxCollider2D>();
        col.isTrigger = true;

        Rigidbody2D rb = go.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        go.AddComponent<Tiro>();
    }

    static Sprite Quadrado()
    {
        if (quadrado == null)
        {
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, Color.white);
            tex.Apply();
            quadrado = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f);
        }
        return quadrado;
    }
}