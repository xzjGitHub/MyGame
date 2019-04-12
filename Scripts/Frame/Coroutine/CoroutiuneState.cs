using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutiuneState
{
    private IEnumerator coroutine;
    private bool running;
    private bool paused;
    private bool stopped;

    public delegate void FinishedCallBack();
    public event FinishedCallBack CallBack;

    public bool Running
    {
        get
        {
            return running;
        }
    }

    public bool Paused
    {
        get
        {
            return paused;
        }
    }


    public CoroutiuneState(IEnumerator c)
    {
        coroutine = c;
    }

    public void Pause()
    {
        paused = true;
    }

    public void Unpause()
    {
        paused = false;
    }

    public void Start()
    {
        running = true;
        CoroutineManager.Instance.StartCoroutine(CallWrapper());
    }

    public void Stop()
    {
        stopped = true;
        running = false;
    }

    private IEnumerator CallWrapper()
    {
        yield return null;
        IEnumerator e = coroutine;
        while(running)
        {
            if(paused)
                yield return null;
            else
            {
                if(stopped == false && e != null && e.MoveNext())
                {
                    yield return e.Current;
                }
                else
                {
                    running = false;
                }
            }
        }

        FinishedCallBack handler = CallBack;
        if(handler != null)
        {
            handler();
        }
    }
}
