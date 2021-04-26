using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator.Play("GameOverFadeIn");
        Time.timeScale = 0;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
        UIManager._remainingLives = 3;
        Time.timeScale = 1;
        PoolBase.instance.objPool.Clear();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UIManager._remainingLives = 3;
        Time.timeScale = 1;
        PoolBase.instance.objPool.Clear();
    }
}