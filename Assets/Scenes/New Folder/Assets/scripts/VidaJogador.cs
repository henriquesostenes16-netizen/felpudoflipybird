using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaJogador : MonoBehaviour
{
    public int vidasMax = 3;
    public Texture2D texturaCoracao;      // arraste o coracao.png aqui
    public float invulneravel = 1.5f;

    [Header("Chão")]
    public float yChao = -4.3f;           // abaixo disso conta como "tocando embaixo"
    public float tempoAteAviso = 3f;
    public float tempoParaSair = 10f;

    int vidas;
    float tempoInv;
    float noChao;
    float contagem = -1f;
    bool morto;
    string motivo = "";
    SpriteRenderer[] sprites;

    void Awake()
    {
        Time.timeScale = 1f;
        vidas = vidasMax;
        sprites = GetComponentsInChildren<SpriteRenderer>();

        if (GetComponent<Collider2D>() == null)
        {
            CircleCollider2D c = gameObject.AddComponent<CircleCollider2D>();
            c.radius = 0.5f;
        }
    }

    void OnTriggerEnter2D(Collider2D o) { Tocou(o); }
    void OnTriggerStay2D(Collider2D o) { Tocou(o); }

    void Tocou(Collider2D o)
    {
        if (morto || tempoInv > 0f) return;

        if (o.GetComponent<Laser>() != null || o.GetComponent<Chefe>() != null)
        {
            LevarDano();
            return;
        }

        if (o.GetComponent<Vida>() != null)   // lesma
        {
            Destroy(o.gameObject);
            LevarDano();
        }
    }

    void LevarDano()
    {
        vidas--;
        tempoInv = invulneravel;
        if (vidas <= 0) Morrer("Você levou 3 batidas");
    }

    void Morrer(string porque)
    {
        morto = true;
        motivo = porque;
        foreach (SpriteRenderer s in sprites) s.enabled = true;
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (morto)
        {
            if (Input.GetKeyDown(KeyCode.R)) Reiniciar();
            return;
        }

        // pisca enquanto está invulnerável
        if (tempoInv > 0f)
        {
            tempoInv -= Time.deltaTime;
            bool ligado = Mathf.FloorToInt(Time.time * 10f) % 2 == 0;
            foreach (SpriteRenderer s in sprites) s.enabled = ligado;
        }
        else
        {
            foreach (SpriteRenderer s in sprites) s.enabled = true;
        }

        // tempo no chão
        if (transform.position.y <= yChao)
        {
            noChao += Time.deltaTime;
            if (contagem < 0f && noChao >= tempoAteAviso) contagem = tempoParaSair;
        }
        else
        {
            noChao = 0f;
            contagem = -1f;
        }

        if (contagem >= 0f)
        {
            contagem -= Time.deltaTime;
            if (contagem <= 0f) Morrer("Você ficou tempo demais no chão");
        }
    }

    public static void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnGUI()
    {
        // corações
        for (int i = 0; i < vidas; i++)
        {
            Rect r = new Rect(15 + i * 60, 15, 52, 52);
            if (texturaCoracao != null)
            {
                GUI.DrawTexture(r, texturaCoracao, ScaleMode.ScaleToFit);
            }
            else
            {
                GUI.color = Color.red;
                GUI.DrawTexture(r, Texture2D.whiteTexture);
                GUI.color = Color.white;
            }
        }

        GUIStyle grande = new GUIStyle(GUI.skin.label);
        grande.alignment = TextAnchor.MiddleCenter;

        // contagem do chão
        if (!morto && contagem >= 0f)
        {
            grande.fontSize = 40;
            grande.normal.textColor = new Color(1f, 0.35f, 0.2f);
            GUI.Label(new Rect(0, 20, Screen.width, 60),
                "SAIA DO CHÃO!  " + Mathf.CeilToInt(contagem), grande);
        }

        // fim de jogo
        if (morto)
        {
            GUI.color = new Color(0f, 0f, 0f, 0.6f);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
            GUI.color = Color.white;

            grande.fontSize = 56;
            grande.normal.textColor = Color.white;
            GUI.Label(new Rect(0, Screen.height / 2 - 80, Screen.width, 70), "FIM DE JOGO", grande);

            grande.fontSize = 26;
            GUI.Label(new Rect(0, Screen.height / 2 - 10, Screen.width, 40), motivo, grande);
            GUI.Label(new Rect(0, Screen.height / 2 + 30, Screen.width, 40), "Aperte R para reiniciar", grande);
        }
    }
}