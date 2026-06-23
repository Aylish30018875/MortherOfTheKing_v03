using UnityEngine;

public class GhostTower : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] SpriteRenderer _spriteRendererZone;
    [SerializeField] Color _validColour = new Color(0, 1, 0, 0.8f);
    [SerializeField] Color _invalidColour = new Color(1, 0, 0, 0.8f);
    [SerializeField] Color _validColourZone = new Color(0, 1, 0, 0.04f);
    [SerializeField] Color _invalidColourZone = new Color(1, 0, 0, 0.04f);
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Color colour = _spriteRenderer.color;
        colour.a = 0.4f;

        _spriteRenderer.color = colour;
        _spriteRendererZone.color = colour;
    }

    public void SetValid(bool valid)
    {
        if (valid)
        {
            _spriteRenderer.color = _validColour;
            _spriteRendererZone.color = _validColourZone;
        }
        else
        {
            _spriteRenderer.color = _invalidColour;
            _spriteRendererZone.color = _invalidColourZone;
        }
    }
}
