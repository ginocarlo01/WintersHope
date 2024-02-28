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

    [SerializeField]
    private string[] sceneTexts = {
        "Your text for scene 0",
        "Your text for scene 1",
        // Add more texts for additional scenes
    };

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
                // Transition to the next scene
                currentIndex++;
                InitializeScene(); // Initialize the next scene
            }
            else
            {
                // End of cutscene, you can handle it here.
                Debug.Log("End of cutscene");
            }
        }

        if (Input.GetKeyDown(restartKey))
        {
            RestartCutscene();
        }

        if (Input.GetKeyDown(skipKey))
        {
            // Skip the entire cutscene, you can handle it here.
            Debug.Log("Cutscene skipped");
        }
    }

    void InitializeScene()
    {
        // Set alpha to 0 for the current image
        Color imageColor = images[currentIndex].color;
        imageColor.a = 0f;
        images[currentIndex].color = imageColor;

        // Show corresponding text immediately
        StartCoroutine(TransitionImageAndText(sceneTexts[currentIndex]));
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
        StartCoroutine(AppendText(text));
    }

    IEnumerator AppendText(string text)
    {
        dialogueText.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            dialogueText.text += text[i];
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void RestartCutscene()
    {
        // Reset variables or perform any necessary setup to restart the cutscene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
