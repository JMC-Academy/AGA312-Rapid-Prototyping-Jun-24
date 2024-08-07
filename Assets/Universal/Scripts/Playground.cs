using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Playground : GameBehaviour
{
    public enum Direction { North, East, South, West }
    public GameObject player;
    public float moveDistance = 2f;
    public float moveTweenTime = 1f;
    public Ease moveEase;
    public float shakeStrength = 0.4f;
    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public Ease scoreEase;
    private int score = 0;
    public int scoreBonus = 100;
    private float bloom = 0;

    [Header("Timer")]
    public Timer timer;
    public TMP_Text timerText;

    void Start()
    {
        //timer.StartTimer(60, TimerDirection.CountDown);
        player.transform.position = _SAVE.GetLastCheckpoint();
        player.GetComponent<Renderer>().material.color = _SAVE.GetColour();
        highScoreText.text = "HIGHEST SCORE: " + _SAVE.GetHighestScore().ToString();


        timer.StartTimer(0, 10, true, TimerDirection.CountUp);

        ExecuteAfterSeconds(2, () =>
        {
            player.transform.localScale = Vector3.one * 2;
        });

        ExecuteAfterFrames(1, () =>
        {
            print("One Frame Later");
        });
        
    }

    void Update()
    {
        timerText.text = timer.GetTime().ToString("F2");
        if(Input.GetKeyDown(KeyCode.C))
        {
            if (timer.timerDirection == TimerDirection.CountUp)
                timer.ChangeTimerDirection(TimerDirection.CountDown);
            else
                timer.ChangeTimerDirection(TimerDirection.CountUp);
        }

        if(Input.GetKeyDown(KeyCode.P))
            timer.ToggleTimerPause();

        if (timer.TimeExpired())
            Debug.Log("Time Expired");


        if (Input.GetKeyDown(KeyCode.W))
            MovePlayer(Direction.North);
        if (Input.GetKeyDown(KeyCode.D))
            MovePlayer(Direction.East);
        if (Input.GetKeyDown(KeyCode.S))
            MovePlayer(Direction.South);
        if (Input.GetKeyDown(KeyCode.A))
            MovePlayer(Direction.West);
    }

    void MovePlayer(Direction _direction)
    {
        switch(_direction)
        {
            case Direction.North:
                player.transform.DOMoveZ(player.transform.position.z + moveDistance, moveTweenTime).SetEase(moveEase).OnComplete(()=>
                {
                    ShakeCamera();
                    IncreaseScore();
                });
                break;
            case Direction.East:
                player.transform.DOMoveX(player.transform.position.x + moveDistance, moveTweenTime).SetEase(moveEase).OnComplete(() =>
                {
                    ShakeCamera();
                    IncreaseScore();
                });
                break;
            case Direction.South:
                player.transform.DOMoveZ(player.transform.position.z - moveDistance, moveTweenTime).SetEase(moveEase).OnComplete(() =>
                {
                    ShakeCamera();
                    IncreaseScore();
                });
                break;
            case Direction.West:
                player.transform.DOMoveX(player.transform.position.x - moveDistance, moveTweenTime).SetEase(moveEase).OnComplete(() =>
                {
                    ShakeCamera();
                    IncreaseScore();
                });
                break;
        }
        _SAVE.SetLastPosition(player.transform.position);
        ChangeColour();
        _EFFECTS.TweenChromaticInOut(1, 0.5f);
        bloom += 0.5f;
        _EFFECTS.SetBloom(bloom);
    }

    void ShakeCamera()
    {
        Camera.main.DOShakePosition(moveTweenTime / 2, shakeStrength);
        _EFFECTS.TweenVignetteInOut(0.5f, 0.5f);
    }

    void ChangeColour()
    {
        Color c = ColorX.GetRandomColour();
        _SAVE.SetColour(c);
        player.GetComponent<Renderer>().material.DOColor(c, moveTweenTime);
    }

    void IncreaseScore()
    {
        TweenX.TweenNumbers(scoreText, score, score + scoreBonus, 1, scoreEase, "F1");
        score = score + scoreBonus;
        _SAVE.SetScore(score);
    }

    public int health = 100000;
    public void Poison()
    {
        health -= 1;
        Debug.Log("Poisoned " + health);
    }
    public void AddHealth(int _health) => health += _health;
    public void LoseHealth(int _health) => health -= _health;
}
