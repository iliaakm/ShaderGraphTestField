Shader "Unlit/NewUnlitShader"
{
    Properties
    {
         _MainTex("Albedo (RGB)", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}

        //Wave Properties
        _Direction("Direction", Vector) = (1.0, 0.0, 0.0, 0.0)
        _Steepness("Steepness", Range(0.1, 1.0)) = 0.5
        _Freq("Frequency", Range(1.0, 10.0)) = 1.0
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
            sampler2D _NormalMap;

            float4 _Direction;
            float _Steepness, _Freq;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);

                float3 pos = o.vertex.xyz;
                float4 dir = normalize(_Direction);

                float defaultWavelength = UNITY_TWO_PI;
                float wL = defaultWavelength / _Freq;

                float phase = sqrt(9.8 / wL);
                float disp = wL * (dot(dir, pos) - (phase * _Time.y));
                float peak = _Steepness / wL;

                pos.x += dir.x * (peak * cos(disp));
                pos.y = peak * sin(disp);
                pos.z += dir.y * (peak * cos(disp));

                o.vertex.xyz = pos;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
