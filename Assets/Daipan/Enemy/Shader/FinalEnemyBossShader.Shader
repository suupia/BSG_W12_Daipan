Shader "Daipan/FinalBoss"
{
    Properties
    {
        _MainColor ("MainColor", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Noise", 2D) = "white" {}
        _Slider ("Slider",Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" "RenderType" = "UniversalPipeline"}

        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"


            struct appdata
            {
                float4 vertex : POSITION;
                float4 color    : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 color    : COLOR;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _NoiseTex;
            float _Slider;
            float4 _MainColor;

            // 透明
            float Dissolve(float2 uv)
            {
                return 1 - step(tex2D(_NoiseTex, uv).y , _Slider - 0.1);
            }

            // 縁
            float Edge(float2 uv)
            {
                return step(tex2D(_NoiseTex, uv).x , _Slider);
            }

            v2f vert (appdata v)
            {
                v2f o;
                VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
                o.vertex = vertexInput.positionCS;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float4 col = tex2D(_MainTex, uv) * i.color;
                float alpha = col.a;
                uv.y -= frac(_Time.x);
                uv.y = 1 - uv.y;
                uv /= 10;
                col = col + (_MainColor - col) * Edge(uv);
                col *= Dissolve(uv);
                col *= alpha;
                return col;
            }
            ENDHLSL
        }
    }
}
