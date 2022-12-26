using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Tagging tagging;

    private void Start()
    {
        animator = GetComponent<Animator>();
        tagging = GetComponent<Tagging>();
    }

    private void OnEnable()
    {
        EventSystem.OnStartGame += OnGameStart;
        EventSystem.OnGameOver += OnGameOver;
        EventSystem.OnNpcTag += OnNpcTagged;
    }

    private void OnDisable()
    {
        EventSystem.OnStartGame -= OnGameStart;
        EventSystem.OnGameOver -= OnGameOver;
        EventSystem.OnNpcTag -= OnNpcTagged;
    }

    private void OnGameStart()
    {
        animator.ResetTrigger(PlayerPrefKeys.stopTrigger);
        animator.SetTrigger(PlayerPrefKeys.runTrigger);
    }

    private void OnGameOver(GameResult gameResult)
    {
        animator.ResetTrigger(PlayerPrefKeys.runTrigger);
        animator.SetTrigger(PlayerPrefKeys.stopTrigger);
    }

    private void OnNpcTagged(Tagging _tagging)
    {
        if (tagging == _tagging)
        {
            animator.ResetTrigger(PlayerPrefKeys.runTrigger);
            animator.SetTrigger(PlayerPrefKeys.stopTrigger);
        }
        
    }
}
