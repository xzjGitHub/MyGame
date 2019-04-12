using System.Collections;
using UnityEngine;

public class TestObjectMove : MonoBehaviour
{

    public float rotateX = 450;      // 最大X轴旋转角度  
    public float speed = 100;        // 速度  
    public float height = 100;       //最大高度   

    public void OnClickButton(Transform target)
    {
        StartParabolaMove(transform.localPosition, target.localPosition);
    }


    /// <summary>
    /// 开始抛物线移动
    /// </summary>
    private void StartParabolaMove(Vector3 start, Vector3 end)
    {
        _startPos = start;
        _stopPos = end;
        // 计算起始位置到目标位置的角度
        _angleToStop = GetAngleToStop(start, end);
        _curDistance = 0;
        // 计算总距离  
        Vector3 v = end - start;
        _totalDistance = Mathf.Sqrt(v.x * v.x + v.z * v.z);
        // 设置单前X，Y轴的旋转角度  
        Vector3 rotation = transform.eulerAngles;
        if (rotateX > 0)
        {
            rotation.x = -rotateX;
        }
        rotation.y = _angleToStop;

        transform.eulerAngles = rotation;
        _curRotation = rotation;
        IEMoveUpdate = new CoroutineUtil(IMoveUpdate(start, end));
    }
    /// <summary>
    /// 移动更新
    /// </summary>
    /// <returns></returns>
    private IEnumerator IMoveUpdate(Vector3 start, Vector3 end)
    {

        bool _isFiring = true;
        while (_isFiring)
        {
            _isFiring = UpdatePos();
            yield return null;
        }
    }
    /// <summary>
    /// 判断是否到达
    /// </summary>
    private bool IsArrived()
    {
        return _curDistance >= _totalDistance;
    }
    /// <summary>
    /// 更新位置
    /// </summary>
    /// <returns></returns>
    private bool UpdatePos()
    {
        // 更新当前位置  
        transform.localPosition = GetNextPos();
        // 如果到达了目标位置就停止
        return !IsArrived();
    }
    /// <summary>
    /// 计算起始位置到目标位置的角度  
    /// </summary>
    private float GetAngleToStop(Vector3 startPos, Vector3 stopPos)
    {
        stopPos.x -= startPos.x;
        stopPos.z -= startPos.z;

        float deltaAngle = 0;
        if (stopPos.x == 0 && stopPos.z == 0)
        {
            return 0;
        }
        else if (stopPos.x > 0 && stopPos.z > 0)
        {
            deltaAngle = 0;
        }
        else if (stopPos.x > 0 && stopPos.z == 0)
        {
            return 90;
        }
        else if (stopPos.x > 0 && stopPos.z < 0)
        {
            deltaAngle = 180;
        }
        else if (stopPos.x == 0 && stopPos.z < 0)
        {
            return 180;
        }
        else if (stopPos.x < 0 && stopPos.z < 0)
        {
            deltaAngle = -180;
        }
        else if (stopPos.x < 0 && stopPos.z == 0)
        {
            return -90;
        }
        else if (stopPos.x < 0 && stopPos.z > 0)
        {
            deltaAngle = 0;
        }

        float angle = Mathf.Atan(stopPos.x / stopPos.z) * Mathf.Rad2Deg + deltaAngle;
        return angle;
    }
    /// <summary>
    /// 获得下一个位置
    /// </summary>
    private Vector3 GetNextPos()
    {
        // 计算x.z轴移动向量，然后再把它们乘移动距离，这样就能移动到下一个位置  
        float delta = speed * Time.deltaTime;
        float deltaX = Mathf.Sin(_angleToStop * Mathf.Deg2Rad);
        float deltaZ = Mathf.Cos(_angleToStop * Mathf.Deg2Rad);
        //
        Vector3 _curPos = Vector3.zero;
        _curPos.x += deltaX * delta;
        _curPos.z += deltaZ * delta;
        // 增加当前距离，用来判断是否到达到终点了  
        _curDistance += delta;
        float deltaDistance;
        _curPos.y = GetParabolaXY(_startPos, _stopPos, height, _curDistance, out deltaDistance);
        // 旋转X轴
        if (rotateX > 0)
        {
            _curRotation.x = -rotateX * (1 + -2 * deltaDistance);
            transform.eulerAngles = _curRotation;
        }
        return _curPos;
    }
    /// <summary>
    /// 得到抛物线XY
    /// </summary>
    private float GetParabolaXY(Vector3 _startPos, Vector3 _endPos, float maxY, float _curDistance, out float deltaDistance)
    {
        float vX, vZ, a, b, apex, deltaHeight, y;
        vX = (_endPos - _startPos).x;
        vZ = (_endPos - _startPos).z;
        //
        a = -1;
        b = Mathf.Sqrt(vX * vX + vZ * vZ);
        apex = b / 2;
        deltaHeight = 1 / ((-apex) * (apex - b) / maxY);
        deltaDistance = _curDistance / b;
        y = 0/*x * (_endPos.y - _startPos.y) + _startPos.y*/;
        y += deltaHeight * (a * (_curDistance * _curDistance) + b * _curDistance);
        return y;
    }

    CoroutineUtil IEMoveUpdate;
    private Vector3 _startPos, _stopPos;    // 起始位置，目标位置  
    private float _angleToStop;     // 从起始点到目标点的角度
    private float _totalDistance, _curDistance;  // 总距离， 当前距离  
    private Vector3 _curRotation; // 当前的旋转角度  
    //


    /*
详解如何一元二次的实现抛物线效果：
y = a*x^2 + b*x
x是当前距离，y是当前高度，意思就是用当前距离来计算出当前高度。
第一步：
如果从起点到终点的距离 = z
该方程就会 = -x^2 + z*x
第二步：
当距离越来越大的时候你计算出的高度也会越来越高，下面就讲讲怎么设置高度限制
如果设置最高高度为 = h
首先：你应该计算出-x^2 + z*x的最高点 =
0 = -2x + z
x = z / 2
由此得出当x等于z / 2时,y便是该方程的最高点
然后：必须将该方程的最高点设为1
先将方程代入最高点 = z / 2，再除于k后便会得到1，现在计算出k的值
1 = (-(z / 2)^2 + z * (z / 2)) / k
k = 1 / (-(z / 2)^2 + z * (z / 2))
第三步：
设当前距离 = x
设最高高度为 = h
设总距离 = z
最后
当前高度 = h * k * -x^2 + z*x
*/
}
