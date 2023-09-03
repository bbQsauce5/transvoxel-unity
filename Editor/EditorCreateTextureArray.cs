using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorCreateTextureArray : Editor
{
    [MenuItem("GameObject/Create/TextureArray", false, 10)]
    static void CreateTextureArray() {
        var selection = Selection.objects;

        var textures = new List<Texture2D>();
        foreach (var obj in selection) {
            var path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path)) {
                var tex2D = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                if (tex2D != null) {
                    Debug.Log("Tex not null! path: " + path);
                    textures.Add(tex2D);
                }
            }
        }

        if (textures.Count <= 0) {
            Debug.Log("No texture selected.");
            return;
        }

        var texArray = new Texture2DArray(textures[0].width, textures[0].height, textures.Count, textures[0].format, true);

        for (int i = 0; i < textures.Count; i++) {
            texArray.SetPixels(textures[i].GetPixels(), i);
        }

        texArray.Apply();

        AssetDatabase.CreateAsset(texArray, "Assets/texArray.asset");
    }
}
