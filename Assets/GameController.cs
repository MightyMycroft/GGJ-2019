﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Animator animator;
    private static float time = 0f;
    public float maxTimeInSeconds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseTime();
        CheckCrackenAnimation();
    }

    private static void IncreaseTime()
    {
        time += Time.deltaTime;
    }

    public static float GetTime()
    {
        return time;
    }

    private void CheckCrackenAnimation()
    {
        if(GetTime() > maxTimeInSeconds)
        {
            Debug.Log("It is over");
            if(animator.GetBool("AttackCracken") == false)
            {
                animator.SetBool("AttackCracken", true);
            }
        }
    }

    public void ResetGame()
    {
        time = 0f;
        animator.SetBool("AttackCracken", false);
    }

}
