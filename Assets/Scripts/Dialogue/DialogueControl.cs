using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{

    [System.Serializable]  //para aparecer no inspector da Unyte usasse o serializable//
    public enum idiom
    {
        pt,
        en,
        spa
    }

    public idiom language;



    [Header("Components")]
    public GameObject dialogueObj;//janela do dialogo
    public Image profileSprite;//sprite do perfil
    public Text speechText;//texto da fala
    public Text actorNameText;//nome do npc

    [Header("Settings")]
    public float typingSpeed;//velocidade da fala

    //Variaveis de controle
    public bool isShowing;//se a janela esta visivel
    private int index;//index das sentenças
    private string[] sentences;
    private string[] currentActorName;
    private Sprite[] actorSprite;

    private Player player;


    //com isso eu consigo acessar qualquer variável e qquer método publico dentro da classe dialogue//
    public static DialogueControl instance;


    private void Awake() //awake é chamado antes de todos os start() na hierarquia de execução de scripts//
    {
        instance = this;//poderia ser usado no void start, porém aqui evita bugs//
    }

    private void Start()
    {
        // FindObjectOfType server para procurar na cena um obj com nome<Player>, se tiver mais d um obj com esse nome não funciona
        player = FindObjectOfType<Player>();
    }


    //efeito para a fala aparecer letra por letra//
    IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    //pular para a proxima fala com Button E como foi configurado//
    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            if(index < sentences.Length - 1)
            {
                index++;
                profileSprite.sprite = actorSprite[index];
                actorNameText.text = currentActorName[index];
                speechText.text = "";
                StartCoroutine(TypeSentence());

            }
            else//quando terminam os textos//
            {
                speechText.text = "";   //limpar o texto para preparar pro proximo texto
                actorNameText.text = "";//limpar o nome e nao bugar
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null; //para zerar as falas//
                isShowing = false;//para liberar o dialogo caso o personagem volte a falar com o NPC//
                player.isPaused = false;
            }
        }
    }
    //chamar a fala do npc//
    public void Speech(string[] txt, string[] actorName, Sprite[] actorProfile)
    {
        if (!isShowing) 
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            currentActorName = actorName;
            actorSprite = actorProfile;
            profileSprite.sprite = actorSprite[index];
            actorNameText.text = currentActorName[index];
            StartCoroutine(TypeSentence());
            isShowing = true;
            player.isPaused = true;
        }

    }
}
