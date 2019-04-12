using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TextureImport : AssetPostprocessor
{
    void ResoureceImport()
    {
        TextureImporter importer = (TextureImporter)assetImporter;
        importer.textureType = TextureImporterType.Sprite; 
        importer.npotScale = TextureImporterNPOTScale.ToNearest;
       // importer.generateCubemap = TextureImporterGenerateCubemap.None;
        importer.isReadable = false;
        importer.alphaSource = TextureImporterAlphaSource.None;
        importer.alphaIsTransparency = true;
        importer.sRGBTexture = true;
        importer.mipmapEnabled = false;
        importer.wrapMode = TextureWrapMode.Clamp;
        importer.filterMode = FilterMode.Bilinear;
        importer.anisoLevel = 2;
    }
}
