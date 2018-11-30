using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class NativePluginImporter : MonoBehaviour
{
	public static void FillTexture(System.IntPtr unityTexture, float x, float y, float z, float w)
	{
		_funcFillTexture?.Invoke(unityTexture, x, y, z, w);
	}


	private DllManager _dll = null;

	private delegate void FnFillTexture(System.IntPtr unityTexture, float x, float y, float z, float w);
	private static FnFillTexture _funcFillTexture = null;

	private void Awake()
	{
		_dll = new DllManager(@"Assets/Plugins/x86_64/MainPlugin.dll");
		_funcFillTexture = _dll.GetDelegate<FnFillTexture>("FillTexture");

		DontDestroyOnLoad(this);
	}

	private void OnDestroy()
	{
		_dll?.Dispose();
		_dll = null;
		_funcFillTexture = null;
	}
}
