// MainPlugin.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"

#ifdef __cplusplus
extern "C" {
#endif

IUnityInterfaces *g_unityInterfaces = nullptr;

int32_t UNITY_INTERFACE_EXPORT UNITY_INTERFACE_API Sum(int32_t a, int32_t b)
{
	return a + b;
}

void UNITY_INTERFACE_EXPORT UNITY_INTERFACE_API UnityPluginLoad(IUnityInterfaces* unityInterfaces)
{
	g_unityInterfaces = unityInterfaces;
}

void UNITY_INTERFACE_EXPORT UNITY_INTERFACE_API UnityPluginUnload()
{
	g_unityInterfaces = nullptr;
}

#ifdef __cplusplus
}
#endif


