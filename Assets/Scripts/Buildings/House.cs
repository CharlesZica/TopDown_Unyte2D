using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [Header("Amounts")]
    [SerializeField] private int woodAmount;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private float timeAmount;

    [Header("Components")]
    [SerializeField] private GameObject houseColl;
    [SerializeField] private SpriteRenderer houseSprite;
    [SerializeField] private Transform point;



    private bool detectingPlayer;
    private Player player;
    private Playeranim playerAnim;
    private PlayerItens playerItens;

    private float timeCount;
    private bool isBeginig;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();//quando iniciar a cena, a unite procura na cena um objeto q tenha player itens
        playerAnim = player.GetComponent<Playeranim>();
        playerItens = player.GetComponent<PlayerItens>();
    }

    // Update is called once per frame
    void Update()
    {

        if (detectingPlayer && Input.GetKeyDown(KeyCode.E) && playerItens.totalWood >= woodAmount)
        {
            //Construção é iniciada
            isBeginig = true;
            playerAnim.OnHammeringStarted();
            houseSprite.color = startColor;
            player.transform.position = point.position;
            player.isPaused = true;
            playerItens.totalWood -= woodAmount;
        }
        if (isBeginig)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= timeAmount) 
            {
                //casa é finalizada
                playerAnim.OnHammeringEnded();
                houseSprite.color = endColor;
                player.isPaused=false;
                houseColl.SetActive(true);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = true;


        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = false;


        }

    }
}
