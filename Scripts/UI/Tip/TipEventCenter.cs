using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class TipEventCenter:Singleton<TipEventCenter>
{
    private TipEventCenter()
    {
    }

    public Action CloseTipEvent;
    public void EmitCloseTipEvent()
    {
        if(CloseTipEvent != null)
        {
            CloseTipEvent();
        }
    }
}

