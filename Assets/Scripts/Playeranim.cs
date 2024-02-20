using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeranim : MonoBehaviour
{
    [Header("Attacks Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;                              //para o player poder efetivar atacks
    [SerializeField] private LayerMask enemyLayer;


    private Player player;

    private Animator anim;

    private Casting cast;

    private bool isHitting;//para o player não ficar sofrendo dano excessivo
    private float recoveryTime = 1f;//tempo de 1 segundo em que o player não toma dano
    private float timeCount;//para o player poder tomar outro dano


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();

        cast = FindObjectOfType<Casting>();
        
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnRun();

        if (isHitting)
        {
            timeCount += Time.deltaTime;//faz o TimeCount ser contado em segundos

            if (timeCount >= recoveryTime)//quando timeCount bater 1 seg. ocorrer a sequencia do codigo
            {
                isHitting = false;//para o player entrar na condição de poder tomar outro dano
                timeCount = 0f;//para zerar o tempo de dano

            }

        }


    }

    #region Movement
    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0)
        {
            if(player.isRolling)
            {
                //verifica se a animal isRoll não esta sendo execultada( == false ou o simbolo !) funciona do msm jeito
                if(anim.GetCurrentAnimatorStateInfo(0).IsName("roll") == false)
                anim.SetTrigger("isRoll");
            }
            else
            {
                anim.SetInteger("transition", 1);
            }
            
            


        }
        else
        {
            anim.SetInteger("transition", 0);

        }

        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if(player.isCutting) 
        {
            anim.SetInteger("transition", 3);
        
        
        
        }

        if (player.isDigging)
        {
            anim.SetInteger("transition", 4);



        }

        if (player.isWatering)
        {
            anim.SetInteger("transition", 5);



        }
    }

    void OnRun()
    {
        if (player.isRunning && player.direction.sqrMagnitude > 0)
        {
            anim.SetInteger("transition", 2);
        }

    }
    #endregion

    #region Attack
    public void OnAttack() 
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);
        if (hit != null) 
        {
            //atacando inimigo(GetComponentInChildren procura o obj AnimationControl nos filhos do esqueleto//
            hit.GetComponentInChildren<AnimationControl>().OnHit();
        
        }
    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

    #endregion
    //é chamado quando player esta no lado de cima da lagoa
    public void OnCastingStarted() 
    {
        anim.SetTrigger("isCasting");
        player.isPaused = true;
    
    }
    //é chamado quando termina de executar a animação de pescaria
    public void OnCastingEnded() 
    {
        cast.OnCasting();
        player.isPaused = false;
    }

    public void OnHammeringStarted() 
    {
        anim.SetBool("hammering", true);
    
    }

    public void OnHammeringEnded() 
    {
        anim.SetBool("hammering", false);

    }

    public void OnHit()
    {
        if(!isHitting) 
        {
            anim.SetTrigger("hit");
            isHitting = true;

        }
        
    }
}
