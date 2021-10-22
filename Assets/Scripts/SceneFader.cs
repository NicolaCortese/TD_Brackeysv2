using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public AnimationCurve fadeCurve;

    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void Start()
    {        
        StartCoroutine(FadeIn());
    }
    
    public void FadeTo(int scene)
    {
      
      StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        
        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime * 0.5f;
            float a = fadeCurve.Evaluate(t);
            fadeImage.GetComponent<CanvasGroup>().alpha = a;
            yield return 0;
        }
    }
    IEnumerator FadeOut(int scene)
    {
        
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 0.5f;
            float a = fadeCurve.Evaluate(t);
            fadeImage.GetComponent<CanvasGroup>().alpha = a;
            yield return 0;
        }
        SceneManager.LoadScene(scene);
        
    }   


}
