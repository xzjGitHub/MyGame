using UnityEngine;
using System.Collections;

public class UICombatStartAnimation : MonoBehaviour
{

    public delegate void CallBack();

    public CallBack OnPlayEnd;

    private void PlayEnd()
    {
        gameObject.SetActive(false);
        if (OnPlayEnd!=null)
        {
            OnPlayEnd();
        }
    }
}
