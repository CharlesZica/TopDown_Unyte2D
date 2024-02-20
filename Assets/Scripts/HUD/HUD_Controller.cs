using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour
{
    [Header("Itens")]
    [SerializeField] private Image waterUIBar;
    [SerializeField] private Image woodUIBar;
    [SerializeField] private Image carrotUIBar;
    [SerializeField] private Image fishUIBar;

    [Header("Tools")]

    //[SerializeField] private Image axeUI;
    //[SerializeField] private Image shovelUI;
    //[SerializeField] private Image bucketUI;
    
    public List<Image> toolsUI = new List<Image>();
    [SerializeField] private Color selectColor; 
    [SerializeField] private Color alphaColor;

    private PlayerItens playerItens;
    private Player player;

    private void Awake()
    {
        playerItens = FindObjectOfType<PlayerItens>();
        player = playerItens.GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        waterUIBar.fillAmount = 0f;
        woodUIBar.fillAmount = 0f;
        carrotUIBar.fillAmount = 0f;
        fishUIBar.fillAmount = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        waterUIBar.fillAmount = playerItens.currentWater / playerItens.waterLimit;
        woodUIBar.fillAmount = playerItens.totalWood / playerItens.woodLimit;
        carrotUIBar.fillAmount = playerItens.carrots / playerItens.carrotsLimit;
        fishUIBar.fillAmount = playerItens.fishes / playerItens.fishesLimit;


        //toolsUI[player.handlingObj].color = selectColor;


        for (int i = 0; i < toolsUI.Count; i++)
        {
            if (i == player.handlingObj)
            {
                toolsUI[i].color = selectColor;
                        
            } 
            else 
            {
                toolsUI[i].color = alphaColor;
            }
            
                 

                 

            
                
        }

    }
}
