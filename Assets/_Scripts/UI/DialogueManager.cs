using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private string dialogueToLoad;
    [SerializeField]
    private string afinityToLoad;
    [SerializeField]
    private DialogueContainer dialogueData;
    [SerializeField]
    private AfinityContainer afinityData;
    [SerializeField]
    [TextArea]
    private string dialogueText;
    private string dialogueEndedText;
    [SerializeField]
    private int index;
    [SerializeField]
    private int length;

    [SerializeField]
    private DialogueUiDisplay display;

    //bool thisDialogueHasChoice
    bool choiceRootDialogue = false;
    int rootIndex;
    public int rootLength;
    bool _goodOrBadRoot;

    public static Action endPlayerInteractAction;

    void Awake()
    {
        SetAfinityAndDialogueData(dialogueToLoad, afinityToLoad);

        display = FindObjectOfType<DialogueUiDisplay>();
        index = 0;
    }

    private void Start()
    {
        //index = 0;
        //NextText();
        SetText(index, choiceRootDialogue, 0, false);
    }
    public int GetIndex()
    {
        return index;
    }
    public int GetLength()
    {
        return length;
    }
    public
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        /*if (Input.GetKeyDown(KeyCode.Space))
            StartDialogue();*/

        /*if (Input.GetKeyDown(KeyCode.RightArrow))
            NextText();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            PreviousText();*/
