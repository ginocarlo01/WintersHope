using EasyTransition;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    public Image[] images;
    public TextMeshProUGUI dialogueText;
    public float fadeInSpeed = 1.0f;
    public float textSpeed = 0.1f;
    public KeyCode restartKey = KeyCode.R;
    public KeyCode skipKey = KeyCode.E;

    private int currentIndex = 0;
    private bool isTransitioning = false;
    private Coroutine textCoroutine;

    [SerializeField]
    private string[] sceneTexts = {
        "Your text for scene 0",
        "Your text for scene 1",
        // Add more texts for additional scenes
    };

    [Header("Transition Settings")]
    [SerializeField]
    private string newLevelName;
    [SerializeField]
    private TransitionSettings transition;
    [SerializeField]
    private float startDelay;
    [SerializeField]
    string nextScene;

    void Start()
    {
        InitializeScene();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTransitioning)
        {
            if (currentIndex < images.Length - 1)
            {
                if (textCoroutine != null)
                {
                    // If the text coroutine is running, complete it immediately
                    StopCoroutine(textCoroutine);
                    dialogueText.text = sceneTexts[currentIndex];
                }

                // Transition to the next scene
                currentIndex++;
                InitializeScene(); // Initialize the next scene
            }
            else
            {
                // End of cutscene, you can handle it here.
                SkipCutscene();
                Debug.Log("End of cutscene");
            }
        }

        if (Input.GetKeyDown(restartKey))
        {
            RestartCutscene();
        }

        if (Input.GetKeyDown(skipKey))
        {
            SkipCutscene();
        }
    }

    void InitializeScene()
    {
        // Set alpha to 0 for the current image
        Color imageColor = images[currentIndex].color;
        imageColor.a = 0f;
        images[currentIndex].color = imageColor;

        // Show corresponding text immediately
        textCoroutine = StartCoroutine(TransitionImageAndText(sceneTexts[currentIndex]));
    }

    IEnumerator TransitionImageAndText(string text)
    {
        // Fade in the image
        while (images[currentIndex].color.a < 1f)
        {
            Color imageColor = images[currentIndex].color;
            imageColor.a += Time.deltaTime * fadeInSpeed;
            images[currentIndex].color = imageColor;
            yield return null;
        }

        // Show corresponding text
        dialogueText.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            dialogueText.text += text[i];
            yield return new WaitForSeconds(textSpeed);
        }

        // Set transitioning to false after the text is fully displayed
        isTransitioning = false;
    }

    void RestartCutscene()
    {
        
        TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, transition, startDelay);
    }

    void SkipCutscene()
    {
        TransitionManager.Instance().Transition(nextScene, transition, startDelay);
    }
}
