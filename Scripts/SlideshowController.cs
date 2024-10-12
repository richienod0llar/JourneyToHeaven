using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]

public class SlideshowController : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> images;
    private AudioSource audioSource;
    private int currentImage = 0;
    private const float imageTime = 8f;
    private float imageTimeElapsed = 0f;
    private GameObject openingSceneCamera;
    private Camera openingSceneCameraCamera;
    private bool skipping;
    private const float skipTime = 1f;
    private float skipTimeElapsed = 0f;
    private SpriteRenderer spriteRenderer;
    private GameObject transition;
    private bool transitioning = false;
    private bool transitioningFinal = false;
    // sprite is a square (default asset)
    private SpriteRenderer transitionSpriteRenderer;
    // transitionTime must be less than or euqal to imageTime / 2
    private const float transitionTime = 4f;
    private float transitionTimeElapsed = 0f;
    private const float zoomFactor = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        openingSceneCamera = GameObject.Find("OpeningSceneCamera");
        openingSceneCameraCamera = openingSceneCamera.GetComponent<Camera>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transition = GameObject.Find("Transition");
        transitionSpriteRenderer = transition.GetComponent<SpriteRenderer>();
        // openingSceneCameraCamera
        openingSceneCameraCamera.backgroundColor = new Color(0f, 0f, 0f, 1f);
        // spriteRenderer
        spriteRenderer.sortingLayerName = "SlideshowImages";
        // transform.position
        transform.position = new Vector3(0f, 0f, 0f);
        // transition.transform.position
        transition.transform.position = new Vector3(0f, 0f, 0f);
        // transitionSpriteRenderer
        transitionSpriteRenderer.color = new Color(0f, 0f, 0f, 0f);
        transitionSpriteRenderer.sortingLayerName = ("SlideshowTransition");
    }

    // Update is called once per frame
    void Update()
    {
        Image();
        if (imageTimeElapsed > imageTime - transitionTime / 2f)
        {
            if (currentImage < images.Count - 1)
            {
                transitioning = true;
                transitioningFinal = false;
            }
            else
            {
                transitioning = false;
                transitioningFinal = true;
            }
        }
        if (transitioning && transitionTimeElapsed > transitionTime)
        {
            transitioning = false;
            transitionTimeElapsed = 0f;
        }
        if (!skipping && Input.anyKeyDown)
        {
            skipping = true;
            float skipTransitionRatio = skipTime * 2f / transitionTime;
            if (transitionTimeElapsed < transitionTime / 2f)
            {
                skipTimeElapsed = skipTransitionRatio * transitionTimeElapsed;
            }
            else
            {
                skipTimeElapsed = skipTransitionRatio * (transitionTime - transitionTimeElapsed);
            }
        }
        if (skipping)
        {
            Skip();
        }
        else if (transitioningFinal)
        {
            TransitionFinal();
        }
        else if (transitioning)
        {
            Transition();
        }
    }

    void Image()
    {
        imageTimeElapsed += Time.deltaTime;
        // cameraHeight, cameraWidth
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float cameraHeight = openingSceneCameraCamera.orthographicSize * 2f;
        float cameraWidth = screenRatio * cameraHeight;
        // currentImage
        if (imageTimeElapsed >= imageTime && currentImage < images.Count - 1)
        {
            currentImage++;
            imageTimeElapsed = 0f;
        }
        // spriteRenderer.sprite
        spriteRenderer.sprite = images[currentImage];
        // transform.localScale
        float imageRatio = spriteRenderer.sprite.bounds.size.x / spriteRenderer.sprite.bounds.size.y;
        float scale;
        if (screenRatio <= imageRatio)
        {
            scale = cameraWidth / spriteRenderer.sprite.bounds.size.x;
        }
        else
        {
            scale = cameraHeight / spriteRenderer.sprite.bounds.size.y;
        }
        float zoomedScale = scale + scale * zoomFactor * (imageTimeElapsed / imageTime);
        transform.localScale = new Vector3(zoomedScale, zoomedScale, 1f);
        // transition.transform.localScale
        float xScale = cameraWidth / transitionSpriteRenderer.sprite.bounds.size.x;
        float yScale = cameraHeight / transitionSpriteRenderer.sprite.bounds.size.y;
        transition.transform.localScale = new Vector3(xScale, yScale, 1f);
    }

    void Skip()
    {
        skipTimeElapsed += Time.deltaTime;
        // audioSource.volume
        audioSource.volume = 1f - Math.Max(0f, Math.Min(1f, skipTimeElapsed / skipTime));
        // transitionSpriteRenderer.color
        float a = Math.Max(0f, Math.Min(1f, skipTimeElapsed / skipTime));
        transitionSpriteRenderer.color = new Color(0f, 0f, 0f, a);
        // change scene when skippingTime is over
        if (skipTimeElapsed > skipTime)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    void Transition()
    {
        transitionTimeElapsed += Time.deltaTime;
        // transitionSpriteRenderer.color
        float a = 0f;
        if (transitionTimeElapsed < transitionTime / 2f)
        {
            a = Math.Max(0f, Math.Min(1f, transitionTimeElapsed / (0.45f * transitionTime)));
        }
        else
        {
            a = Math.Max(0f, Math.Min(1f, (transitionTime - transitionTimeElapsed) / (0.45f * transitionTime)));
        }
        transitionSpriteRenderer.color = new Color(0f, 0f, 0f, a);
    }

    void TransitionFinal()
    {
        transitionTimeElapsed += Time.deltaTime;
        // audioSource.volume
        audioSource.volume = 1f - Math.Max(0f, Math.Min(1f, transitionTimeElapsed / transitionTime));
        // transitionSpriteRenderer.color
        float a = 0f;
        if (transitionTimeElapsed < transitionTime / 2f)
        {
            a = Math.Max(0f, Math.Min(1f, transitionTimeElapsed / (0.45f * transitionTime)));
        }
        else
        {
            a = 1f;
        }
        transitionSpriteRenderer.color = new Color(0f, 0f, 0f, a);
        // change scene when transition is over
        if (transitionTimeElapsed > transitionTime)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}