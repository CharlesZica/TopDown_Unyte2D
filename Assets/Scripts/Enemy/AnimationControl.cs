using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;


    private Playeranim player;//chamar o elemento do script player
    private Animator anim;
    private Skeleton skeleton;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Playeranim>();//procura um obj na cena que tenha o script Playeranim
        skeleton = GetComponentInParent<Skeleton>();//procura obj <Skeleton> no Pai//
    }

    public void PlayAnim(int value) 
    {
        anim.SetInteger("transition", value);
    }

    public void Atack()
    {
        if (!skeleton.isDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

            if (hit != null)
            {
                //detecta colisão com player(Hora que o esqueleto bate no player)
                player.OnHit();

            }
        }


    }
    public void OnHit()//skeleton tomando dano
    {
        if(skeleton.currentHealth <= 0) 
        {
            skeleton.isDead = true;
            anim.SetTrigger("death");

            Destroy(skeleton.gameObject, 10f);
        
        }
        else
        {
            anim.SetTrigger("hit");
            skeleton.currentHealth--;
            //calculo para mostrar a vida do player  
            skeleton.healthBar.fillAmount = skeleton.currentHealth / skeleton.totalHealth;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
