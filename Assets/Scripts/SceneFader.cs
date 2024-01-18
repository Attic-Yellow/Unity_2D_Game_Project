using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image faderPanel;
    public float fadeSpeed = 1f;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(int SceneNumber)
    {
        StartCoroutine(FadeOut(SceneNumber));
    }

    IEnumerator FadeIn()
    {
        faderPanel.color = new Color(faderPanel.color.r, faderPanel.color.g, faderPanel.color.b, 0);

        while (faderPanel.color.a > 0f)
        {
            faderPanel.color = new Color(faderPanel.color.r, faderPanel.color.g, faderPanel.color.b, faderPanel.color.a - (Time.deltaTime / fadeSpeed));
            yield return null;
        }
    }

    IEnumerator FadeOut(int SceneNumber)
    {


        while (faderPanel.color.a < 1f)
        {

            faderPanel.color = new Color(faderPanel.color.r, faderPanel.color.g, faderPanel.color.b, faderPanel.color.a + (Time.deltaTime / fadeSpeed));
            yield return null;
        }

        SceneManager.LoadScene(SceneNumber);
    }
}