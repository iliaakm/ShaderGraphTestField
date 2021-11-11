Shader "Unlit/ASCIIart"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_ASCIIAtlas("ASCII Atlas", 2D) = "white" {}
		_Width("Width", float) = 1024
		_Height("Height", float) = 768
		_PixelSize("Pixel Size", int) = 5
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				sampler2D _ASCIIAtlas;
				float _Height;
				float _Width;
				int _PixelSize;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					return o;
				}

				float2 centralCellPos(float2 pixels)
				{
					float halfCellX = ceil(_PixelSize / 2);
					float halfCellY = ceil(_PixelSize / 2);

					float xColorPos = (ceil(pixels.x / _PixelSize) * _PixelSize + halfCellX) / _Width;
					float yColorPos = (ceil(pixels.y / _PixelSize) * _PixelSize + halfCellY) / _Height;

					return float2(xColorPos, yColorPos);
				}

				float2 getASCIIChar(float2 pixels, float4 col)
				{
					float2 pixelsIndex = float2(pixels.x % _PixelSize, pixels.y % _PixelSize);
					float3 grayscale = dot(col.rgb, float3(0.3, 0.59, 0.11));
					float gray = (grayscale.r + grayscale.g + grayscale.b) / 3.0;
					gray = clamp(gray, 0.0, 0.9);

					float offset = ceil(gray * 10.0) / 10.0;
					float2 percentPosition = float2(pixelsIndex.x / (10 *_PixelSize) + offset,
						pixelsIndex.y / (3 *_PixelSize));

					return percentPosition;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					float2 pixelPos = float2(ceil(i.uv.x * _Width), ceil(i.uv.y * _Height));

					float2 cellUV = centralCellPos(pixelPos);
					fixed4 col = tex2D(_MainTex, cellUV);

					float2 asciiPos = getASCIIChar(pixelPos, col);

					return tex2D(_ASCIIAtlas, asciiPos) * col;
				}
				ENDCG
			}
		}
}
