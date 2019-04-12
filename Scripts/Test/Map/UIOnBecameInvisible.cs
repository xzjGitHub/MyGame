using UnityEngine;
using System.Collections;

public class UIOnBecameInvisible : MonoBehaviour
{

    public bool isVisible;


    private void OnBecameVisible()
    {
        isVisible = true;
    }
    private void OnBecameInvisible()
    {
        isVisible = false;
    }
    
}
