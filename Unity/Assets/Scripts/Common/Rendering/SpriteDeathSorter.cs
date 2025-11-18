using UnityEngine;

[ExecuteAlways]
public class SpriteDepthSorter : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = spriteRenderer == null ? GetComponentInParent<SpriteRenderer>() : spriteRenderer;
    }

    private void LateUpdate()
    {
#if UNITY_EDITOR
        if (spriteRenderer != null)
#endif
            spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -1000);
    }
}
