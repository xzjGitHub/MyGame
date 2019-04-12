using UnityEngine;
using System.Collections.Generic;

public class UIAlterParticleSystemLayer : MonoBehaviour
{
    public string sortingLayer = "char";
    public int orderLayer = 21;


    public void Init(string _sortingLayer = "char", int _orderLayer = 21)
    {
        if (isFirst) return;
        sortingLayer = _sortingLayer;
        orderLayer = _orderLayer;
       // _sortingLayer = "Default";
        foreach (Transform item in transform)
        {
            UpdateTransformRenderer(item);
        }
        isFirst = true;
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    public void UpdateShow(string _sortingLayer = "char", int _orderLayer = 21)
    {
       // _sortingLayer = "Default";
        SetRenderersShow(_sortingLayer, _orderLayer);
    }

    /// <summary>
    /// 设置Renderer显示
    /// </summary>
    private void SetRenderersShow(string _sortingLayer , int _orderLayer)
    {
        foreach (var item in renderers)
        {
            item.Key.sortingLayerName = _sortingLayer;
            item.Key.sortingOrder = _orderLayer + item.Value;
        }
    }

    /// <summary>
    /// 更新指定Transform的Renderer
    /// </summary>
    private void UpdateTransformRenderer(Transform _transform)
    {
        AddRenderer(_transform);
        foreach (Transform item in _transform)
        {
            UpdateTransformRenderer(item);
        }
    }



    /// <summary>
    /// 添加Renderer
    /// </summary>
    private void AddRenderer(Transform _transform)
    {
        particleSystem = _transform.GetComponent<ParticleSystem>();
        if (particleSystem == null) return;
        renderer = GameTools.GetObjRenderer(particleSystem.gameObject);
        if (renderer == null) return;
        renderers.Add(renderer, renderer.sortingOrder);
    }

    //
    private new ParticleSystem particleSystem;
    private new Renderer renderer;
    private Dictionary<Renderer, int> renderers = new Dictionary<Renderer, int>();
    private bool isFirst;
}
