using UnityEngine;

public class FadingObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float fadeTime = 2;
    private MeshRenderer _meshRenderer;
    private Color orgColor;
    private LTDescr animation;
    void Start()
    {
        TurnOnAnimation();
        _meshRenderer = GetComponent<MeshRenderer>();
        orgColor = _meshRenderer.material.color;
        Debug.Log(orgColor);
    }

    private void TurnOnAnimation()
    {
        animation = LeanTween.alpha(gameObject, 0, fadeTime)
            .setLoopPingPong(-1).setEase(LeanTweenType.easeInSine);
    }
    public void ResetAnimation()
    {
        LeanTween.cancel(animation.id);
        GetComponent<MeshRenderer>().material.color = orgColor;
        TurnOnAnimation();
    }
}
