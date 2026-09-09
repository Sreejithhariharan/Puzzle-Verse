using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimeCardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Card Image")]
    [SerializeField] private Image cardImage;

    [Header("Outline")]
    [SerializeField] private Outline outline;

    [Header("Animation")]
    [SerializeField] private float hoverScale = 1.06f;
    [SerializeField] private float selectedScale = 1.08f;
    [SerializeField] private float animationSpeed = 10f;

    [Header("Colors")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = new Color(1.15f, 1.15f, 1.15f, 1f);
    [SerializeField] private Color selectedColor = new Color(1.2f, 1.2f, 1.2f, 1f);

    private Vector3 normalScale;
    private Vector3 targetScale;

    private bool isHovering;
    private bool isSelected;

    private void Awake()
    {
        normalScale = transform.localScale;
        targetScale = normalScale;

        if (cardImage != null)
            cardImage.color = normalColor;

        if (outline != null)
            outline.enabled = false;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * animationSpeed
        );

        if (cardImage != null)
        {
            Color targetColor = normalColor;

            if (isSelected)
                targetColor = selectedColor;
            else if (isHovering)
                targetColor = hoverColor;

            cardImage.color = Color.Lerp(
                cardImage.color,
                targetColor,
                Time.deltaTime * animationSpeed
            );
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;

        if (!isSelected)
        {
            targetScale = normalScale * hoverScale;

            if (outline != null)
                outline.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        if (!isSelected)
        {
            targetScale = normalScale;

            if (outline != null)
                outline.enabled = false;
        }
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;

        if (isSelected)
        {
            targetScale = normalScale * selectedScale;

            if (outline != null)
                outline.enabled = true;
        }
        else
        {
            targetScale = normalScale;

            if (outline != null)
                outline.enabled = isHovering;
        }
    }

    public bool IsSelected()
    {
        return isSelected;
    }
}