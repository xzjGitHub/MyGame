using UnityEngine;
using System.Collections;
using Spine.Unity;
using UnityEngine.UI;

public class TestEffect : MonoBehaviour
{
    public string name1 = "1";
    public string name2 = "4";
    public string name3 = "qiu";
    public SkeletonAnimation skeletonAnimation;
    public Button button;

    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    private void OnClick()
    {
        skeletonAnimation.state.SetAnimation(0, name1, false);
        skeletonAnimation.state.SetAnimation(1, name2, false);
        skeletonAnimation.state.SetAnimation(2, name3, false);
    }
}
