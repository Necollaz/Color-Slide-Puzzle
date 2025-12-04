Shader "Unlit/TileUnlitFakeLit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _ShadeStrength ("Shade Strength", Range(0,1)) = 0.35
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "Queue"="Geometry"
        }
        LOD 50

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _BaseColor;
            float _ShadeStrength;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = normalize(UnityObjectToWorldNormal(v.normal));
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 texCol = tex2D(_MainTex, i.uv);

                float3 lightDir = normalize(float3(0.3, 1.0, 0.4));
                float NdotL = saturate(dot(i.normal, lightDir));
                float shade = lerp(_ShadeStrength, 1.0, NdotL);

                return texCol * _BaseColor * shade;
            }
            ENDCG
        }
    }
}