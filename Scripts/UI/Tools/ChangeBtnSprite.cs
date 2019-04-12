using UnityEngine;

public class ChangeBtnSprite: MonoBehaviour
{

    [SerializeField]
    public GameObject NormalSprite;

    [SerializeField]
    public GameObject ClickSpriteSprite;

    private void Start()
    {
        UpdateSpriteShow(false);
    }


    public void OnPointerDownCallBack()
    {
        UpdateSpriteShow(true);
    }

    public void OnPointerUpCallBack()
    {
        UpdateSpriteShow(false);
    }


    private void UpdateSpriteShow(bool click)
    {
        NormalSprite.SetActive(!click);
        ClickSpriteSprite.SetActive(click);
    }
}
