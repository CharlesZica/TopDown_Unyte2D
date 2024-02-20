using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")]
public class DialogueSetings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;


    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string sentence;

    public List<Sentences> dialogues = new List<Sentences>();
}

[System.Serializable]

public class Sentences
{
    public string actorName;
    public Sprite profile;
    public Languages sentence;

}

[System.Serializable]


public class Languages
{
    public string portuguese;
    public string english;
    public string spanish;
}


//CRIAÇÂO D UMA CLASSE PARA CRIAR UM BOTÂO NO INSPECTOR//
#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSetings))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();//redesenha o que esta dentro do inspector//

        DialogueSetings ds = (DialogueSetings)target;

        Languages l = new Languages();
        //definição da lingua mãe//
        l.portuguese = ds.sentence;
        //sentences = fala //
        Sentences s = new Sentences();
        s.profile = ds.speakerSprite;
        s.sentence = l;
        //para criação de um botão visual//
        if (GUILayout.Button("Create Dialogue"))
        {
            if(ds.sentence != "") 
            {
                ds.dialogues.Add(s);

                ds.speakerSprite = null;

                ds.sentence = "";
            
            }
        }
    }

}




#endif




