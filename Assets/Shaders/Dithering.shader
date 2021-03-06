Shader "Unlit/Dithering"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PatternTex("Pattern Texture", 2D) = "white" {}
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _PatternTex;
            float4 _PatternTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 colScene = tex2D(_MainTex, i.uv);
                fixed4 colPattern = tex2D(_PatternTex, i.uv * _PatternTex_ST);
                float grayscaleScene = (colScene.r + colScene.g + colScene.b) / 3.0;
                
                float color = 0.0;
                if (grayscaleScene > colPattern.r)
                {
                    color = 1.0;
                }

                return fixed4(color, color, color, 1.0);
            }
            ENDCG
        }
    }
}
