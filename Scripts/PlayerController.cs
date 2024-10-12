using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerController : MonoBehaviour
{
    private bool alreadyDestroyedThundercloudSpawner;
    private Animator animator;
    private GameObject asteroidSpawner;
    [SerializeField]
    private GameObject asteroidSpawnerPrefab;
    private AudioSource audioSource;
    private float audioSourcePitchOutfit1 = 1f;
    private float audioSourcePitchOutfit2 = 1.5f;
    private float audioSourcePitchOutfit3 = 2f;
    private float audioSourcePitchOutfit4 = 3f;
    private GameObject balloonCluster;
    private BalloonClusterController balloonClusterController;
    private SpriteRenderer balloonClusterSpriteRenderer;
    private BoxCollider2D boxCollider2D;
    private GameObject camera;
    private CameraController cameraController;
    private GameObject canvas;
    private GameObject cliff;
    private GameObject cliffBorder1;
    private GameObject cliffBorder2;
    [SerializeField]
    private GameObject cliffBorder2Prefab;
    private float decelerationTime = 0f;
    private bool falling = false;
    private GameObject finalCloud;
    private AudioSource finalCloudAudioSource;
    private GameObject finalCloudBorder;
    [SerializeField]
    private GameObject finalCloudBorderPrefab;
    private bool flying = false;
    private bool gameOverNotCalledYet = true;
    [SerializeField]
    private GameObject gameFinishedTextPrefab;
    [SerializeField]
    private GameObject gameOverTextPrefab;
    private GameObject hawkSpawner;
    [SerializeField]
    private GameObject hawkSpawnerPrefab;
    private float horizontalFlyingForceParameter = 1000f;
    private float horizontalFlyingForceParameterHeaven = 2000f;
    private GameObject hotAirBalloon;
    private Animator hotAirBalloonAnimator;
    private HotAirBalloonController hotAirBalloonController;
    private bool isGameOver = false;
    private bool isWifeLeft;
    private bool isWifeRight;
    private bool left = false;
    private GameObject leftBorder;
    private Mode mode = Mode.RunningOnMeadow;
    private bool passedHouse = false;
    private bool passedSpaceStationModule = false;
    private bool right = false;
    private GameObject rightBorder;
    private Rigidbody2D rigidbody2D;
    private GameObject rocket;
    private Animator rocketAnimator;
    private AudioSource rocketAudioSource;
    private RocketController rocketController;
    private float runningSpeed;
    private float runningSpeedOutfit1 = 4f;
    private float runningSpeedOutfit2 = 6f;
    private float runningSpeedOutfit3 = 8f;
    private float runningSpeedOutfit4 = 12f;
    private GameObject sky;
    private AudioSource skyAudioSource;
    private GameObject spaceStationPlatform;
    private GameObject spaceStationPlatformBorder1;
    private GameObject spaceStationPlatformBorder2;
    [SerializeField]
    private GameObject spaceStationPlatformBorder2Prefab;
    private SpriteRenderer spriteRenderer;
    private float stunTime = 0f;
    [SerializeField]
    private GameObject thundercloudSpawnerPrefab;
    private GameObject thundercloudSpawner;
    private GameObject timer;
    private TimerController timerController;
    private float verticalFlyingSpeed;
    private float verticalFlyingSpeedBalloonCluster = 4f;
    private float verticalFlyingSpeedHotAirBalloon = 8f;
    private float verticalFlyingSpeedRocket = 16f;
    private float verticalFlyingSpeedHeaven = 32f;
    private GameObject wife;
    private WifeController wifeController;
    private SpriteRenderer wifeSpriteRenderer;

    enum Mode
    {
        RunningOnMeadow,
        FlyingWithBalloonCluster,
        FallingTowardsCliff,
        RunningOnCliff,
        FlyingWithHotAirBalloon,
        FallingTowardsSpaceStationPlatform,
        RunningOnSpaceStationPlatform,
        FlyingWithRocket,
        FlyingInHeaven,
        FlyingInHeavenDeceleration,
        FallingTowardsFinalCloud,
        RunningOnFinalCloud,
        RunningWithWife
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        balloonCluster = GameObject.Find("BalloonCluster");
        balloonClusterController = balloonCluster.GetComponent<BalloonClusterController>();
        balloonClusterSpriteRenderer = balloonCluster.GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        camera = GameObject.Find("Camera");
        cameraController = camera.GetComponent<CameraController>();
        canvas = GameObject.Find("Canvas");
        cliff = GameObject.Find("Cliff");
        cliffBorder1 = GameObject.Find("CliffBorder1");
        finalCloud = GameObject.Find("FinalCloud");
        finalCloudAudioSource = finalCloud.GetComponent<AudioSource>();
        hotAirBalloon = GameObject.Find("HotAirBalloon");
        hotAirBalloonAnimator = hotAirBalloon.GetComponent<Animator>();
        hotAirBalloonController = hotAirBalloon.GetComponent<HotAirBalloonController>();
        leftBorder = GameObject.Find("LeftBorder");
        rightBorder = GameObject.Find("RightBorder");
        rigidbody2D = GetComponent<Rigidbody2D>();
        rocket = GameObject.Find("Rocket");
        rocketAnimator = rocket.GetComponent<Animator>();
        rocketAudioSource = rocket.GetComponent <AudioSource>();
        rocketController = rocket.GetComponent<RocketController>();
        runningSpeed = runningSpeedOutfit1;
        sky = GameObject.Find("Sky");
        skyAudioSource = sky.GetComponent<AudioSource>();
        spaceStationPlatform = GameObject.Find("SpaceStationPlatform");
        spaceStationPlatformBorder1 = GameObject.Find("SpaceStationPlatformBorder1");
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = GameObject.Find("Timer");
        timerController = timer.GetComponent<TimerController>();
        verticalFlyingSpeed = verticalFlyingSpeedBalloonCluster;
        wife = GameObject.Find("Wife");
        wifeController = wife.GetComponent<WifeController>();
        wifeSpriteRenderer = wife.GetComponent<SpriteRenderer>();
        // audioSource
        audioSource.loop = true;
        audioSource.pitch = audioSourcePitchOutfit1;
        audioSource.playOnAwake = false;
        // rigidbody2D
        rigidbody2D.drag = 2f;
        rigidbody2D.gravityScale = 1f;
        // spriteRenderer.sortingLayer
        // the order of the sorting layers is as follows:
        // [background], [platforms], Wife, Player2, [buildings], [obstacles], blackHole, [vehicles], Player1, [openingScene]
        // whereas
        // [background] = Sky, Galaxies, Stars, Sun, Moon, Clouds
        // [platforms] = Cliff, FinalCloud, Meadow, SpaceStationPlatform
        // [buildings] = House, SpaceStationModule
        // [obstacles] = Comets, Hawks, Thunderclouds, ThundercloudLightning
        // [vehicles] = BalloonCluster, HotAirBalloon, HotAirBallonLightning, Rocket
        // [openingScene] = SlideshowImages, SlideshowTransition
        // time:                                                                    sortingLayer change:
        // FlyingWithBalloonCluster -> FallingTowardsCliff                          Player1 -> Player2
        // FlyingWithHotAirBalloon -> FallingTowardsSpaceStationPlatform            Player2 -> Player1
        // FallingTowardsSpaceStationPlatform -> RunningOnSpaceStationPlatform      Player1 -> Player2
        // FlyingWithRocket -> FlyingInHeaven                                       Player2 -> Player1
        // FlyingInHeavenDeceleration -> FallingTowardsFinalCloud                   Player1 -> Player2
        spriteRenderer.sortingLayerName = "Player1";
        // transform.position
        transform.position = new Vector3(0f, 1.5f, 0f);
        // transform.localScale
        transform.localScale = new Vector3(1f, 1f, 1f);
        float scale = 1f / spriteRenderer.bounds.size.x;
        transform.localScale = new Vector3(scale, scale, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // destroy when out of screen
        if (mode == Mode.RunningWithWife && (transform.position.x < -40f || transform.position.x > 40f))
        {
            Destroy(gameObject);
        }
        // destroy when out of world
        if (transform.position.y < -100f)
        {
            Destroy(gameObject);
        }
        // audioSource.panStereo
        audioSource.panStereo = Math.Min(1f, Math.Max(-1f, transform.position.x / 10f));
        // ControlInput
        ControlInput();
        // isWifeLeft/isWifeRight
        if (mode != Mode.RunningWithWife)
        {
            if (wife.transform.position.x < transform.position.x)
            {
                isWifeLeft = true;
                isWifeRight = false;
            }
            else
            {
                isWifeLeft = false;
                isWifeRight = true;
            }
        }
        // animator
        animator.SetBool("falling", falling);
        animator.SetBool("flying", flying);
        animator.SetBool("isWifeLeft", isWifeLeft);
        animator.SetBool("isWifeRight", isWifeRight);
        animator.SetBool("left", left);
        animator.SetBool("passedHouse", passedHouse);
        animator.SetBool("passedSpaceStationModule", passedSpaceStationModule);
        animator.SetBool("right", right);
        // set transform.rotation to the standard value so that the Rigidbody2D component does not intefere (e.g. when the player hits cliffBorder1)
        transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
        // RunningOnMeadow
        if (mode == Mode.RunningOnMeadow)
        {
            RunningOnMeadow();
        }
        // FlyingWithBalloonCluster
        else if (mode == Mode.FlyingWithBalloonCluster)
        {
            FlyingWithBalloonCluster();
        }
        // FallingTowardsCliff is handled by physics,
        // FallingTowardsCliff -> RunningOnCliff is handled by OnCollsionEnter2D
        // RunningOnClif
        else if (mode == Mode.RunningOnCliff)
        {
            RunningOnCliff();
        }
        // FlyingWithHotAirBalloon
        else if (mode == Mode.FlyingWithHotAirBalloon)
        {
            FlyingWithHotAirBalloon();
        }
        // FallingTowardsSpaceStationPlatform is handled by physics,
        // FallingTowardsSpaceStationPlatform -> RunningOnSpaceStationPlatform is handled by OnCollsionEnter2D
        // RuningOnSpaceStationPlatform
        else if (mode == Mode.RunningOnSpaceStationPlatform)
        {
            RunningOnSpaceStationPlatform();
        }
        // FlyingWithRocket
        else if (mode == Mode.FlyingWithRocket)
        {
            FlyingWithRocket();
        }
        // FlyingInHeaven
        else if (mode == Mode.FlyingInHeaven)
        {
            FlyingInHeaven();
        }
        // FlyingInHeavenDeceleration
        else if (mode == Mode.FlyingInHeavenDeceleration)
        {
            FlyingInHeavenDeceleration();
        }
        // FallingTowardsFinalCloud is handled by physics,
        // FallingTowardsFinalCloud -> RunningOnFinalCloud is handled by OnCollisionEnter2D
        // RunningOnFinalCloud
        else if (mode == Mode.RunningOnFinalCloud)
        {
            RunningOnFinalCloud();
        }
        // RunningWithWife
        else if (mode == Mode.RunningWithWife)
        {
            RunningWithWife();
        }
    }

    void ControlInput()
    {
        // leftDown
        if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            left = true;
        }
        // leftUp
        if (Input.GetKeyUp("a") || Input.GetKeyUp("left"))
        {
            left = false;
        }
        // rightDown
        if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            right = true;
        }
        // rightUp
        if (Input.GetKeyUp("d") || Input.GetKeyUp("right"))
        {
            right = false;
        }
    }

    public void DecrementVerticalFlyingSpeed(float value)
    {
        verticalFlyingSpeed -= value;
    }

    void FlyingInHeaven()
    {
        // horizontal movement
        float horizontalForce = Input.GetAxis("Horizontal") * horizontalFlyingForceParameterHeaven * Time.deltaTime;
        rigidbody2D.AddForce(new Vector2(horizontalForce, 0f));
        // vertical movement
        float verticalDistance = verticalFlyingSpeed * Time.deltaTime;
        transform.Translate(new Vector3(0f, verticalDistance, 0f));
        // FlyingInHeaven -> FlyingInHeavenDeceleration
        // if the deceleration is the same as verticalFlyingSpeed at the moment of the transition to deceleration,
        // so that then it takes exactly one second for verticalFlyingSpeed to become zero, the covered distance in this second is:
        // -0.5f * verticalFlyingSpeed + verticalFlyingSpeed
        if (transform.position.y > 1100f - (-0.5f * verticalFlyingSpeed + verticalFlyingSpeed))
        {
            mode = Mode.FlyingInHeavenDeceleration;
        }
    }

    void FlyingInHeavenDeceleration()
    {
        // horizontal movement
        float horizontalForce = Input.GetAxis("Horizontal") * horizontalFlyingForceParameterHeaven * Time.deltaTime;
        rigidbody2D.AddForce(new Vector2(horizontalForce, 0f));
        // vertical movement (deceleration)
        decelerationTime += Time.deltaTime;
        float yVelocity = verticalFlyingSpeed - verticalFlyingSpeed * decelerationTime;
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, yVelocity);
        // FlyingInHeavenDeceleration -> FallingTowardsFinalCloud
        if (rigidbody2D.velocity.y < 0f)
        {
            falling = true;
            finalCloudBorder = Instantiate(finalCloudBorderPrefab, finalCloud.transform) as GameObject;
            flying = false;
            mode = Mode.FallingTowardsFinalCloud;
            rigidbody2D.gravityScale = 1f;
            spriteRenderer.sortingLayerName = "Player2";
        }
    }

    void FlyingWithBalloonCluster()
    {
        if (isGameOver)
        {
            if (gameOverNotCalledYet)
            {
                GameOver();
                gameOverNotCalledYet = false;
            }
            Destroy(hawkSpawner);
            verticalFlyingSpeed = -1f;
        }
        // horizontal movement
        float horizontalForce = Input.GetAxis("Horizontal") * horizontalFlyingForceParameter * Time.deltaTime;
        rigidbody2D.AddForce(new Vector2(horizontalForce, 0f));
        // vertical movement
        float verticalDistance = verticalFlyingSpeed * Time.deltaTime;
        transform.Translate(new Vector3(0f, verticalDistance, 0f));
        // FlyingWithBalloonCluster -> FallingTowardsCliff
        if (transform.position.y >= cliff.transform.position.y + 8f)
        {
            balloonCluster.transform.parent = null;
            balloonClusterController.SetVerticalFlyingSpeed(verticalFlyingSpeed * 2f);
            cliffBorder2 = Instantiate(cliffBorder2Prefab, cliff.transform) as GameObject;
            Destroy(hawkSpawner);
            falling = true;
            flying = false;
            mode = Mode.FallingTowardsCliff;
            rigidbody2D.gravityScale = 1f;
            spriteRenderer.sortingLayerName = "Player2";
        }
    }

    void FlyingWithHotAirBalloon()
    {
        // not stunned
        if (stunTime == 0f)
        {
            // verticalFlyingSpeed
            verticalFlyingSpeed = verticalFlyingSpeedHotAirBalloon;
            // horizontal movement
            float horizontalForce = Input.GetAxis("Horizontal") * horizontalFlyingForceParameter * Time.deltaTime;
            rigidbody2D.AddForce(new Vector2(horizontalForce, 0f));
            // vertical movement
            float verticalDistance = verticalFlyingSpeed * Time.deltaTime;
            transform.Translate(new Vector3(0f, verticalDistance, 0f));
        }
        // stunned
        else
        {
            // update stunTime
            stunTime = Math.Max(0f, stunTime - Time.deltaTime);
            // verticalFlyingSpeed
            verticalFlyingSpeed = 1f;
            // no horizontal movement
            // vertical movement
            float verticalDistance = verticalFlyingSpeed * Time.deltaTime;
            transform.Translate(new Vector3(0f, verticalDistance, 0f));
        }
        // destroy thunderCloudSpawner at y = 260
        if (alreadyDestroyedThundercloudSpawner == false && transform.position.y > 260)
        {
            Destroy(thundercloudSpawner);
        }
        // FlyingWithHotAirBalloon -> FallingTowardsSpaceStationPlatform
        if (transform.position.y >= spaceStationPlatform.transform.position.y + 4f)
        {
            hotAirBalloon.transform.parent = null;
            hotAirBalloonAnimator.SetBool("isPlayerInside", false);
            hotAirBalloonController.SetVerticalFlyingSpeed(verticalFlyingSpeed * 1.25f);
            spaceStationPlatformBorder2 = Instantiate(spaceStationPlatformBorder2Prefab, spaceStationPlatform.transform) as GameObject;
            falling = true;
            flying = false;
            mode = Mode.FallingTowardsSpaceStationPlatform;
            rigidbody2D.gravityScale = 1f;
            skyAudioSource.Stop();
            spriteRenderer.enabled = true;
            spriteRenderer.sortingLayerName = "Player1";
        }
    }

    void FlyingWithRocket()
    {
        if (isGameOver)
        {
            if (gameOverNotCalledYet)
            {
                GameOver();
                gameOverNotCalledYet = false;
            }
            Destroy(asteroidSpawner);
        }
        else
        {
            // horizontal movement
            float horizontalForce = Input.GetAxis("Horizontal") * horizontalFlyingForceParameter * Time.deltaTime;
            rigidbody2D.AddForce(new Vector2(horizontalForce, 0f));
            // vertical movement
            float verticalDistance = verticalFlyingSpeed * Time.deltaTime;
            transform.Translate(new Vector3(0f, verticalDistance, 0f));
            // FlyingWithRocket -> FlyingInHeaven
            if (transform.position.y > 700f)
            {
                audioSource.pitch = audioSourcePitchOutfit4;
                Destroy(asteroidSpawner);
                finalCloudAudioSource.Play();
                mode = Mode.FlyingInHeaven;
                rigidbody2D.drag = 1f;
                rocket.transform.parent = null;
                rocketAudioSource.Stop();
                rocketController.SetVerticalFlyingSpeed(verticalFlyingSpeedHeaven * 0.75f);
                runningSpeed = runningSpeedOutfit4;
                spriteRenderer.enabled = true;
                spriteRenderer.sortingLayerName = "Player1";
                verticalFlyingSpeed = verticalFlyingSpeedHeaven;
            }
        }
    }

    void GameOver()
    {
        cameraController.SetIsGameOver(true);
        Destroy(leftBorder);
        Destroy(rightBorder);
        GameObject gameOverText = Instantiate(gameOverTextPrefab, canvas.transform) as GameObject;
        timerController.SetIsGameOver(true);
        wifeController.SetIsGameOver(true);
    }

    public float GetVerticalFlyingSpeed()
    {
        return verticalFlyingSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // FallingTowardsCliff -> RunningOnCliff
        if (mode == Mode.FallingTowardsCliff && collision.gameObject == cliffBorder2)
        {
            Destroy(cliffBorder1);
            falling = false;
            mode = Mode.RunningOnCliff;
        }
        // FallingTowardsSpaceStationPlatform -> RunningOnSpaceStationPlatform
        if (mode == Mode.FallingTowardsSpaceStationPlatform && collision.gameObject == spaceStationPlatformBorder2)
        {
            Destroy(spaceStationPlatformBorder1);
            falling = false;
            mode = Mode.RunningOnSpaceStationPlatform;
            spriteRenderer.sortingLayerName = "Player2";
        }
        // FallingTowardsFinalCloud -> RunningOnFinalCloud
        if (mode == Mode.FallingTowardsFinalCloud && collision.gameObject == finalCloudBorder)
        {
            falling = false;
            mode = Mode.RunningOnFinalCloud;
        }
    }

    void Running()
    {
        // movement
        float horizontalDistance = Input.GetAxisRaw("Horizontal") * runningSpeed * Time.deltaTime;
        Vector3 movement = new Vector3(horizontalDistance, 0f, 0f);
        if (transform.position.x > -10f && transform.position.x < 10f)
        {
            transform.Translate(movement);
        }
        // audio
        if ((left == true && right == false) || (left == false && right == true))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void RunningOnCliff()
    {
        Running();
        // Outftit 1 -> Outfit2
        if (passedHouse == false && transform.position.x <= 0.3f)
        {
            audioSource.pitch = audioSourcePitchOutfit2;
            passedHouse = true;
            runningSpeed = runningSpeedOutfit2;
        }
        // RunningOnCliff -> FlyingWithHotAirBalloon
        if (transform.position.x <= hotAirBalloon.transform.position.x)
        {
            audioSource.Stop();
            flying = true;
            hotAirBalloonAnimator.SetBool("isPlayerInside", true);
            hotAirBalloon.transform.parent = transform;
            mode = Mode.FlyingWithHotAirBalloon;
            rigidbody2D.gravityScale = 0f;
            skyAudioSource.Play();
            spriteRenderer.enabled = false;
            thundercloudSpawner = Instantiate(thundercloudSpawnerPrefab) as GameObject;
            verticalFlyingSpeed = verticalFlyingSpeedHotAirBalloon;
        }
    }

    void RunningOnFinalCloud()
    {
        Running();
        // RunningOnFinalCloud -> RunningWithWife
        if (Math.Abs(transform.position.x - wife.transform.position.x) < spriteRenderer.bounds.size.x / 2f + wifeSpriteRenderer.bounds.size.x / 2f)
        {
            audioSource.pitch = audioSourcePitchOutfit1;
            audioSource.Play();
            animator.SetBool("runningWithWife", true);
            cameraController.SetIsGameFinished(true);
            Destroy(leftBorder);
            Destroy(rightBorder);
            GameObject gameFinishedText = Instantiate(gameFinishedTextPrefab, canvas.transform) as GameObject;
            mode = Mode.RunningWithWife;
            runningSpeed = runningSpeedOutfit1;
            timerController.SetIsGameFinishied(true);
            wifeController.SetRunningWithPlayer(true);
        }
    }

    void RunningOnMeadow()
    {
        Running();
        // RunningOnMeadow -> FlyingWithBallonCluster
        if (transform.position.x >= balloonCluster.transform.position.x)
        {
            audioSource.Stop();
            balloonCluster.transform.parent = transform;
            balloonCluster.transform.localPosition = new Vector3(0f, 15.2f, 0f);
            flying = true;
            hawkSpawner = Instantiate(hawkSpawnerPrefab) as GameObject;
            mode = Mode.FlyingWithBalloonCluster;
            rigidbody2D.gravityScale = 0f;
            verticalFlyingSpeed = verticalFlyingSpeedBalloonCluster;
        }
    }

    void RunningOnSpaceStationPlatform()
    {
        Running();
        // Outfit2 -> Outfit3
        if (passedSpaceStationModule == false && transform.position.x >= -1.8f)
        {
            audioSource.pitch = audioSourcePitchOutfit3;
            passedSpaceStationModule = true;
            runningSpeed = runningSpeedOutfit3;
        }
        // RunningOnSpaceStation -> FlyingWithRocket
        if (transform.position.x >= rocket.transform.position.x)
        {
            asteroidSpawner = Instantiate(asteroidSpawnerPrefab) as GameObject;
            audioSource.Stop();
            flying = true;
            mode = Mode.FlyingWithRocket;
            rigidbody2D.drag = 0f;
            rigidbody2D.gravityScale = 0f;
            rocketAnimator.SetBool("isPlayerInside", true);
            rocketAudioSource.Play();
            rocket.transform.parent = transform;
            spriteRenderer.enabled = false;
            verticalFlyingSpeed = verticalFlyingSpeedRocket;
        }
    }

    void RunningWithWife()
    {
        // movement
        float horizontalDistance;
        if (isWifeLeft)
        {
            horizontalDistance = -1f * runningSpeed * Time.deltaTime;
            // audioSource.volume (reduce volume to zero between x = -10 and x = -30)
            audioSource.volume = Math.Min(1f, Math.Max(0f, 1f - (-transform.position.x - 10f) / 20f));
        }
        else
        {
            horizontalDistance = runningSpeed * Time.deltaTime;
            // audioSource.volume (reduce volume to zero between x = 10 and x = 30)
            audioSource.volume = Math.Min(1f, Math.Max(0f, 1f - (transform.position.x - 10f) / 20f));
        }
        Vector3 movement = new Vector3(horizontalDistance, 0f, 0f);
        transform.Translate(movement);

    }

    public void SetIsGameOver(bool value)
    {
        isGameOver = value;
    }

    public void SetStunTime(float value)
    {
        stunTime = value;
    }
}