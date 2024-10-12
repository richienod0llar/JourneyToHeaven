using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(TextMeshProUGUI))]

// gameFinishedText is a child of canvas
public class GameFinishedTextController : MonoBehaviour
{
    private RectTransform rectTransform;
    private TMP_Text tmpText;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        tmpText = GetComponent<TextMeshProUGUI>();
        // rectTransform
        rectTransform.anchorMax = new Vector2(0.7f, 0.6f);
        rectTransform.anchorMin = new Vector2(0.3f, 0.4f);
        rectTransform.offsetMax = new Vector2(0f, 0f);
        rectTransform.offsetMin = new Vector2(0f, 0f);
        // tmpText
        tmpText.alignment = TextAlignmentOptions.Center;
        tmpText.color = new Color32(0, 255, 0, 255);
        tmpText.enableAutoSizing = true;
        tmpText.fontSizeMax = 1000f;
        tmpText.fontSizeMin = 0f;
        tmpText.fontStyle = FontStyles.Bold;
        tmpText.text = "Game Finished";
    }

    // Update is called once per frame
    void Update()
    {
    }
}