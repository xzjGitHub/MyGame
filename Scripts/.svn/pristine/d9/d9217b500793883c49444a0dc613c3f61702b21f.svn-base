using UnityEngine;
using System.Collections;

public class Radar : MonoBehaviour
{
    public Transform target;
    public float speed = 10;
    private float distanceToTarget;
    private bool move = true;


    private float Test()
    {
        float angle;
        Vector3 velocity = Quaternion.Inverse(transform.rotation) * target.position; //对目标向量进行反向旋转，得到的新向量与z轴的夹角即为目标向量与当前物体方向的夹角          
         angle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg; //返回tan值为x/z的角的弧度，再转化为度数。
        return angle;



        float dot = Vector3.Dot(transform.forward, target.forward);
         angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return angle;
        //  float angle = Vector3.Angle(transform.position, target.position); //求出两向量之间的夹角  
        //  Vector3 normal = Vector3.Cross(transform.position, target.position);//叉乘求出法线向量  
        //  angle *= Mathf.Sign(Vector3.Dot(normal, upVector));  //求法线向量与物体上方向向量点乘，结果为1或-1，修正旋转方向  


        Vector3 targetDir = target.position - transform.position; // 目标坐标与当前坐标差的向量
        return Vector3.Angle(transform.forward, targetDir); // 返回当前坐标与目标坐标的角度
    }








    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, -Test());

        //  distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);
        //  StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {

        while (move)
        {
            Vector3 targetPos = target.transform.position;
            //朝向目标  (Z轴朝向目标)  
            this.transform.LookAt(targetPos);
            //根据距离衰减 角度  
            float angle = Mathf.Min(1, Vector3.Distance(this.transform.position, targetPos) / distanceToTarget) * 45;
            //旋转对应的角度（线性插值一定角度，然后每帧绕X轴旋转）  
            this.transform.rotation = this.transform.rotation * Quaternion.Euler(0, 0, Mathf.Clamp(-angle, -42, 42));
            //Vector3 _temp = this.transform.rotation.eulerAngles;
            //_temp = new Vector3(_temp.x, 0, _temp.z);
            //this.transform.eulerAngles = _temp;
            //当前距离目标点  
            float currentDist = Vector3.Distance(this.transform.position, target.transform.position);
            if (currentDist < 0.5f)
            {
                move = false;
            }
            //平移 （朝向Z轴移动）  
            this.transform.Translate(Vector3.forward * Mathf.Min(speed * Time.deltaTime, currentDist));
            yield return null;
        }
    }
}