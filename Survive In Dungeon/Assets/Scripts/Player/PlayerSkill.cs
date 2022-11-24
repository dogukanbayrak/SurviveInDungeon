using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{

    [Header("Cooldown Icons")]
    public Image[] CooldownIcons;

    [Header("Out of Mana Icons")]
    public Image[] ManaIcons;

    [Header("Cooldown Times")]
    public float[] cooldownTimes;

    public List<float> CooldownTimesList = new List<float>();

    private bool faded = false;

    private int[] fadeImages = new int[] { 0, 0, 0, 0, 0 };

    private Animator anim;

    private bool canAttack = true;

    private PlayerController playerController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }


    void Start()
    {
        for(int i=0; i < 5; i++)
        {
            CooldownTimesList.Add(cooldownTimes[i]);
        }
        
    }

    void Update()
    {
        if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
        CheckToFade();
        CheckInput();
    }


    void CheckInput()
    {
        if(anim.GetInteger("Attack") == 0)
        {
            //playerController.FinishedMovement = false;

            if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                playerController.FinishedMovement = true;
            }
        }


        //Skill Input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerController.TargetPosition = transform.position;
            if(playerController.FinishedMovement && fadeImages[0] !=1 && canAttack)
            {
                fadeImages[0] = 1;
                anim.SetInteger("Attack", 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerController.TargetPosition = transform.position;
            if (playerController.FinishedMovement && fadeImages[1] != 1 && canAttack)
            {
                fadeImages[1] = 1;
                anim.SetInteger("Attack", 2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerController.TargetPosition = transform.position;
            if (playerController.FinishedMovement && fadeImages[2] != 1 && canAttack)
            {
                fadeImages[2] = 1;
                anim.SetInteger("Attack", 3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerController.TargetPosition = transform.position;
            if (playerController.FinishedMovement && fadeImages[3] != 1 && canAttack)
            {
                fadeImages[3] = 1;
                anim.SetInteger("Attack", 4);
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            playerController.TargetPosition = transform.position;
            if (playerController.FinishedMovement && fadeImages[4] != 1 && canAttack)
            {
                fadeImages[4] = 1;
                anim.SetInteger("Attack", 5);
            }
        }

        else
        {
            anim.SetInteger("Attack", 0);
            
        }

    }


    void CheckToFade()
    {

        for(int i=0; i<CooldownIcons.Length; i++)
        {
            if (fadeImages[i] == 1)
            {
                if (FadeAndWait(CooldownIcons[i], CooldownTimesList[i]))
                {
                    fadeImages[i] = 0;
                }
            }
        }        
    }

    bool FadeAndWait(Image fadeImage, float fadeTime)
    {
        faded = false;
        if (fadeImage==null)
        {
            return faded;
        }

        if (!fadeImage.gameObject.activeInHierarchy)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.fillAmount = 1f;
        }

        fadeImage.fillAmount -= fadeTime * Time.deltaTime;

        if(fadeImage.fillAmount <= 0f)
        {
            fadeImage.gameObject.SetActive(false);
            faded = true;
        }
        return faded;
    }
}
