using System;
using System.Collections;
using UnityEngine;

public class ResourceLoadAsync
{

    public ResourceRequest Res { get { return res; } }

    public bool IsDone
    {
        get
        {
            if (res == null)
            {
                return false;
            }
            return res.isDone;
        }
    }

    public ResourceLoadAsync(string path, Action<ResourceLoadAsync, object, object> action, object obj1, object obj2)
    {
        action2 = action;
        param = obj1;
        param1 = obj2;
        new CoroutineUtil(LoadRes(path));
    }

    public ResourceLoadAsync(string path, Action<ResourceLoadAsync, object> action, object obj)
    {
        action1 = action;
        param = obj;
        new CoroutineUtil(LoadRes(path));
    }

    public ResourceLoadAsync(string path, Action<ResourceLoadAsync> action)
    {
        this.action = action;
        new CoroutineUtil(LoadRes(path));
    }

    private IEnumerator LoadRes(string path)
    {
        for (int i = 0; i < 10; i++)
        {
            yield return null;
        }
        int index = 0;
        res = Resources.LoadAsync(path);
        while (!res.isDone)
        {
            index++;
            yield return null;
        }
        LogHelperLSK.LogError(index);
        if (action != null)
        {
            action(this);
        }
        if (action1 != null)
        {
            action1(this, param);
        }
        if (action2 != null)
        {
            action2(this, param, param1);
        }
    }

    //
    private readonly Action<ResourceLoadAsync> action;
    private readonly Action<ResourceLoadAsync, object> action1;
    private readonly Action<ResourceLoadAsync, object, object> action2;
    private readonly object param;
    private readonly object param1;
    private ResourceRequest res;
}
