using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createSprite : MonoBehaviour
{
    public float pixelPerUnit = 20.0f;
    public Color clickColor = Color.blue;
    public int penSize = 1;
    public int textureWidth = 1000;
    public int textureHeight = 800;

    private float offsetPen = 1.5f;
    private Texture2D texture;
    private Sprite mySprite;
    private SpriteRenderer sr;
    private Color32[] textureColorArray;

    void Awake()
    {
        sr = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sr.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        transform.position = new Vector3(1.5f, 1.5f, 0.0f);

        texture = new Texture2D(textureWidth, textureHeight);
        sr.material.mainTexture = texture;

        CreateTexture(texture, textureWidth, textureHeight, Color.white);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Add/Reset sprite"))
        {
            CreateTexture(texture, textureWidth, textureHeight, Color.white);
            mySprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelPerUnit);
            sr.sprite = mySprite;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mySprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelPerUnit);
            sr.sprite = mySprite;
        }
        if (Input.GetMouseButton(0))
        {
           Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePos += (Vector2)sr.sprite.bounds.extents;
            mousePos *= pixelPerUnit;
            mousePos.x -= offsetPen * pixelPerUnit;
            mousePos.y -= offsetPen * pixelPerUnit;
            Debug.Log("Pos : " + mousePos.x + "/" + mousePos.y);
            if (mousePos.x < 0 || mousePos.x > textureWidth || mousePos.y < 0 || mousePos.y > textureHeight)
                return;
            ChangePixelColor((int)mousePos.x, (int)mousePos.y, penSize, clickColor);

            mySprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelPerUnit);
            sr.sprite = mySprite;
        }
    }

    void ChangePixelColor(int xPos, int yPos, int diameter, Color32 color)
    {
        for (int x = xPos - diameter; x < xPos + diameter; x++)
        {
            for (int y = yPos - diameter; y < yPos + diameter; y++)
            {
                if (xPos - diameter + 1 > 0 && yPos - diameter + 1 > 0 &&
                    xPos + diameter - 1 < textureWidth && yPos + diameter - 1 < textureHeight)
                    texture.SetPixel(x, y, color);
            }
        }
        //texture.SetPixels32(textureColorArray);
        texture.Apply();
    }

    void CreateTexture(Texture2D texture, int width, int height, Color32 color)
    {
        textureColorArray = new Color32[width * height];
        for (int i = 0; i < textureColorArray.Length; i++)
        {
            textureColorArray[i] = color;
        }
        texture.SetPixels32(textureColorArray);
        Debug.Log("length color : " + textureColorArray.Length);
        texture.Apply();
    }
}