using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vida = 1;
    SpriteRenderer sr;
    float flash;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void Dano(int d)
    {
        vida -= d;
        flash = 0.1f;
        if (vida <= 0) Destroy(gameObject);
    }

    void Update()
    {
        if (sr == null) return;
        if (flash > 0f)
        {
            flash -= Time.deltaTime;
            sr.color = Color.red;
        }
        else
        {
            sr.color = Color.white;
        }
    }
}