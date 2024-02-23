using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[CreateAssetMenu(fileName = "Conversation Container", menuName = "ScriptableObject/ConversationContainerObject")]
public class DialogueContainer : ScriptableObject
{
    //[SerializeField]
    //private string characterDialogue;
    [SerializeField]
    private Sprite ch1, ch2;
    [SerializeField]
    private bool isThisDialogueaCutScene;
    [SerializeField]
    private bool dialogueEnded;
    [SerializeField]
    private string textEnded;
    [Serializable]
    private struct rootDialogue { [TextArea] public string text; public Color c1, c2; }
    [Serializable]
    private struct dialogueData { public int id;  public string characterName; [TextArea] public string text; public bool hasChoice; public string goodChoice, badChoice; public rootDialogue[] goodChoiceRoot, badChoiceRoot; /*[TextArea] public string[] goodChoiceRoot, badChoiceRoot;*/ public float afinityToAdd; public float afinityToRemove; public Color c1, c2; public Sprite e1, e2; public Sprite backGroundImage; public bool choiced; public bool badChoiceChoosed, goodChoiceChoosed; }
    [SerializeField]
    private dialogueData[] data;
    [Header("----DEBUG-----")]
    [SerializeField]
    private bool removeH;
    [SerializeField]
    private bool reset;
    private void OnValidate()
    {
        if (removeH)
        {
            Debug.Log("health removed");
            removeH = false;
        }

        for (int i = 0; i < data.Length; i++)
        {
            data[i].id = i;
            //data[i].c1.a = 1;
            //data[i].c2.a = 1;
        }

        if (reset)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i].choiced = false;
                data[i].goodChoiceChoosed = false;
                data[i].badChoiceChoosed = false;
            }
            reset = false;
        }
    }
    public Sprite GetBakgroundImage(int id)
    {
        return data.FirstOrDefault(x => x.id == id).backGroundImage;
    }
    public string GetText(int id)
    {
        return data.FirstOrDefault(x => x.id == id).text;
    }
    public string GetName(int id)
    {
        return data.FirstOrDefault(x => x.id == id).characterName;
    }
    public string GetRootText(int id, int choiceRootId, bool goodOrBad)
    {
        //Debug.Log(id);
        //Debug.Log(choiceRootId);
        if (goodOrBad)
        {
            //Debug.Log(choiceRootId);
            //Debug.Log(data.FirstOrDefault(x => x.id == _id).goodChoiceRoot.Length);
            return data.FirstOrDefault(x => x.id == id).goodChoiceRoot[choiceRootId].text;
            
        }
        else 
        {
            return data.FirstOrDefault(x => x.id == id).badChoiceRoot[choiceRootId].text;
        }
    }
    public int GetRootlength(int id, bool goodOrBad)
    {
        if (goodOrBad)
            return data.FirstOrDefault(x => x.id == id).goodChoiceRoot.Length;
        else
            return data.FirstOrDefault(x => x.id == id).badChoiceRoot.Length;
    }

    /*public void SetChoiceChoosed()
    {

    }*/
    public Color GetColor(int id, int colorIndex)
    {
        if (colorIndex == 0)
            return data.FirstOrDefault(x => x.id == id).c1;
        else
            return data.FirstOrDefault(x => x.id == id).c2;
    }
    public Color GetRootColor(int id, int choiceRootId, bool gOrB, int colorIndex)
    {
        if (gOrB)
        {
            if(colorIndex ==0)
            return data.FirstOrDefault(x => x.id == id).goodChoiceRoot[choiceRootId].c1;
            else
            return data.FirstOrDefault(x => x.id == id).goodChoiceRoot[choiceRootId].c2;
        }
        else
        {
            if (colorIndex == 0)
                return data.FirstOrDefault(x => x.id == id).badChoiceRoot[choiceRootId].c1;
            else
                return data.FirstOrDefault(x => x.id == id).badChoiceRoot[choiceRootId].c2;
        }
    }
    public Sprite GetExpression(int id, int expressionIndex)
    {
        if (expressionIndex == 0)
            return data.FirstOrDefault(x => x.id == id).e1;
        else
            return data.FirstOrDefault(x => x.id == id).e2;
    }

    public Sprite GetCharacterSprite1()
    {
        return ch1;
    }

    public Sprite GetCharacterSprite2()
    {
        return ch2;
    }
    public bool GetHasChoice(int id)
    {
        return data.FirstOrDefault(x => x.id == id).hasChoice;
    }

    public string GetGoodChoice(int id)
    {
        return data.FirstOrDefault(x => x.id == id).goodChoice;
    }

    public string GetBadChoice(int id)
    {
        return data.FirstOrDefault(x => x.id == id).badChoice;
    }
    public int GetLength()
    {
        return data.Length;
    }

    public bool GetChoiced(int id)
    {
        return data.FirstOrDefault(x => x.id == id).choiced;
    }

    public void SetChoiced(int id, bool value)
    {
        int i = Array.FindIndex(data, x => x.id == id);
        data[i].choiced = value;
    }

    public bool GetChoiceChoosed(int id, bool bOrG)
    {
        if(bOrG)
        return data.FirstOrDefault(x => x.id == id).goodChoiceChoosed;
        else
        return data.FirstOrDefault(x => x.id == id).badChoiceChoosed;
    }

    public void SetGoodChoiced(int id, bool value)
    {
        int i = Array.FindIndex(data, x => x.id == id);
        data[i].goodChoiceChoosed = value;
    }

    public void SetBadChoiced(int id, bool value)
    {
        int i = Array.FindIndex(data, x => x.id == id);
        data[i].badChoiceChoosed = value;
    }
    public bool GetDialogueEnded()
    {
        return dialogueEnded;
    }

    public void SetDialogueEnd(bool value)
    {
        dialogueEnded = value;
    }
    public float GetAfinityToAdd(int id)
    {
        return data.FirstOrDefault(x => x.id == id).afinityToAdd;
    }
    public float GetAfinityToRemove(int id)
    {
        return data.FirstOrDefault(x => x.id == id).afinityToRemove;
    }

    public string GetTextEnded()
    {
        return textEnded;
    }

    public bool GetIsThisDaiologueACusscene()
    {
        return isThisDialogueaCutScene;
    }

}