#endif
    }

    void SetText(int value, bool choiceRootDialogue, int rootValue, bool goodOrBadChoice)
    {
        if (!choiceRootDialogue)
            dialogueText = dialogueData.GetText(value);
        else
            dialogueText = dialogueData.GetRootText(value, rootValue, goodOrBadChoice);
    }


    public void NextText()
    {

        if (!choiceRootDialogue)
        {
            index++;
            // Debug.Log(index);


            if (index == length)
            {

               //Debug.Log("dialogu ended");
                index = 0;
                display.EndDialogue();
                endPlayerInteractAction?.Invoke();

            }
            if (index > length)
            {
                index = length;
                if (isThisDialogueACutScene())
                {
                    EndCutScene();
                }

            }
            SetText(index, choiceRootDialogue, 0, false);
            //index++;
        }
        else
        {
            Debug.Log(index);
            Debug.Log(rootIndex);
            if (rootIndex == rootLength)
            {
                rootIndex = rootLength;
                choiceRootDialogue = false;
                Debug.Log(rootLength);
                NextText();
                //index++;
                //return;
            }
            /*if (rootIndex == rootLength)
                choiceRootDialogue = false;*/

            if (choiceRootDialogue)
            {
                /*if (dialogueData.GetChoiceChoosed(index, true))
                    SetText(index, choiceRootDialogue, rootIndex, true);
                else
                    SetText(index, choiceRootDialogue, rootIndex, _goodOrBadRoot);
                if (dialogueData.GetChoiceChoosed(index, false))
                    SetText(index, choiceRootDialogue, rootIndex, false);
                else
                   SetText(index, choiceRootDialogue, rootIndex, _goodOrBadRoot);*/
                //if (!GetChoiced())
                //{
                    if (dialogueData.GetChoiceChoosed(index, true))
                        SetText(index, choiceRootDialogue, rootIndex, true);
                    if (dialogueData.GetChoiceChoosed(index, false))
                        SetText(index, choiceRootDialogue, rootIndex, false);
                    rootIndex++;
                //}
                //else
                //{
                  //  if (dialogueData.GetChoiceChoosed(index, true))
                       // SetText(index, choiceRootDialogue, rootIndex, true);
                   // if (dialogueData.GetChoiceChoosed(index, false))
                   //     SetText(index, choiceRootDialogue, rootIndex, false);
                   // rootIndex++;
                //}
            }

        }

        /*if (!choiceRootDialogue)
            SetText(index, false, 0, false);
        else
        {

            Debug.Log(rootIndex);
            SetText(rootIndex, true, rootIndex, _goodOrBadRoot);
        }*/

    }

    public void PreviousText()
    {
        //index--;

        //if (index < 0)
        //    index = 0;

        //SetText(index, choiceRootDialogue, 0, false);

        if (!choiceRootDialogue)
        {
            //index--;
            // Debug.Log(index);

            /*if (index == length)
            {
#if !UNITY_EDITOR
          //  dialogueData.SetDialogueEnd(true);
#else
                Debug.Log("dialogu ended");
#endif
            }*/
            index--;
            //SetText(index, choiceRootDialogue, 0, false);
            if (index < 0)
            {
                index = 0;
                /*if (isThisDialogueACutScene())
                {
                    //EndCutScene();
                }*/
                //SetText(index, choiceRootDialogue, 0, false);
            }

            SetText(index, choiceRootDialogue, 0, false);
        }
        else
        {
            rootIndex--;
            Debug.Log(index);
            Debug.Log(rootIndex);
            if (rootIndex <= 0)
            {
                rootIndex = 0;
                //rootIndex = rootLength;
                choiceRootDialogue = false;
                //Debug.Log(rootLength);
                PreviousText();
                //SetText(index, choiceRootDialogue, 0, false);
                //index++;
                //return;
            }
            /*if (rootIndex == rootLength)
                choiceRootDialogue = false;*/

            if (choiceRootDialogue)
            {
                if (dialogueData.GetChoiceChoosed(index, true))
                    SetText(index, choiceRootDialogue, rootIndex, true);
                if (dialogueData.GetChoiceChoosed(index, false))
                    SetText(index, choiceRootDialogue, rootIndex, false);
                rootIndex--;
            }

        }

        /*if (!choiceRootDialogue)
            SetText(index, false, 0, false);
        else
        {

            Debug.Log(rootIndex);
            SetText(rootIndex, true, rootIndex, _goodOrBadRoot);
        }*/
    }
    public Color GetColor(int colorIndex)
    {
        return dialogueData.GetColor(index, colorIndex);
    }

    public Color GetRootColor(int colorIndex)
    {
        return dialogueData.GetRootColor(index, rootIndex, _goodOrBadRoot, colorIndex);
    }

    public Sprite GetExpression(int expressionIndex)
    {
        return dialogueData.GetExpression(index, expressionIndex);
    }

    public Sprite GetCharacterSprite1()
    {
        //return dialogueData.GetCharacterSprite1();
        return dialogueData.GetExpression(index, 0);
    }

    public Sprite GetCharacterSprite2()
    {
        //return dialogueData.GetCharacterSprite2();
        return dialogueData.GetExpression(index, 1);
    }

    public Sprite GetBackground()
    {
        return dialogueData.GetBakgroundImage(index);
    }
    public bool GetHasChoice()
    {
        return dialogueData.GetHasChoice(index);
    }

    public bool GetChoiced()
    {
        return dialogueData.GetChoiced(index);
    }

    public string GetGoodChoice()
    {
        return dialogueData.GetGoodChoice(index);
    }
    public string GetBadChoice()
    {
        return dialogueData.GetBadChoice(index);
    }
    public string GetText()
    {
        return dialogueText;
    }

    public void StartDialogue()
    {

        display.dialogueManager = this;
        //Debug.Log(index);
        display.SetThings();
        //GameManager.instace.SetState(GameManager.GameState.interaction);
    }
    public bool GetDialogueEnded()
    {
        return dialogueData.GetDialogueEnded();
    }
    public string GetTextEnded()
    {
        return dialogueData.GetTextEnded();
    }
    public void AddAfinity()
    {
//#if UNITY_EDITOR
  //      Debug.Log("AfinityAdded");
    //    afinityData.IncreaseAfinity(dialogueData.GetAfinityToAdd(index));
      //  return;
//#else
        //if (!dialogueData.GetChoiced(index))
        //{
            //dialogueData.SetChoiced(index, true);
            afinityData.IncreaseAfinity(dialogueData.GetAfinityToAdd(index));
          //  Debug.Log("AfinityIncreased");
        //}
//#endif

    }
    public void RemoveAfinity()
    {
//#if UNITY_EDITOR
  //      Debug.Log("AfinityRemoved");
    //    afinityData.DecreaseAfinity(dialogueData.GetAfinityToRemove(index));
      //  return;
//#else
        //if (!dialogueData.GetChoiced(index))
        //{
            //dialogueData.SetChoiced(index, true);
            afinityData.DecreaseAfinity(dialogueData.GetAfinityToRemove(index));
            Debug.Log("AfinityDecreased");
        //}
//#endif
    }

    public void SetChoiceRoot(bool value, bool bOrG)
    {
        if (!dialogueData.GetChoiced(index))
        {
            choiceRootDialogue = true;
            _goodOrBadRoot = value;
            Debug.Log(_goodOrBadRoot);
            rootIndex = 0;
            rootLength = dialogueData.GetRootlength(index, _goodOrBadRoot);
            if (bOrG)
                dialogueData.SetGoodChoiced(index, true);
            else
                dialogueData.SetBadChoiced(index, true);

            dialogueData.SetChoiced(index, true);
        }
        else
        {
            choiceRootDialogue = true;
            rootIndex = 0;
            if (dialogueData.GetChoiceChoosed(index, bOrG))
            {
                rootLength = dialogueData.GetRootlength(index, true);
            }
            else
                rootLength = dialogueData.GetRootlength(index, false);

            if (!dialogueData.GetChoiceChoosed(index, bOrG))
            {
                rootLength = dialogueData.GetRootlength(index, false);
            }
            else
                rootLength = dialogueData.GetRootlength(index, true);
        }
    }
    public void SetEnterChoiceRoot()
    {
        choiceRootDialogue = true;
    }
    public bool IsRootDialogue()
    {
        return choiceRootDialogue;
    }

    public bool isThisDialogueACutScene()
    {
        return dialogueData.GetIsThisDaiologueACusscene();
    }

    public void EndCutScene()
    {
        //GameManager.instace.OnEndCutScene();
        //CutsceneManager.instance.OnEndCutScene();
    }

    public void SetAfinityAndDialogueData(string dialogueDataName, string afinityDataName)
    {
        var data = Resources.Load<DialogueContainer>("Dialogues/" + dialogueDataName);

        if (data != null)
        {
            dialogueData = data;
            length = dialogueData.GetLength() - 1;
        }

        var afinity = Resources.Load<AfinityContainer>("Afinities/" + afinityDataName);

        if (afinity != null)
        {
            afinityData = afinity;
        }

        if (dialogueData != null)
            SetText(index, choiceRootDialogue, 0, false);
    }

    public string GetNameText() {
       return dialogueData.GetName(index);
    }
}
