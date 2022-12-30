using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHover : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Color normalColor = Color.white;
    public Color hoverColor1 = new Color32(251, 170, 128, 255);
    public Color hoverColor2 = new Color32(249, 191, 115, 255);

private void Start()
    {
        text.colorGradient = new VertexGradient(normalColor, normalColor, normalColor, normalColor);
    }

    public void OnHover()
    {
        text.colorGradient = new VertexGradient(hoverColor1, hoverColor1, hoverColor2, hoverColor2);
    }

    public void OnExit()
    {
        text.colorGradient = new VertexGradient(normalColor, normalColor, normalColor, normalColor);
    }
}