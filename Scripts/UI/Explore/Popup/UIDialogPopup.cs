using System;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogPopup : MonoBehaviour
{
    public Action callParam;
    public delegate void CallBack(int bountyID);

    public CallBack callAcceptBounty;

    public void OpenUI(int dialogID)
    {
        GetObj();
        InitInfo(dialogID);
        UpdateShow();
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 初始化信息
    /// </summary>
    /// <param name="dialogID"></param>
    private void InitInfo(int dialogID)
    {
        _dialogID = dialogID;
        _dialogIndex = 0;
        _dialogTemplate = Dialog_templateConfig.GetDialog_template(dialogID);
        if (_dialogTemplate == null)
        {
            _isLastDialog = true;
            return;
        }
        _isLastDialog = _dialogTemplate.textSet == null || _dialogTemplate.textSet.Count == 0;
    }
    /// <summary>
    /// 更新显示
    /// </summary>
    private void UpdateShow()
    {
        if (_dialogTemplate == null)
        {
            _iconImage.gameObject.SetActive(false);
            return;
        }
        //
        Text_template textTemplate = Text_templateConfig.GetText_config(_dialogTemplate.textSet[_dialogIndex]);
        // _iconImage.sprite=ResourceLoadUtil.LoadSprite(res)
        _introText.text = textTemplate.text;
        _iconImage.gameObject.SetActive(true);
    }

    private void OnClickButton()
    {
        //最后一个
        if (_isLastDialog)
        {
            if (callAcceptBounty!=null)
            {
                callAcceptBounty(_dialogTemplate.acceptBounty);
            }
            if (callParam != null)
            {
                callParam();
            }
            callAcceptBounty = null;
            callParam = null;
            gameObject.SetActive(false);
            return;
        }
        //
        _dialogIndex++;
        _isLastDialog = _dialogIndex == _dialogTemplate.textSet.Count - 1;
        UpdateShow();
    }

    private void GetObj()
    {
        if (_isFirst)
        {
            return;
        }
        //
        _iconImage = transform.Find("Icon").GetComponent<Image>();
        _introText = transform.Find("Icon/Text").GetComponent<Text>();
        _button = transform.GetComponent<Button>();
        //
        _button.onClick.AddListener(OnClickButton);
        //
        _isFirst = true;
    }

    //
    private bool _isFirst;
    private bool _isLastDialog;
    private int _dialogIndex;
    private int _dialogID;
    private Dialog_template _dialogTemplate;
    //
    private Image _iconImage;
    private Text _introText;
    private Button _button;
}
