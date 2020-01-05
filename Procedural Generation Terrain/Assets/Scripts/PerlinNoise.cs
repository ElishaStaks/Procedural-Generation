/*
* Author: Elisha_Anagnostakis
* Description: This script generates perlin noise for our mesh
*/

using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Width of the texture")]
    private int m_width = 256;

    [SerializeField]
    [Tooltip("Height of the texture")]
    private int m_height = 256;

    [SerializeField]
    [Tooltip("Scale of the texture")]
    private float m_scale = 20;

    [SerializeField]
    [Tooltip("X offset of the texture")]
    private float m_offsetX = 100;

    [SerializeField]
    [Tooltip("Y offset of the texture")]
    private float m_offsetY = 100;

    private void Start()
    {
        m_offsetX = Random.Range(0f, 5000f);
        m_offsetY = Random.Range(0f, 5000f);
    }

    // Start is called before the first frame update
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    /// <summary>
    /// Generates the textures noise
    /// </summary>
    /// <returns></returns>
    Texture2D GenerateTexture()
    {
        // Create the 2d texture and its size with the width and height member variables
        Texture2D texture = new Texture2D(m_width, m_height);

        // Generate Perlin noise map for the texture
        // Cycles through the width and height of the texture 
        for (int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                // Calculates the perlin noise colour
                Color colour = CalculateColour(x, y);
                texture.SetPixel(x, y, colour);
            }
        }
        // Applys the above properties to the texture
        texture.Apply();
        return texture;
    }

    /// <summary>
    /// Calculates the noise
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    Color CalculateColour(int x, int y)
    {
        // Create an x and y coordinate for our noise 
        float xCoord = (float)x / m_width * m_scale + m_offsetX;
        float yCoord = (float)y / m_height * m_scale + m_offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        // Returns the calculated colour, scale and offset of the texture
        return new Color(sample, sample, sample);
    }
}