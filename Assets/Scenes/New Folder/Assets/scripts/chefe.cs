using System.Collections;
using UnityEngine;

public class Chefe : MonoBehaviour
{
    public Transform alvo;
    public float limiteY = 3f;          // sobe e desce do chefe
    public float limiteLaser = 4f;      // altura máxima do laser
    public float xParado = 6f;
    public float velocidadeEntrada = 2f;
    public float velocidadeVertical = 1.5f;

    [Header("Laser")]
    public float tempoAviso = 1.5f;
    public float duracaoLaser = 0.5f;
    public float alturaLaser = 0.9f;
    public float intervaloMin = 2f;
    public float intervaloMax = 4f;
    [Range(0f, 1f)] public float chanceMirar = 0.7f;
    public float erroMira = 1.5f;

    float t;
    bool atirando;
    GameObject aviso;
    GameObject raio;

    void Update()
    {
        Vector3 p = transform.position;

        if (p.x > xParado)
        {
            p.x -= velocidadeEntrada * Time.deltaTime;
        }
        else
        {
            if (!atirando)
            {
                atirando = true;
                StartCoroutine(CicloLaser());
            }
            t += Time.deltaTime * velocidadeVertical;
            p.y = Mathf.Sin(t) * limiteY;
        }

        transform.position = p;
    }

    IEnumerator CicloLaser()
    {
        yield return new WaitForSeconds(1.5f);

        while (true)
        {
            float y;
            if (alvo != null && Random.value < chanceMirar)
                y = alvo.position.y + Random.Range(-erroMira, erroMira);
            else
                y = Random.Range(-limiteLaser, limiteLaser);
            y = Mathf.Clamp(y, -limiteLaser, limiteLaser);

            // 1) aviso: linha vermelha piscando
            aviso = CriarFaixa("AvisoLaser", y, 0.08f, Color.red, 7);
            SpriteRenderer sa = aviso.GetComponent<SpriteRenderer>();
            float tempo = 0f;
            while (tempo < tempoAviso)
            {
                tempo += Time.deltaTime;
                bool forte = Mathf.FloorToInt(tempo * 8f) % 2 == 0;
                sa.color = new Color(1f, 0f, 0f, forte ? 1f : 0.35f);
                yield return null;
            }
            Destroy(aviso);

            // 2) disparo: raio grosso que machuca
            raio = CriarFaixa("Laser", y, alturaLaser, new Color(1f, 0.25f, 0.25f), 8);
            BoxCollider2D col = raio.AddComponent<BoxCollider2D>();
            col.isTrigger = true;
            raio.AddComponent<Laser>();
            yield return new WaitForSeconds(duracaoLaser);
            Destroy(raio);

            yield return new WaitForSeconds(Random.Range(intervaloMin, intervaloMax));
        }
    }

    GameObject CriarFaixa(string nome, float y, float altura, Color cor, int ordem)
    {
        GameObject go = new GameObject(nome);
        go.transform.position = new Vector3(0f, y, 0f);
        go.transform.localScale = new Vector3(22f, altura, 1f);

        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = Utilitarios.Quadrado();
        sr.color = cor;
        sr.sortingOrder = ordem;
        return go;
    }

    void OnDestroy()
    {
        if (aviso != null) Destroy(aviso);
        if (raio != null) Destroy(raio);
    }
}