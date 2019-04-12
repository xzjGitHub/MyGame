using UnityEngine.UI;

public class GmPanel: UIPanelBehaviour
{
    public InputField IdInput;
    public InputField NumInput;
    public InputField CharInput;

    private void Awake()
    {
        if(IdInput == null)
        {
            IdInput = transform.Find("IdInput").GetComponent<InputField>();
        }
        if(NumInput == null)
        {
            NumInput = transform.Find("NumInput").GetComponent<InputField>();
        }
        if(CharInput == null)
        {
            CharInput = transform.Find("CharInput").GetComponent<InputField>();
            transform.Find("CloseBtn").GetComponent<Button>().onClick.AddListener(OnClickClose);
            transform.Find("Button").GetComponent<Button>().onClick.AddListener(OnClickAddClick);
        }

        CharInput.text = string.Empty;
        NumInput.text = string.Empty;
        IdInput.text = string.Empty;
    }

    public void OnClickAddClick()
    {
        if(!string.IsNullOrEmpty(IdInput.text) && !string.IsNullOrEmpty(NumInput.text))
        {
            AddItem();
        }

        if(!string.IsNullOrEmpty(CharInput.text))
        {
            AddChar();
        }
    }


    private void AddItem()
    {
        int id = int.Parse(IdInput.text);
        int num = int.Parse(NumInput.text);
        if(Item_instanceConfig.GetItemInstance(id) == null)
        {
            TipManager.Instance.ShowTip("请输入正确的id");
            return;
        }
        if(num <= 0 || num > 999999)
        {
            TipManager.Instance.ShowTip("请输入正确的数量:0到999999");
            return;
        }
        ItemSystem.Instance.CreateItem(id,num,true);
        UIPanelManager.Instance.Hide<GmPanel>();
        TipManager.Instance.ShowTip("添加成功");
    }

    private void AddChar()
    {
        if(string.IsNullOrEmpty(CharInput.text))
        {
            return;
        }

        int id = int.Parse(CharInput.text);
        if(Char_templateConfig.GetTemplate(id) == null)
        {
            TipManager.Instance.ShowTip("请输入正确的id");
        }
        else
        {
            CharSystem.Instance.CreateChar(new CharCreate(id));
            TipManager.Instance.ShowTip("添加角色成功");
        }
    }

    public void OnClickClose()
    {
        UIPanelManager.Instance.Hide<GmPanel>();
    }
}
