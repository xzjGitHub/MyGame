using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineUtil
{
    public delegate void FinishedHandler();
    public event FinishedHandler Finished;      //外部注册此事件

    private CoroutiuneState m_conroutine;

    public CoroutineUtil(IEnumerator c,bool autoStart = true)
    {
        m_conroutine = CoroutineManager.Instance.CreateCoroutiune(c);
        m_conroutine.CallBack += ConroutineFinished;
        if(autoStart)
        {
            Start();
        }
    }

    public bool Running
    {
        get
        {
            return m_conroutine.Running;
        }
    }

    public bool Paused
    {
        get
        {
            return m_conroutine.Paused;
        }
    }

    public void Start()
    {
        m_conroutine.Start();
    }

    public void Stop()
    {
        m_conroutine.Stop();
    }

    public void Pause()
    {
        m_conroutine.Pause();
    }

    public void UnPause()
    {
        m_conroutine.Unpause();
    }

    private void ConroutineFinished()
    {
        FinishedHandler handler = Finished;
        if(handler != null)
        {
            handler();
        }
    }
}
