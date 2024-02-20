using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    [SerializeField] private bool detectingPlayer;
    [SerializeField] private int waterValue;

    private PlayerItens player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerItens>();//quando iniciar a cena, a unite procura na cena um objeto q tenha player itens
    }

    // Update is called once per frame
    void Update()
    {
        
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E))
        { 
        
            player.WaterLimit(waterValue);
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

