// MainPlugin.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"

template<class Intf>
using ComPtr = _com_ptr_t<_com_IIID<Intf, &__uuidof(Intf)>>;

#ifdef __cplusplus
extern "C" {
#endif

ComPtr<ID3D11Device> g_device = nullptr;

void UNITY_INTERFACE_EXPORT UNITY_INTERFACE_API FillTexture(IUnknown *unityTexture, float x, float y, float z, float w)
{
	if (g_device == nullptr || unityTexture == nullptr)
	{
		return;
	}

	ComPtr<ID3D11DeviceContext> dc;
	ComPtr<ID3D11Texture2D> texture;
	ComPtr<ID3D11RenderTargetView> rtv;
	if (FAILED(unityTexture->QueryInterface(&texture)))
	{
		return;
	}

	g_device->GetImmediateContext(&dc);
	if (FAILED(g_device->CreateRenderTargetView(texture, &CD3D11_RENDER_TARGET_VIEW_DESC(D3D11_RTV_DIMENSION_TEXTURE2D, DXGI_FORMAT_R8G8B8A8_UNORM), &rtv)))
	{
		return;
	}

	float c[] = { x, y, z, w };
	dc->ClearRenderTargetView(rtv, c);
}

void UNITY_INTERFACE_EXPORT UNITY_INTERFACE_API UnityPluginLoad(IUnityInterfaces* unityInterfaces)
{
	g_device = nullptr;
	if (unityInterfaces != nullptr)
	{
		auto *p = unityInterfaces->Get<IUnityGraphicsD3D11>();
		if (p != nullptr)
		{
			g_device = p->GetDevice();
		}
	}
}

void UNITY_INTERFACE_EXPORT UNITY_INTERFACE_API UnityPluginUnload()
{
	g_device = nullptr;
}

#ifdef __cplusplus
}
#endif


