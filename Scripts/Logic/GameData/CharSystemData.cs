using ProtoBuf;
using System.Collections.Generic;

[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class CharSystemData 
{
    public Dictionary<int, CharData> charDataList = new Dictionary<int, CharData>();
}
