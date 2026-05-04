using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Renderer))]
public class TextureTilingByScale : MonoBehaviour
{
    [SerializeField] private Vector2 _tilingMultiplier = Vector2.one;

    private Renderer _renderer;
    private Vector3 _lastScale;

#if UNITY_EDITOR
    private void Update()
    {
        if (Application.isPlaying)
            return;

        if (transform.localScale == _lastScale)
            return;

        UpdateTiling();
    }

    private void OnValidate()
    {
        Init();
        UpdateTiling();
    }
#endif

    private void Awake()
    {
        Init();

#if UNITY_EDITOR
        if (Application.isPlaying == false)
            UpdateTiling();
#endif
    }

    private void Init()
    {
        if (_renderer == null)
            _renderer = GetComponent<Renderer>();
    }

    private void UpdateTiling()
    {
        if (_renderer == null)
            return;

        Vector3 scale = transform.localScale;

        _renderer.sharedMaterial.mainTextureScale = new Vector2(
            scale.x * _tilingMultiplier.x,
            scale.z * _tilingMultiplier.y
        );

        _lastScale = scale;
    }
}