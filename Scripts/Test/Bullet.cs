using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void OnClickButton(Transform target)
    {
        Fire(transform.localPosition, target.localPosition);
    }

    // Use this for initialization  
    void Start()
    {
        //// 開始測試  
        //Vector3 end = transform.position;
        //end.x = end.x * -1;
        //Fire(transform.position, end);
    }

    // Update is called once per fram  
    void Update()
    {
        if (_isFiring)
        {
            //Vector3 center = sunrise.localPosition + sunset.localPosition * 0.5F;
            //center -= new Vector3(0, 1, 0);
            //Vector3 riseRelCenter = sunrise.localPosition - center;
            //Vector3 setRelCenter = sunset.localPosition - center;
            //transform.localPosition = Vector3.Slerp(riseRelCenter, setRelCenter, Time.time);
            //transform.localPosition += center;
            UpdateArrow();
        }
    }

    // 所有變量  
    public float rotateX = 45;      // 箭的最大X軸旋轉角度  
    public float speed = 10;        // 箭的速度  
    public float height = 10;       // 箭的最大高度   
    private Vector3 _startPos, _stopPos, _curPos;    // 起始位置，目標位置，當前位置  
    private float _angleToStop;     // 從起始點到目標點的角度  
    private float _startHeight, _stopHeight; // 起始高度，結束高度  
    private bool _isFiring = false;     //判斷箭是否正在移動  
    private float _totalDistance, _curDistance;  // 總距離， 當前距離  
    private Vector3 _curRotation; // 當前的旋轉角度  

    // 發射函數，你只要調用這一個函數就能發射箭了  
    public void Fire(Vector3 start, Vector3 stop)
    {
        _startPos = start;
        _stopPos = stop;
        _angleToStop = GetAngleToStop(start, stop); // 計算 起始位置 到 目標位置的角度  
        _startHeight = start.y;
        _stopHeight = stop.y;
        _curDistance = 0;

        // 計算總距離  
        Vector3 v = _stopPos - _startPos;
        _totalDistance = Mathf.Sqrt(v.x * v.x + v.z * v.z);

        // 設置當前位置  
        transform.position = start;
        _curPos = start;

        // 設置當前X，Y軸的旋轉角度  
        Vector3 rotation = transform.eulerAngles;
        //if (rotateX > 0)
        //{
        //    rotation.x = -rotateX;
        //}
        //  rotation.y = _angleToStop;
        if (rotateX > 0)
        {
            rotation.z = -rotateX;
        }
         rotation.y = 0;

        transform.eulerAngles = rotation;
        _curRotation = rotation;

        // 設置判斷爲發射狀態，讓Update函數能夠更新  
        _isFiring = true;
    }

    // 計算 起始位置 到 目標位置的角度  
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

    // 計算X,Z軸移動向量，然後再把它們乘移動距離，這樣就能移動到下一個位置  
    private float deltaX;
    private float deltaZ;
    private float delta;
    float h, deltaDistance;


    // 更新箭到下一個位置  
    private void SetNextStep()
    {
        // 計算X,Z軸移動向量，然後再把它們乘移動距離，這樣就能移動到下一個位置  
        deltaX = Mathf.Sin(_angleToStop * Mathf.Deg2Rad);
        deltaZ = Mathf.Cos(_angleToStop * Mathf.Deg2Rad);
        delta = speed * Time.deltaTime;
        _curPos.x += deltaX * delta;
        _curPos.z += deltaZ * delta;
        // 增加當前距離，用來判斷是否到達終點了  
        _curDistance += delta;

        /************************************************/
        // 計算出當前的高度  
        // 這個是一元二次方程(ax^2 + bx)，大家都知道它是一條拋物線的方程，也是弓箭軌道最重要的地方。  
        // 我會在下面跟大家詳解如果運用簡單的一元二次方程來做弓箭的拋物線效果  
        /************************************************/
        Get(_startPos, _stopPos, height, _curDistance, out h, out deltaDistance);
        //float a = -1;
        //float b = _totalDistance;
        //float apex = _totalDistance / 2;
        //float deltaHeight = 1 / ((-apex) * (apex - _totalDistance) / height);
        //float deltaDistance = _curDistance / _totalDistance;
        //float h = deltaDistance * (_stopHeight - _startHeight) + _startHeight;
        //h += deltaHeight * (a * (_curDistance * _curDistance) + b * _curDistance);
        _curPos.y = h;

        // 更新當前箭的位置  
        transform.localPosition = _curPos;

        //// 旋轉X軸  
        //if (rotateX > 0)
        //{
        //    _curRotation.x = -rotateX * (1 + -2 * deltaDistance);
        //    transform.eulerAngles = _curRotation;
        //}      
        // 旋轉z軸  
        if (rotateX > 0)
        {
            _curRotation.z = -rotateX * (1 + -2 * deltaDistance);
            transform.eulerAngles = _curRotation;
        }
    }


    private float vX;
    private float vZ;
    //
    private float a;
    private float b;
    private float apex;
    private float deltaHeight;
    private void Get(Vector3 _startPos, Vector3 _endPos, float maxY, float _curDistance, out float y, out float x)
    {
        vX = (_endPos - _startPos).x;
        vZ = (_endPos - _startPos).z;
        //
        a = -1;
        b = Mathf.Sqrt(vX * vX + vZ * vZ);
        apex = b / 2;
        deltaHeight = 1 / ((-apex) * (apex - b) / maxY);
        x = _curDistance / b;
        y = 0/*x * (_endPos.y - _startPos.y) + _startPos.y*/;
        y += deltaHeight * (a * (_curDistance * _curDistance) + b * _curDistance);
    }

    // 判斷是否到達  
    private bool IsArrived()
    {
        return _curDistance >= _totalDistance;
    }

    private void UpdateArrow()
    {
        SetNextStep();
        // 如果到達了目標地點就取消發射狀態  
        if (IsArrived())
        {
            _isFiring = false;
        }
    }


    public Transform sunrise;//起点  
    public Transform sunset;//落点  



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

    CoroutineUtil IEMove;
}
