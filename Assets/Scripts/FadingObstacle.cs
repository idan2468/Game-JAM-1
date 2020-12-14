using System;
using System.Collections;
using UnityEngine;

public class FadingObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float fadeTime = 2;
    [SerializeField] private float fadedTime = 5;
    [SerializeField] private float unfadedTime = 7;
    private float thresholdDisappearAlpha = 0.2f; 
    private MeshRenderer meshRenderer;
    private Color orgColor;
    private LTDescr fadingAnimation;
    private BoxCollider boxCollider;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        orgColor = meshRenderer.material.color;
        StartCoroutine(FadingRoutine());
    }

    IEnumerator FadingRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(unfadedTime);
            AnimateFading();
            yield return new WaitForSeconds(fadedTime);
            yield return new WaitUntil(() => Physics.CheckBox(boxCollider.center, boxCollider.size / 2));
            AnimateUnfading();
        }
    }
    
    private void AnimateFading()
    {
        fadingAnimation = LeanTween.alpha(gameObject, 0, fadeTime).setEase(LeanTweenType.easeInSine)
            .setOnComplete(() => boxCollider.enabled = false);
    }

    private void AnimateUnfading()
    {
        boxCollider.enabled = true;
        fadingAnimation = LeanTween.alpha(gameObject, 1, fadeTime).setEase(LeanTweenType.easeInSine);
    }
    
    public void ResetAnimation()
    {
        LeanTween.cancel(fadingAnimation.id);
        GetComponent<MeshRenderer>().material.color = orgColor;
        AnimateFading();
    }
}
