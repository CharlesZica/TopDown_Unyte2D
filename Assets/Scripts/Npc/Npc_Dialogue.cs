using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_Dialogue : MonoBehaviour
{

    public float dialogueRange;
    public LayerMask playerLayer;

    public DialogueSetings dialogue;

    bool playerHit;// para o player colidir com dialogo//
                        //lista de varios dialogos(sentences)//
    private List<string> sentences = new List<string>();//para armazenar as sentences que estão em lista//
    private List<string> actorName = new List<string>();
    private List<Sprite> actorSprite = new List<Sprite>();

    private void Start()// para chamar ao inicializar o void GetNPCInfo//
    {
        GetNPCInfo();
    }

    //é chamado a cada frame//
    void Update()
    {       
        //pressionando E e estando perto do NPC, ativa o dialogo
        if (Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            //depois do colisor do NPC ativar o dialogo apareceram as frase de (sentences)
            DialogueControl.instance.Speech(sentences.ToArray(), actorName.ToArray(), actorSprite.ToArray());//ToArray transforma tudo que esta em private List em um array//
        }
    }

    void GetNPCInfo()//para chamar o dialogo ja que sem esse metodo não será possivel a principio//
    {
        for (int i = 0; i < dialogue.dialogues.Count; i++)  //para puxar o dialogo em sequencia tantos quantos tiverem//
        {
            switch (DialogueControl.instance.language) //para selecionar um idioma( nos permite colocar determinada linha de codigo)//
            {
                //executa o que esta no case e sai fora no break//
                case DialogueControl.idiom.pt:
                    sentences.Add(dialogue.dialogues[i].sentence.portuguese);

                    break;

                case DialogueControl.idiom.en:
                    sentences.Add(dialogue.dialogues[i].sentence.english);
                    break;

                case DialogueControl.idiom.spa:
                    sentences.Add(dialogue.dialogues[i].sentence.spanish);
                    break;


            }

            actorName.Add(dialogue.dialogues[i].actorName);//chama o nome do ator do primeiro dialogo
            actorSprite.Add(dialogue.dialogues[i].profile);//chama o sprite do ator do primeiro dialogo


        }
    }

    // Usado pela fisica
    void FixedUpdate()
    {
        ShowDialogue();
    }

    void ShowDialogue()
    {
        //Identifica quando o player esta na area de ação com NPC
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);


        //para aparecer o dialogo pois ativa o colisor que tem no NPC fazendo o playerHit = true;
        if (hit != null)

        {

            playerHit = true;

        }
        //para deparecer o dialogo ao sair de perto do NPC, pois o player a qualquer momento se ficar fora do colisor
        else
        {
            playerHit = false;
            
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
