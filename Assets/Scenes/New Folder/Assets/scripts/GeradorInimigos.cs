using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeradorInimigos : MonoBehaviour
{
    public Sprite spriteInimigo;
    public float escalaInimigo = 1f;
    public GameObject inimigoPrefab;      // opcional
    public Transform alvo;
    public float intervalo = 2.5f;
    public int maxSimultaneos = 3;
    public float limiteX = 10f;
    public float limiteY = 4f;
    public float velocidade = 3f;
    public float variacaoMira = 1.5f;

    [Header("Chefe")]
    public float tempoAteChefe = 30f;
    public Sprite spriteChefe;            // vazio = usa a imagem da lesma
    public float escalaChefe = 3f;
    public int vidaChefe = 20;

    class Voador
    {
        public Transform t;
        public Vector2 dir;
    }

    readonly List<Voador> ativos = new List<Voador>();
    float tempo;
    float relogio;
    bool pronto;
    bool chefeFase;
    bool chefeCriado;
    bool venceu;
    GameObject chefe;

    void Start()
    {
        if (spriteInimigo == null && inimigoPrefab == null)
        {
            Debug.LogError("Arraste uma imagem no campo Sprite Inimigo.");
            return;
        }
        if (inimigoPrefab != null && inimigoPrefab.GetComponentInChildren<GeradorInimigos>() != null)
        {
            Debug.LogError("O prefab da lesma tem o script GeradorInimigos dentro. Remova pelo Inspector.");
            return;
        }

        intervalo = Mathf.Max(1f, intervalo);
        maxSimultaneos = Mathf.Clamp(maxSimultaneos, 1, 10);
        tempo = intervalo;
        pronto = true;
    }

    void Update()
    {
        if (venceu && Input.GetKeyDown(KeyCode.R))
        {
            VidaJogador.Reiniciar();
            return;
        }

        if (!pronto) return;

        relogio += Time.deltaTime;

        if (!chefeFase && relogio >= tempoAteChefe)
            IniciarChefe();

        if (chefeFase)
        {
            if (chefeCriado && chefe == null) venceu = true;
            return;
        }

        tempo += Time.deltaTime;
        if (tempo >= intervalo)
        {
            tempo = 0f;
            Gerar();
        }

        for (int i = ativos.Count - 1; i >= 0; i--)
        {
            Voador v = ativos[i];
            if (v.t == null)
            {
                ativos.RemoveAt(i);
                continue;
            }

            v.t.position += (Vector3)(v.dir * velocidade * Time.deltaTime);

            if (Mathf.Abs(v.t.position.x) > limiteX + 6f || Mathf.Abs(v.t.position.y) > limiteY + 6f)
            {
                Destroy(v.t.gameObject);
                ativos.RemoveAt(i);
            }
        }
    }

    void Gerar()
    {
        if (ativos.Count >= maxSimultaneos) return;

        Vector3 pos = new Vector3(limiteX, Random.Range(-limiteY, limiteY), 0f);
        GameObject go;

        if (spriteInimigo != null)
        {
            go = new GameObject("Lesma");
            go.transform.position = pos;
            go.transform.localScale = Vector3.one * escalaInimigo;

            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = spriteInimigo;
            sr.sortingOrder = 5;

            CircleCollider2D col = go.AddComponent<CircleCollider2D>();
            col.isTrigger = true;
        }
        else
        {
            go = Instantiate(inimigoPrefab, pos, Quaternion.identity);
            foreach (SpriteRenderer sr in go.GetComponentsInChildren<SpriteRenderer>())
                sr.sortingOrder = 5;
        }

        if (go.GetComponent<Vida>() == null)
            go.AddComponent<Vida>().vida = 1;

        Vector2 dir = Vector2.left;
        if (alvo != null)
        {
            Vector2 destino = (Vector2)alvo.position + new Vector2(0f, Random.Range(-variacaoMira, variacaoMira));
            dir = (destino - (Vector2)pos).normalized;
        }

        ativos.Add(new Voador { t = go.transform, dir = dir });
    }

    void IniciarChefe()
    {
        chefeFase = true;

        foreach (Voador v in ativos)
            if (v.t != null) Destroy(v.t.gameObject);
        ativos.Clear();

        Sprite sp = spriteChefe != null ? spriteChefe : spriteInimigo;
        if (sp == null)
        {
            Debug.LogError("O chefe precisa de uma imagem: preencha Sprite Chefe ou Sprite Inimigo.");
            return;
        }

        chefe = new GameObject("Chefe");
        chefe.transform.position = new Vector3(limiteX + 3f, 0f, 0f);
        chefe.transform.localScale = Vector3.one * escalaChefe;

        SpriteRenderer sr = chefe.AddComponent<SpriteRenderer>();
        sr.sprite = sp;
        sr.sortingOrder = 5;

        CircleCollider2D col = chefe.AddComponent<CircleCollider2D>();
        col.isTrigger = true;

        chefe.AddComponent<Vida>().vida = vidaChefe;

        Chefe c = chefe.AddComponent<Chefe>();
        c.alvo = alvo;
        c.limiteY = limiteY * 0.75f;
        c.limiteLaser = limiteY;

        chefeCriado = true;
    }

    void OnGUI()
    {
        if (!venceu) return;

        GUIStyle estilo = new GUIStyle(GUI.skin.label);
        estilo.alignment = TextAnchor.MiddleCenter;
        estilo.normal.textColor = Color.white;

        estilo.fontSize = 56;
        GUI.Label(new Rect(0, Screen.height / 2 - 60, Screen.width, 70), "VOCÊ VENCEU!", estilo);
        estilo.fontSize = 26;
        GUI.Label(new Rect(0, Screen.height / 2 + 10, Screen.width, 40), "Aperte R para reiniciar", estilo);
    }
}