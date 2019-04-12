using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{
    public InputField inputField;
    //强度
    public float m_intensity=0.3f;
    //每秒衰减
    public float m_decay = 0.8f;

    public void Shake()
    {
        if (isWork) return;
        //if( shake_intensity <= 0 ) {
        m_originPosition = transform.position;
        //originRotation = transform.rotation; 
        //}
        m_nowShakeIntensity = m_intensity;
        //skake_instensity / shake_decay = 震动时间
        //shake_intensity = .2f; 
        //shake_decay = 0.8f;
        m_coroutineUtil = new CoroutineUtil(ShakeCamera());
        isWork = true;
    }

    public void StopShake()
    {
        m_coroutineUtil.Stop();
    }

    private IEnumerator ShakeCamera()
    {
        m_intensity = (inputField == null ? 2 : int.Parse(inputField.text)) / 10f;
        while (m_nowShakeIntensity > 0)
        {
            yield return null;
            m_nowShakeIntensity -= m_decay * Time.deltaTime;
            m_nowShakeIntensity = m_nowShakeIntensity <= 0 ? 0 : m_nowShakeIntensity;
            var shakePosition = m_originPosition + Random.insideUnitSphere * m_nowShakeIntensity;
            transform.position = new Vector3(shakePosition.x, shakePosition.y, transform.position.z);
            //transform.rotation = new Quaternion( originRotation.x + Random.Range( -shake_intensity, shake_intensity ) * .2f, originRotation.y + Random.Range( -shake_intensity, shake_intensity ) * .2f, originRotation.z + Random.Range( -shake_intensity, shake_intensity ) * .2f, originRotation.w + Random.Range( -shake_intensity, shake_intensity ) * .2f );             
        }
        isWork = false;
    }

    //初始位置
    private Vector3 m_originPosition;
    //当前的强度
    private float m_nowShakeIntensity;
    //初始旋转
    //private Quaternion originRotation;
    private bool isWork;
    private CoroutineUtil m_coroutineUtil;
}