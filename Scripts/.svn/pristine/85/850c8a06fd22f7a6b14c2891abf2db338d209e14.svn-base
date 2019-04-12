using UnityEngine;
using UnityEngine.UI;

public class UIEnergyInfo : MonoBehaviour
{
    /// <summary>
    /// 更新显示
    /// </summary>
    public void UpdateShow(int _currentValue, int maxValue)
    {
        Init();
        //更新能量显示
        text.text = _currentValue + "/" + maxValue;
        valueImage.fillAmount = (float) _currentValue/maxValue;
        //
        gameObject.SetActive(true);
    }



    private void Init()
    {
        if (isFirst)
        {
            return;
        }
        //
        valueImage = transform.Find("Value").GetComponent<Image>();
        text = transform.Find("Text").GetComponent<Text>();
        //
        isFirst = true;
    }



    //
    private Image valueImage;
    private Text text;
    //
    private bool isFirst;
}
