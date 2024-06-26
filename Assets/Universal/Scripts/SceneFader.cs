using UnityEngine;

public class SceneFader : MonoBehaviour
{
    CanvasGroup cvg;

    private void Awake()
    {
        cvg = GetComponent<CanvasGroup>();
        cvg.alpha = 1;
    }

    void Start()
    {
        FadeX.FadeCanvas(cvg, 0, 5);
    }
}
