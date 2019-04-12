using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIAlterParticleSystemAlpha : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    private new Renderer renderer;
    private Material aMaterial;
    private const string str = "_TintColor";
    private Dictionary<Material, Color> listColors = new Dictionary<Material, Color>();
    //
    public float aspd = 1.5f;
    //
    private int sum;
    private bool isCheck;
    //
    private CoroutineUtil IE_UpdateColor;

    void Update()
    {
        UpdateCheck();
    }

    /// <summary>
    /// 设置Alpha
    /// </summary>
    public void SetAlpha(int value)
    {
        return;
        GetInfo();
        if (listColors.Count == 0) return;
        foreach (var item in listColors)
        {
            SetColorAlpha(item.Key, item.Value, value);
        }
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 重置Alpha
    /// </summary>
    public void ResetAlpha()
    {
        ResetColor();
    }


    public void StartUpdate()
    {
        return;
        GetInfo();
        sum = 0;
        if (listColors.Count == 0) return;
        foreach (var item in listColors)
        {
            if (item.Value.a == 0) continue;
            IE_UpdateColor = new CoroutineUtil(IEUpdateColor(item.Key, item.Value));
        }
        isCheck = true;
    }

    private void UpdateCheck()
    {
        if (!isCheck) return;
        if (sum != listColors.Count) return;
        //
        isCheck = false;
       // gameObject.SetActive(false);
        ResetColor();
    }

    private void ResetColor()
    {
        return;
        foreach (var item in listColors)
        {
            item.Key.SetColor(str, item.Value);
        }
    }

    /// <summary>
    /// 设置颜色Alpha
    /// </summary>
    private void SetColorAlpha(Material _material, Color _color, int value)
    {
        Color _col = _color;
        _col.a = value;
        _material.SetColor(str, _col);
    }

    IEnumerator IEUpdateColor(Material _material, Color _color)
    {
        yield return null;
        Color _col = _color;

        while (_col.a > 0)
        {
            _col.a -= Time.deltaTime * aspd;
            _material.SetColor(str, _col);
            yield return null;
        }
        sum++;
    }

    private void UpdateTransform(Transform _transform)
    {
        AddMaterial(_transform);
        foreach (Transform item in _transform)
        {
            UpdateTransform(item);
        }
    }

    private void AddMaterial(Transform _transform)
    {
        particleSystem = _transform.GetComponent<ParticleSystem>();
        if (particleSystem == null) return;
        renderer = particleSystem.GetComponent<Renderer>();
        if (renderer == null) return;
        aMaterial = renderer.sharedMaterial;
        if (aMaterial == null) return;
        if (aMaterial.name.Contains("Default")) return;
        if (listColors.ContainsKey(aMaterial)) return;
        listColors.Add(aMaterial, aMaterial.GetColor(str));
    }

    private void GetInfo()
    {
        if (listColors.Count > 0) return;
        foreach (Transform item in transform)
        {
            UpdateTransform(item);
        }
    }

    /// <summary>
    /// 停止所有协成
    /// </summary>
    private void StopAllCoroutine()
    {
        if (IE_UpdateColor != null) IE_UpdateColor.Stop();
        IE_UpdateColor = null;
    }

    // 当 MonoBehaviour 将被销毁时调用此函数
    private void OnDestroy()
    {
        ResetColor();
        StopAllCoroutine();
    }
}
