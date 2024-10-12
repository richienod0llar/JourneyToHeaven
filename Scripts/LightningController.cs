using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

// lightning is a child of hotAirBallon or thundercloud
public class LightningController : MonoBehaviour
{
    private bool alreadyPlayedAudio = false;
    private AudioSource audioSource;
    // if true, must be set by HotAirBalloon after calling Instantiate
    private bool isChildOfHotAirBalloon;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer thundercloudSpriteRenderer;
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        thundercloudSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        // audioSource
        audioSource.playOnAwake = false;
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "ThundercloudLightning";
        // transform.localScale
        transform.localScale = new Vector3(1f, 1f, 1f);
        float scale = thundercloudSpriteRenderer.bounds.size.y * 0.5f / spriteRenderer.bounds.size.y;
        transform.localScale = new Vector3(scale, scale, 1);
        // transform.rotation
        float zAngle = Random.Range(0f, 360f);
        transform.eulerAngles = (new Vector3(0f, 0f, zAngle));
    }

    // Update is called once per frame
    void Update()
    {
        // if child of hotAirBalloon:
        if (isChildOfHotAirBalloon)
        {
            // destroy after stun is over (if child of hotAirBalloon)
            time += Time.deltaTime;
            if (time > 4f)
            {
                Destroy(gameObject);
            }
            // audioSource
            audioSource.panStereo = transform.position.x / 10f;
            if (alreadyPlayedAudio == false && audioSource.isPlaying == false)
            {
                alreadyPlayedAudio = true;
                audioSource.Play();
            }
            // spriteRenderer.sortingLayer
            spriteRenderer.sortingLayerName = "HotAirBalloonLightning";
        }
        // transform.rotation
        float random = Random.Range(0f, 1f);
        if (random < 0.1f)
        {
            float zAngle = Random.Range(0f, 360f);
            transform.eulerAngles = (new Vector3(0f, 0f, zAngle));
        }
    }

    public void SetIsChildOfHotAirBalloon(bool value)
    {
        isChildOfHotAirBalloon = value;
    }
}
