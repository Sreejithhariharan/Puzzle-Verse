using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Text")]
    [SerializeField] private TMP_Text buttonText;

    [Header("Hover Indicator")]
    [SerializeField] private TMP_Text hoverIndicator;

    [Header("Colors")]
    [SerializeField] private Color normalColor = Color.yellow;
    [SerializeField] private Color hoverColor = new Color(1f, 0.65f, 0.05f);

    [Header("Animation")]
    [SerializeField] private float hoverScale = 1.08f;
    [SerializeField] private float animationSpeed = 10f;
    [SerializeField] private float indicatorSpeed = 8f;

    private Vector3 normalScale;
    private Vector3 targetScale;

    private Color indicatorTargetColor;

    private void Awake()
    {
        normalScale = transform.localScale;
        targetScale = normalScale;

        if (buttonText != null)
            buttonText.color = normalColor;

        if (hoverIndicator != null)
        {
            Color color = hoverIndicator.color;
            color.a = 0f;
            hoverIndicator.color = color;

            indicatorTargetColor = color;
        }
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * animationSpeed
        );

        if (hoverIndicator != null)
        {
            hoverIndicator.color = Color.Lerp(
                hoverIndicator.color,
                indicatorTargetColor,
                Time.deltaTime * indicatorSpeed
            );
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = normalScale * hoverScale;

        if (buttonText != null)
            buttonText.color = hoverColor;

        if (hoverIndicator != null)
        {
            indicatorTargetColor = new Color(
                hoverColor.r,
                hoverColor.g,
                hoverColor.b,
                1f
            );
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = normalScale;

        if (buttonText != null)
            buttonText.color = normalColor;

        if (hoverIndicator != null)
        {
            indicatorTargetColor = new Color(
                hoverColor.r,
                hoverColor.g,
                hoverColor.b,
                0f
            );
        }
    }
}