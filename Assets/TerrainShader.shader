Shader "Custom/TerrainShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        sampler2D terrainGradient;
        sampler2D _MainTex;
        float minTerrainHeight;
        float maxTerrainHeight;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 worldPosY = IN.worldPos.y;

            float heightValue = saturate((worldPosY - minTerrainHeight) / (maxTerrainHeight - minTerrainHeight)); 

            float4 gradientColor = tex2D(terrainGradient, float2(0, heightValue));

            o.Albedo = gradientColor.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}