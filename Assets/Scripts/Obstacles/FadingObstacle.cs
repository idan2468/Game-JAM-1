using UnityEngine;

public class FadingObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float fadeTime = 2;
    private float _thresholdDisappearAlpha = 0.2f; 
    private MeshRenderer _meshRenderer;
    private Color orgColor;
    private LTDescr animation;
    private BoxCollider _collider;
    void Start()
    {
        TurnOnAnimation();
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<BoxCollider>();
        orgColor = _meshRenderer.material.color;
    }

    private void TurnOnAnimation()
    {
        animation = LeanTween.alpha(gameObject, 0, fadeTime)
            .setLoopPingPong(-1).setEase(LeanTweenType.easeInSine).setOnUpdate((float val) =>
            {
                _collider.enabled = val > _thresholdDisappearAlpha;
            });
    }
    public void ResetAnimation()
    {
        LeanTween.cancel(animation.id);
        GetComponent<MeshRenderer>().material.color = orgColor;
        TurnOnAnimation();
    }
}
