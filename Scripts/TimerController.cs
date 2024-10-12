using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(TextMeshProUGUI))]

// timer is a child of canvas
public class TimerController : MonoBehaviour
{
    private bool isGameFinished = false;
    private bool isGameOver = false;
    private RectTransform rectTransform;
    private float time = 0f;
    private TMP_Text tmpText;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        tmpText = GetComponent<TextMeshProUGUI>();
        // rectTransform
        rectTransform.anchorMax = new Vector2(0.97f, 0.95f);
        rectTransform.anchorMin = new Vector2(0.89f, 0.87f);
        rectTransform.offsetMax = new Vector2(0f, 0f);
        rectTransform.offsetMin = new Vector2(0f, 0f);
        // tmpText
        tmpText.alignment = TextAlignmentOptions.Bottom;
        tmpText.color = new Color32(0, 0, 255, 255);
        tmpText.enableAutoSizing = true;
        tmpText.fontSizeMax = 1000f;
        tmpText.fontSizeMin = 0f;
        tmpText.text = "0.0";
    }

    // Update is called once per frame
    void Update()
    {
        // game finished: change text color to green
        if (isGameFinished)
        {
            tmpText.color = new Color(0, 255, 0, 255);
        }
        // game over: change text color to red
        else if (isGameOver)
        {
            tmpText.color = new Color(255, 0, 0, 255);
        }
        // else: update text
        else
        {
            time += Time.deltaTime;
            string timeFormat = "";
            if (time >= 600f)
            {
                timeFormat = "mm\\:ss\\:ff";
            }
            else if (time >= 60f)
            {
                timeFormat = "m\\:ss\\:ff";
            }
            else
            {
                timeFormat = "s\\:ff";
            }
            TimeSpan timeSpan = TimeSpan.FromSeconds((double)time);
            string timeText = timeSpan.ToString(timeFormat);
            tmpText.text = timeText;
        }
    }

    public void SetIsGameFinishied(bool value)
    {
        isGameFinished = value;
    }

    public void SetIsGameOver(bool value)
    {
        isGameOver = value;
    }
}