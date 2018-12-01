using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillTexture : MonoBehaviour
{
	public RenderTexture _texture;

	private float _time;

	private void Start()
	{
		_time = 0.0f;
	}

	// Update is called once per frame
	void Update()
	{
		if (_texture != null)
		{
			_time += Time.deltaTime * 0.5f;
			float c = _time - Mathf.Floor(_time);
			NativePluginImporter.FillTexture(_texture.GetNativeTexturePtr(), 0.0f, 0.0f, c, 1.0f);
		}
	}
}
