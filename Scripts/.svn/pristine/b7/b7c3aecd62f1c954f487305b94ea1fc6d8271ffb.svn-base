using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelBehaviour: MonoBehaviour
{
    protected GameObject PanelGameObject;
    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnReactive() { }
    protected virtual void OnUpdate() { }
    protected virtual void Destroy() { }
    protected virtual void OnShow(List<System.Object> parmers = null) { }
    protected virtual void OnHide() { }

    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }

    private void Update()
    {
        OnUpdate();
    }

    private void OnDestroy()
    {
        Destroy();
    }

    public void Show(List<System.Object> parmers = null)
    {
        this.gameObject.SetActive(true);
        OnShow(parmers);
    }

    public void Reactive()
    {
        this.gameObject.SetActive(true);
        OnReactive();
    }

    public void Hide(bool hide=false)
    {
        OnHide();
        this.gameObject.SetActive(hide);
    }


    public bool IsShow()
    {
        return this.gameObject.activeSelf;
    }
}
