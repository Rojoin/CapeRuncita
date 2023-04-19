using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite[Random.Range(0, sprite.Length)];
    }
}
