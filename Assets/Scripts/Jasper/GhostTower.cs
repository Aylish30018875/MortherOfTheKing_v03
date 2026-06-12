using UnityEngine;

public class GhostTower : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Color _validColour = new Color(0, 1, 0, 0.4f);
    [SerializeField] Color _invalidColour = new Color(1, 0, 0, 0.4f);

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Color colour = _spriteRenderer.color;
        colour.a = 0.4f;
        _spriteRenderer.color = colour;
    }

    public void SetValid(bool valid)
    {
        if (valid)
        {
            _spriteRenderer.color = _validColour;
        }
        else
        {
            _spriteRenderer.color = _invalidColour;
        }
    }
}
