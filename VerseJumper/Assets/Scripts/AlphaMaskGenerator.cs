using UnityEngine;

public class AlphaMaskGenerator : MonoBehaviour
{
    public Texture2D baseTexture; // The source texture
    public Color maskColor = Color.black; // Color for the alpha mask

    private void Start()
    {
        // Check if the base texture and alpha mask color are provided
        if (baseTexture != null)
        {
            // Create a copy of the base texture
            Texture2D maskedTexture = Instantiate(baseTexture);

            // Apply the alpha mask by setting the alpha channel based on the maskColor
            for (int x = 0; x < maskedTexture.width; x++)
            {
                for (int y = 0; y < maskedTexture.height; y++)
                {
                    Color pixelColor = maskedTexture.GetPixel(x, y);
                    float alpha = (pixelColor.r == maskColor.r && pixelColor.g == maskColor.g && pixelColor.b == maskColor.b) ? 0f : 1f;
                    pixelColor.a = alpha;
                    maskedTexture.SetPixel(x, y, pixelColor);
                }
            }

            // Apply the changes to the texture
            maskedTexture.Apply();

            // Create a new material and assign the masked texture
            Material material = new Material(Shader.Find("Sprites/Default"));
            material.mainTexture = maskedTexture;

            // Create a new GameObject with a Sprite Renderer to display the masked texture
            GameObject maskedObject = new GameObject("MaskedObject");
            SpriteRenderer spriteRenderer = maskedObject.AddComponent<SpriteRenderer>();
            spriteRenderer.material = material;

            // Set the position and scale of the masked object as needed
            maskedObject.transform.position = new Vector3(0, 0, 0); // Set position
            maskedObject.transform.localScale = new Vector3(1, 1, 1); // Set scale
        }
    }
}
