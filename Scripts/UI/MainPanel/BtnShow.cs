
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/29 15:50:34
//Note:     
//--------------------------------------------------------------


using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public partial class NewMainPanel
{
    private void BtnShow(List<MainPanelDefine> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            GetBtnObjByType(list[i]).SetActive(true);
        }
    }
}

