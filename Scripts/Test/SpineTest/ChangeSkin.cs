using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{

    public Texture2D t;
    public Texture2D t2;
    public string soltName = "wuqi2";

    // Use this for initialization  
    private IEnumerator Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        yield return new WaitForSeconds(0.2f);

        Material m = CreateMeshAttachmentByTexture(skeletonAnimation.skeleton.FindSlot(soltName), t2);
        //换下一张图片，如果是动态加载的图片，需要消除之前的DestroyImmediate(m.mainTexture,false);  
        m.mainTexture = t;

        //  List<string> names = GetAttachName(defaultSkinName, soltName);

        //  Attachment attachment = GetAttachment(18, names[0], defaultSkinName);

    }

    /// <summary>  
    /// Creates the region attachment by texture.  
    /// </summary>  
    /// <returns>The region attachment by texture.</returns>  
    /// <param name="slot">Slot.</param>  
    /// <param name="texture">Texture.</param>  
    public Material CreateRegionAttachmentByTexture(Spine.Slot slot, Texture2D texture)
    {
        if (slot == null)
        {
            return null;
        }

        Spine.RegionAttachment oldAtt = slot.Attachment as Spine.RegionAttachment;
        if (oldAtt == null || texture == null)
        {
            return null;
        }

        Spine.RegionAttachment att = new Spine.RegionAttachment(oldAtt.Name)
        {
            RendererObject = CreateRegion(texture),
            Width = oldAtt.Width,
            Height = oldAtt.Height,
            //  att.Offset = oldAtt.Offset;
            Path = oldAtt.Path,

            X = oldAtt.X,
            Y = oldAtt.Y,
            Rotation = oldAtt.Rotation,
            ScaleX = oldAtt.ScaleX,
            ScaleY = oldAtt.ScaleY
        };

        att.SetUVs(0f, 1f, 1f, 0f, false);

        Material mat = new Material(Shader.Find("Sprites/Default"))
        {
            mainTexture = texture
        };
        (att.RendererObject as Spine.AtlasRegion).page.rendererObject = mat;

        slot.Attachment = att;
        return mat;
    }

    /// <summary>  
    /// Creates the mesh attachment by texture.  
    /// </summary>  
    /// <returns>The mesh attachment by texture.</returns>  
    /// <param name="slot">Slot.</param>  
    /// <param name="texture">Texture.</param>  
    public Material CreateMeshAttachmentByTexture(Spine.Slot slot, Texture2D texture)
    {
        if (slot == null)
        {
            return null;
        }

        Spine.MeshAttachment oldAtt = slot.Attachment as Spine.MeshAttachment;
        if (oldAtt == null || texture == null)
        {
            return null;
        }

        Spine.MeshAttachment att = new Spine.MeshAttachment(oldAtt.Name)
        {
            RendererObject = CreateRegion(texture),
            Path = oldAtt.Path,

            Bones = oldAtt.Bones,
            Edges = oldAtt.Edges,
            Triangles = oldAtt.Triangles,
            Vertices = oldAtt.Vertices,
            WorldVerticesLength = oldAtt.WorldVerticesLength,
            HullLength = oldAtt.HullLength,
            RegionRotate = false,

            RegionU = 0f,
            RegionV = 1f,
            RegionU2 = 1f,
            RegionV2 = 0f,
            RegionUVs = oldAtt.RegionUVs
        };

        att.UpdateUVs();

        Material mat = new Material(Shader.Find("Sprites/Default"))
        {
            mainTexture = texture
        };
        (att.RendererObject as Spine.AtlasRegion).page.rendererObject = mat;

        slot.Attachment = att;
        return mat;
    }

    private Spine.AtlasRegion CreateRegion(Texture2D texture)
    {

        Spine.AtlasRegion region = new Spine.AtlasRegion
        {
            width = texture.width,
            height = texture.height,
            originalWidth = texture.width,
            originalHeight = texture.height,
            rotate = false,
            page = new Spine.AtlasPage
            {
                name = texture.name,
                width = texture.width,
                height = texture.height,
                uWrap = Spine.TextureWrap.ClampToEdge,
                vWrap = Spine.TextureWrap.ClampToEdge
            }
        };

        return region;
    }



    //Material m;
    //string slot;
    //Texture2D texture;
    //void SetSkin()
    //{
    //    SkeletonAnimation _skeletonAnimation = GetComponent<SkeletonAnimation>();
    //    m = CreateTextureSizeAttachmentByTexture(_skeletonAnimation.skeleton.FindSlot(slot), texture);
    //}


    public enum SkinName
    {
        A = 1,
        B = 2,
        C = 3,
    }


    [System.Serializable]
    public struct EquipmentAttach
    {
        public SkinName skinName;
        // [SpineSlot(dataField: EQ.EQUIP_SKELETON_DATA)]
        public string slotName;
    }

    private SkeletonAnimation skeletonAnimation;
    private string defaultSkinName = "default";
    /// <summary>
    /// 得到附件名字
    /// </summary>
    /// <param name="skinName">皮肤名字</param>
    /// <param name="slotName">位置名字</param>
    /// <returns></returns>
    public List<string> GetAttachName(string skinName, string slotName)
    {
        ExposedList<Slot> slots = skeletonAnimation.skeleton.Slots;
        int index = 0;
        List<string> names = new List<string>();
        for (int i = 0; i < slots.Count; i++)
        {
            Slot tempSlot = slots.Items[i];
            if (tempSlot.Data.Name == slotName)
            {
                index = i;
            }
        }
        skeletonAnimation.skeleton.Data.FindSkin(skinName).FindNamesForSlot(index, names);
        return names;
    }

    /// <summary>
    /// 得到附件
    /// </summary>
    /// <param name="slotIndex">位置索引</param>
    /// <param name="attachmentName">附件名字</param>
    /// <param name="skinName">皮肤名字</param>
    /// <returns></returns>
    public Attachment GetAttachment(int slotIndex, string attachmentName, string skinName)
    {
        Skin targetSkin = skeletonAnimation.skeleton.Data.FindSkin(skinName);
        Dictionary<Skin.AttachmentKeyTuple, Attachment> attachments = targetSkin.Attachments;
        Attachment attachment;
        attachments.TryGetValue(new Skin.AttachmentKeyTuple(slotIndex, attachmentName), out attachment);
        return attachment;
    }
    /// <summary>
    /// 设置皮肤
    /// </summary>
    /// <param name="dynamicSkin">动态皮肤</param>
    /// <param name="slotName">位置名字</param>
    /// <param name="targetSkinName">目标皮肤名字</param>
    /// <param name="attachName">依附名字</param>
    public void SetSkin(Skin dynamicSkin, string slotName, string targetSkinName, string attachName)
    {
        Slot changeSlot = skeletonAnimation.skeleton.FindSlot(slotName);

        if (dynamicSkin != null)
        {
            Skin targetSkin = skeletonAnimation.skeleton.Data.FindSkin(targetSkinName);

            ExposedList<Slot> slots = skeletonAnimation.skeleton.Slots;
            for (int i = 0; i < slots.Count; i++)
            {
                Slot tempSlot = slots.Items[i];
                if (tempSlot.Data.Name == changeSlot.Data.Name)
                {
                    Attachment targetAttachment;

                    targetAttachment = GetAttachment(i, attachName, targetSkinName);
                    changeSlot.Attachment = targetAttachment;
                    dynamicSkin.Attachments[new Skin.AttachmentKeyTuple(i, attachName)] = targetAttachment;
                    tempSlot = changeSlot;
                }
            }
            //skeletonAnimation.skeleton.SetSlots(slots);
        }
        skeletonAnimation.skeleton.Skin = dynamicSkin;
    }

    /// <summary>
    /// 重置皮肤
    /// </summary>
    /// <param name="dynamicSkin">动态皮肤</param>
    /// <param name="slotName">位置名字</param>
    /// <param name="targetSkinName">目标皮肤名字</param>
    /// <param name="attachName">依附名字</param>
    public void ResetSkin(Skin dynamicSkin, string slotName, string targetSkinName, string attachName)
    {
        Slot changeSlot = skeletonAnimation.skeleton.FindSlot(slotName);

        if (dynamicSkin != null)
        {
            Skin targetSkin = skeletonAnimation.skeleton.Data.FindSkin(targetSkinName);

            ExposedList<Slot> slots = skeletonAnimation.skeleton.Slots;
            for (int i = 0; i < slots.Count; i++)
            {
                Slot tempSlot = slots.Items[i];
                if (tempSlot.Data.Name == changeSlot.Data.Name)
                {
                    dynamicSkin.Attachments[new Skin.AttachmentKeyTuple(i, attachName)] = null;
                    tempSlot = changeSlot;
                }
            }
           // skeletonAnimation.skeleton.SetSlots(slots);
        }
        skeletonAnimation.skeleton.Skin = dynamicSkin;
    }

    /// <summary>
    /// 换装调用函数
    /// </summary>
    /// <param name="attach"></param>
    public void UpgradeEquipmentAttach(EquipmentAttach[] attach)
    {

        for (int i = 0; i < attach.Length; i++)
        {
            List<string> attachName = GetAttachName(attach[i].skinName.ToString(), attach[i].slotName);
            for (int j = 0; j < attachName.Count; j++)
            {
                SetSkin(skeletonAnimation.skeleton.Data.FindSkin(defaultSkinName.ToString()), attach[i].slotName, attach[i].skinName.ToString(), attachName[j]);
            }
        }
    }

}