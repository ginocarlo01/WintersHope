using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUiDisplay : MonoBehaviour
{
    public DialogueManager dialogueManager;
    [SerializeField]
    private float displayTextDelay;
    [SerializeField]
    private char[] splited;
    [SerializeField]
    private GameObject UiObject;
    [SerializeField]
    private TextMeshProUGUI displayText, nameText;
    [SerializeField]
    private Button nextButton, previousButton, endDialogueButton;
    [SerializeField]
    private Image characterLeft, characterRight;
    [SerializeField]
    private Image expressionCharacterLeft, expressionCharacterRight;
    [SerializeField]
    private Image backGroundImage;
    [SerializeField]
    private GameObject choicebuttons;
    [SerializeField]
    private Button goodChoiceButton, badChoiceButton;
    private void Awake()
    {
        UiObject.SetActive(false);
        endDialogueButton.onClick.AddListener(EndDialogue);
    }
    void Update()
    {
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.RightArrow))
            NextButton();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            PreviousButton();
#endif
        /*if (Input.GetButtonDown("VERDE0") && GameManager.instace.GetState() == GameManager.GameState.interaction)
        {
            if (dialogueManager.GetHasChoice() && !dialogueManager.IsRootDialogue())
            {
                AddAfinity();
                NextButton();
            }
            else if (dialogueManager.IsRootDialogue() && dialogueManager.GetHasChoice())
                NextButton();
            else
                NextButton();

            //if (dialogueManager.IsRootDialogue() && dialogueManager.GetHasChoice())
            // NextButton();

        }

        if (Input.GetButtonDown("VERMELHO0") && GameManager.instace.GetState() == GameManager.GameState.interaction)
        {
            PreviousButton();
        }

        if (Input.GetButtonDown("AZUL0") && GameManager.instace.GetState() == GameManager.GameState.interaction)
        {
            if (dialogueManager.GetHasChoice() && !dialogueManager.IsRootDialogue() && !(dialogueManager.GetChoiced()))
            {
                RemoveAfinity();
                NextButton();
            }
        }
        if (Input.GetButtonDown("PRETO0") && GameManager.instace.GetState() == GameManager.GameState.interaction || GameManager.instace.GetState() == GameManager.GameState.cutscene)
        {
            if (endDialogueButton.isActiveAndEnabled)
                EndDialogue();
        }*/
    }


    public void SetThings()
    {
        if (dialogueManager.isThisDialogueACutScene())
        {
            endDialogueButton.gameObject.SetActive(false);
        }
        else
        {
            endDialogueButton.gameObject.SetActive(true);
        }

        if (dialogueManager.GetDialogueEnded())
        {
            StopAllCoroutines();
            SetCharacterName();
            UiObject.SetActive(true);
            SetCharacterImage();
            //SetExpression();
            SetColor();
            SetText(dialogueManager.GetTextEnded());

            nextButton.gameObject.SetActive(false);
            previousButton.gameObject.SetActive(false);
            goodChoiceButton.gameObject.SetActive(false);
            badChoiceButton.gameObject.SetActive(false);

            StartCoroutine(DisplayText());
        }
        else
        {
            StopAllCoroutines();
            SetCharacterName();
            UiObject.SetActive(true);
            SetCharacterImage();
            //SetExpression();
            SetColor();
            SetText(dialogueManager.GetText());
            SetBackgroundImage();
            nextButton.onClick.RemoveAllListeners();
            previousButton.onClick.RemoveAllListeners();
            goodChoiceButton.onClick.RemoveAllListeners();
            badChoiceButton.onClick.RemoveAllListeners();

            nextButton.onClick.AddListener(NextButton);
            previousButton.onClick.AddListener(PreviousButton);
            goodChoiceButton.onClick.AddListener(GoodChoice);
           // goodChoiceButton.onClick.AddListener(NextButton);
            badChoiceButton.onClick.AddListener(BadChoice);
           // badChoiceButton.onClick.AddListener(NextButton);

            nextButton.gameObject.SetActive(true);
            previousButton.gameObject.SetActive(true);

            ChoiceOption();



            StartCoroutine(DisplayText());
        }
    }

    void SetText(string text)
    {
        displayText.text = "";
        //if (dialogueManager.GetDialogueEnded()) {

        if (splited.Length > 0)
        {
            splited = new char[0];
            splited = text.ToCharArray();//dialogueManager.GetTextEnded().ToCharArray();
        }
        else
            splited = text.ToCharArray();//dialogueManager.GetTextEnded().ToCharArray();
                                         //}
                                         //else
                                         //{


        //if (splited.Length > 0)
        // {
        // splited = new char[0];
        // splited = dialogueManager.GetText().ToCharArray();
        //}
        // else
        // splited = dialogueManager.GetText().ToCharArray();
        //}
    }

    void SetColor()
    {
        //if (!dialogueManager.IsRootDialogue())
        //{
        if (characterLeft != null)
            characterLeft.color = dialogueManager.GetColor(0);
        if (characterRight != null)
            characterRight.color = dialogueManager.GetColor(1);
        //}
        // else
        //{
        //   if (characterLeft != null)
        //  characterLeft.color = dialogueManager.GetColor(0);
        //  if (characterRight != null)
        // characterRight.color = dialogueManager.GetColor(1);
        //}



    }
    void SetBackgroundImage()
    {
        if (dialogueManager.isThisDialogueACutScene())
        {
            if (backGroundImage != null)
            {
                backGroundImage.gameObject.SetActive(true);
                backGroundImage.sprite = dialogueManager.GetBackground();
            }
        }
        else
            backGroundImage.gameObject.SetActive(false);
    }
    void SetCharacterImage()
    {
        if (characterLeft != null)
            characterLeft.sprite = dialogueManager.GetCharacterSprite1();
        if (characterRight != null)
            characterRight.sprite = dialogueManager.GetCharacterSprite2();
    }

    void SetExpression()
    {
        if (expressionCharacterLeft != null)
            expressionCharacterLeft.sprite = dialogueManager.GetExpression(0);
        if (expressionCharacterRight != null)
            expressionCharacterRight.sprite = dialogueManager.GetExpression(1);
        //mooooooooooooooooooooooooooooooooooooreee
        //amogus
    }

    void SetCharacterName()
    {
        nameText.text = dialogueManager.GetNameText();
    }
    void NextButton()
    {
        /*if (dialogueManager.GetHasChoice())
        {
            
        }*/

        dialogueManager.NextText();
        SetCharacterName();
        ChoiceOption();
        StopAllCoroutines();
        SetCharacterImage();
        SetExpression();
        SetCharacterImage();
        SetBackgroundImage();
        SetColor();
        SetText(dialogueManager.GetText());
        StartCoroutine(DisplayText());

    }

    void PreviousButton()
    {

        dialogueManager.PreviousText();
        SetCharacterName();
        ChoiceOption();
        StopAllCoroutines();
        SetCharacterImage();
        SetExpression();
        SetCharacterImage();
        SetBackgroundImage();
        SetColor();
        SetText(dialogueManager.GetText());
        StartCoroutine(DisplayText());
    }

    void AddAfinity()
    {
        dialogueManager.AddAfinity();
        dialogueManager.SetChoiceRoot(true, true);
    }
    void RemoveAfinity()
    {
        dialogueManager.RemoveAfinity();
        dialogueManager.SetChoiceRoot(false, false);
    }

    public void EndDialogue()
    {
        //GameManager.instace.SetState(GameManager.GameState.play);
        dialogueManager = null;
        StopAllCoroutines();
        UiObject.SetActive(false);
    }

    void ChoiceOption()
    {
        if (dialogueManager.GetHasChoice())
        {

            choicebuttons.SetActive(true);
            goodChoiceButton.GetComponentInChildren<TextMeshProUGUI>().text = dialogueManager.GetGoodChoice();
            badChoiceButton.GetComponentInChildren<TextMeshProUGUI>().text = dialogueManager.GetBadChoice();
            nextButton.gameObject.SetActive(false);
            if (dialogueManager.GetChoiced())
            {
                dialogueManager.SetEnterChoiceRoot();
                goodChoiceButton.interactable = false;
                badChoiceButton.interactable = false;
                nextButton.gameObject.SetActive(true);
            }
            //previousButton.gameObject.SetActive(false);
        }
        else
        {
            choicebuttons.SetActive(false);
            nextButton.gameObject.SetActive(true);
            previousButton.gameObject.SetActive(true);
        }

        if (dialogueManager.IsRootDialogue())
        {
            choicebuttons.SetActive(false);
            nextButton.gameObject.SetActive(true);
            previousButton.gameObject.SetActive(true);
        }
        /*if (dialogueManager.isThisDialogueACutScene())
        {
            if (dialogueManager.GetIndex() == dialogueManager.GetLength())
            {
                endDialogueButton.gameObject.SetActive(true);
                choicebuttons.SetActive(false);
                nextButton.gameObject.SetActive(false);
                previousButton.gameObject.SetActive(false);
            }
            else
            {

            }
        }*/
    }


    IEnumerator DisplayText()
    {
        int index = 0;
        float startTime = Time.realtimeSinceStartup;

        while (index < splited.Length)
        {
            if (Time.realtimeSinceStartup - startTime >= displayTextDelay)
            {
                displayText.text += splited[index];
                index++;
                startTime = Time.realtimeSinceStartup;
            }

            yield return null;
        }
    }


    void BadChoice()
    {
        RemoveAfinity();
        NextButton();
    }

    void GoodChoice()
    {
        AddAfinity();
        NextButton();
    }
}

