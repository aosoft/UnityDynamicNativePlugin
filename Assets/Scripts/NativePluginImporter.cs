using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class NativePluginImporter : MonoBehaviour
{
	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
	private static extern System.IntPtr LoadLibrary(string lpFileName);

	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool FreeLibrary(System.IntPtr hModule);

	[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
	private static extern System.IntPtr GetProcAddress(System.IntPtr hModule, string procName);

	delegate int FnSum(int a, int b);

	private System.IntPtr _dll = System.IntPtr.Zero;

	private void Awake()
	{
		_dll = LoadLibrary(@"Assets/Plugins/x86_64/MainPlugin.dll");
		var p = GetProcAddress(_dll, "Sum");
		var funcSum = Marshal.GetDelegateForFunctionPointer<FnSum>(p);

		Debug.Log(funcSum(10, 20));

		DontDestroyOnLoad(this);
	}

	private void OnDestroy()
	{
		if (_dll != System.IntPtr.Zero)
		{
			FreeLibrary(_dll);
			_dll = System.IntPtr.Zero;
		}
	}
}
