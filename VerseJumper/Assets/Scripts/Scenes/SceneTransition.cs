using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    public AudioManager am;

    private bool isFading = false;

    private void Start()
    {
        // Ensure the fade image is visible and at full opacity
        fadeImage.color = Color.black;
        am = FindObjectOfType<AudioManager>();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = 1 - (timer / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0); // Ensure it's completely transparent
    }

    public void TransitionToScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(Transition(sceneName));
        }
    }

    private IEnumerator Transition(string sceneName)
    {
        isFading = true;

        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = timer / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = Color.black;
        am.thanos();
        SceneManager.LoadScene(sceneName);
    }
}
