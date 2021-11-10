Shader "Unlit/ASCIlart"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Width("Width", float) = 1024
        _Height("Height", float) = 768
        _CellHeight("Cell Height", float) = 5
        _CellWidth("Cell Width", float) = 5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
            float _Height;
            float _Width;
            float _CellWidth;
            float _CellHeight;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float2 centralCellPos(float2 uv)
            {
                float pixelX = ceil(uv.x * _Width);
                float pixelY = ceil(uv.y * _Height);

                float halfCellX = ceil(_CellWidth / 2);
                float halfCellY = ceil(_CellHeight / 2);

                float xColorPos = (ceil(pixelX / _CellWidth) * _CellWidth + halfCellX) / _Width;
                float yColorPos = (ceil(pixelY / _CellHeight) * _CellHeight + halfCellY) / _Height;

                return float2(xColorPos, yColorPos);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 cellUV = centralCellPos(i.uv);
                fixed4 col = tex2D(_MainTex, cellUV);

                return col;
            }
            ENDCG
        }
    }
}
