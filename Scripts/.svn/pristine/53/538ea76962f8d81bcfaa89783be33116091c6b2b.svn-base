using UnityEngine;
using UnityEngine.UI;

public class UIBountyPopup : MonoBehaviour
{
    public void OpenUI()
    {
        GetObj();
        UpdateShow();
        gameObject.SetActive(true);
    }


    private void UpdateShow()
    {
        _mainBounty.OpenUI(BountySystem.Instance.MainBountyAttributes.Find(a => a.BountyState == BountyState.Accepted), BuountyUIState.Hidden);
        _randomBounty.OpenUI(BountySystem.Instance.RandomBounty, BuountyUIState.Hidden);
    }

    /// <summary>
    /// 点击了遮罩
    /// </summary>
    private void OnClickMask()
    {
        gameObject.SetActive(false);
    }

    private void GetObj()
    {
        if (_isFirst)
        {
            return;
        }

        _maskButton = transform.Find("Back").GetComponent<Button>();
        _maskButton.onClick.AddListener(OnClickMask);
        //
        _mainBounty = transform.Find("Main").gameObject.AddComponent<UIBountyInfo>();
        _randomBounty = transform.Find("Random").gameObject.AddComponent<UIBountyInfo>();
        //
        _isFirst = true;
    }

    //
    private Button _maskButton;
    //
    private bool _isFirst;
    //
    private UIBountyInfo _mainBounty;
    private UIBountyInfo _randomBounty;

}
