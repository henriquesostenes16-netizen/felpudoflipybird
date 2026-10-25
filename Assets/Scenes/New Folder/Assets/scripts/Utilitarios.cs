using UnityEngine;

public static class Utilitarios
{
    static Sprite quadrado;

    public static Sprite Quadrado()
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