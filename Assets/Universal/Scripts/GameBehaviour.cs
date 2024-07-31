using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GameBehaviour : MonoBehaviour
{
    protected static SaveManager _SAVE { get { return SaveManager.INSTANCE; } }
    protected static EffectsManager _EFFECTS { get { return EffectsManager.Instance; } }

    #region Coroutine Helpers

    /// <summary>
    /// Executes the Action block as a Coroutine on the next frame.
    /// </summary>
    /// <param name="func">The Action block</param>
    protected void ExecuteNextFrame(Action func)
    {
        StartCoroutine(ExecuteAfterFramesCoroutine(1, func));
    }
    /// <summary>
    /// Executes the Action block as a Coroutine after X frames.
    /// </summary>
    /// <param name="func">The Action block</param>
    protected void ExecuteAfterFrames(int frames, Action func)
    {
        StartCoroutine(ExecuteAfterFramesCoroutine(frames, func));
    }
    private IEnumerator ExecuteAfterFramesCoroutine(int frames, Action func)
    {
        for (int f = 0; f < frames; f++)
            yield return new WaitForEndOfFrame();
        func();
    }

    /// <summary>
    /// Executes the Action block as a Coroutine after X seconds
    /// </summary>
    /// <param name="seconds">Seconds.</param>
    protected void ExecuteAfterSeconds(float seconds, Action func)
    {
        if (seconds <= 0f)
            func();
        else
            StartCoroutine(ExecuteAfterSecondsCoroutine(seconds, func));
    }
    private IEnumerator ExecuteAfterSecondsCoroutine(float seconds, Action func)
    {
        yield return new WaitForSeconds(seconds);
        func();
    }

    /// <summary>
    /// Executes the Action block as a Coroutine whern a condition is met
    /// </summary>
    /// <param name="condition">The Condition block</param>
    /// <param name="func">The Action block</param>
    protected void ExecuteWhenTrue(Func<bool> condition, Action func)
    {
        StartCoroutine(ExecuteWhenTrueCoroutine(condition, func));
    }
    private IEnumerator ExecuteWhenTrueCoroutine(Func<bool> condition, Action func)
    {
        while (condition() == false)
            yield return new WaitForEndOfFrame();
        func();
    }

    #endregion

}

public class GameBehaviour<T> : GameBehaviour where T : GameBehaviour
{
    static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameBehaviour<" + typeof(T).ToString() + "> not instantiated!\nNeed to call Instantiate() from " + typeof(T).ToString() + "Awake().");
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
