
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/29 9:24:58
//Note:     
//--------------------------------------------------------------

using I2.TextAnimation;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 
/// </summary>
public class AlphaAnimCreator: ITextAnimCreator
{
    public void CreateTextAnim(TextAnimation textAnim,int animCount,List<float> time)
    {
        SE_AnimationSlot sl = new SE_AnimationSlot();
        sl._LocalSerializedData = "aa";
        for(int i = 0; i < animCount; i++)
        {
            SE_Animation anim = sl._Animation;
            SE_AnimSequence_Alpha sa = new SE_AnimSequence_Alpha();
            SE_AnimSequence sequence = sa as SE_AnimSequence;

            sequence._Name = "myANim" + i;
            sequence._Separation = 0.15f;
            sequence._InitAllElements = true;
            sequence._Duration = 5f;
            sequence._Playback = SE_Animation.ePlayback.Single;
            var list = anim._Sequences.ToList();
            list.Add(sequence);
            anim._Sequences = list.ToArray();
        }

        var list1 = textAnim._AnimationSlots.ToList();
        list1.Add(sl);
        textAnim._AnimationSlots = list1.ToArray();
    }
}

