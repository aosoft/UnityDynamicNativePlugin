﻿//#define DO_NOT_CALL_UNITYPLUGINLOAD

using System;
using System.Runtime.InteropServices;

public class DllManager : IDisposable
{
	private IntPtr _dll = IntPtr.Zero;

	delegate void FnUnityPluginLoad(IntPtr unityInterfaces);
	delegate void FnUnityPluginUnload();

	public DllManager(string dllpath)
	{
		_dll = LoadLibrary(dllpath);

#if !DO_NOT_CALL_UNITYPLUGINLOAD
		if (_dll != IntPtr.Zero)
		{
			GetDelegate<FnUnityPluginLoad>("UnityPluginLoad")?.Invoke(GetUnityInterface());
		}
#endif
	}

	public void Dispose()
	{
		if (_dll != IntPtr.Zero)
		{
#if !DO_NOT_CALL_UNITYPLUGINLOAD
			GetDelegate<FnUnityPluginUnload>("UnityPluginUnload")?.Invoke();
#endif
			FreeLibrary(_dll);
			_dll = IntPtr.Zero;
		}
	}

	public TDelegate GetDelegate<TDelegate>(string procname)
	{
		if (_dll != IntPtr.Zero)
		{
			return Marshal.GetDelegateForFunctionPointer<TDelegate>(GetProcAddress(_dll, procname));
		}
		return default(TDelegate);
	}

	[DllImport("UnityInterfaceManager")]
	private static extern IntPtr GetUnityInterface();

	[DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
	private static extern IntPtr LoadLibrary(string lpFileName);

	[DllImport("kernel32", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool FreeLibrary(IntPtr hModule);

	[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
	private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
}