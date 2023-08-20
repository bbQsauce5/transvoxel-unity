using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FastNoiseLiteInstance))]
public class FastNoiseLiteInstanceEditor : Editor
{
	private FastNoiseLiteInstance _fastNoiseLiteInstance;
	private int _textureWidth;
	private int _textureHeight;

	private void Awake()
	{
		_fastNoiseLiteInstance = target as FastNoiseLiteInstance;
	}

	public override void OnInspectorGUI()
    {
		base.OnInspectorGUI();

		EditorGUILayout.Space();

		if(GUILayout.Button("Apply Settings"))
        {
			_fastNoiseLiteInstance.ApplySettings();
        }
    }

	public override bool HasPreviewGUI()
	{
		return true;
	}
	
	public override GUIContent GetPreviewTitle()
	{
		return new GUIContent("Noise Preview");
	}

	public override void DrawPreview(Rect previewArea)
	{
		if(_textureWidth == (int)previewArea.width || _textureHeight == (int)previewArea.height)
        {
			return;
        }

		int textureWidth = (int)previewArea.width;
		int textureHeight = (int)previewArea.height;
		
		var previewTexture = new Texture2D(textureWidth, textureHeight);
		var pixels = new Color[textureWidth * textureHeight];
		var noiseData = new float[textureWidth * textureHeight];

		float minValue = float.MaxValue;
		float maxValue = float.MinValue;

		for(int x = 0; x < textureWidth; ++x)
        {
			for(int y = 0; y < textureHeight; ++y)
            {
				float noiseValue = _fastNoiseLiteInstance.GetNoise(x, y);
				noiseData[y * textureWidth + x] = noiseValue;

				minValue = Mathf.Min(minValue, noiseValue);
				maxValue = Mathf.Max(maxValue, noiseValue);
            }
        }

		float valueRange = maxValue - minValue;

		for(int i = 0; i < noiseData.Length; ++i)
        {
			noiseData[i] = (noiseData[i] - minValue) / valueRange;
			pixels[i] = Color.white * noiseData[i];
        }

		previewTexture.SetPixels(pixels);
		previewTexture.filterMode = FilterMode.Bilinear;
		previewTexture.Apply();

		GUI.DrawTexture(previewArea, previewTexture, ScaleMode.StretchToFill, false);
	}
}
