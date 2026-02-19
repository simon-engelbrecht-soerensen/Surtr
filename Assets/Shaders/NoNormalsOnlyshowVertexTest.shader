Shader "Custom/URP/NoNormals_VertexColorOnly_FullLights"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightCutoff ("Maximum distance", Float) = 2.0
        _Color ("Main Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            "RenderPipeline"="UniversalPipeline"
            "Queue"="Geometry"
        }

        // =====================================================
        // Forward Lit Pass
        // =====================================================
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            // Main light shadows
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE

            // Additional lights
            #pragma multi_compile _ _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _CLUSTER_LIGHT_LOOP
            
            #pragma multi_compile _ _SHADOWS_SOFT

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float3 positionWS : TEXCOORD1;
                float4 shadowCoord : TEXCOORD2;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float4 _MainTex_ST;
            float4 _Color;
            float _LightCutoff;

            Varyings vert (Attributes v)
            {
                Varyings o;

                float3 positionWS = TransformObjectToWorld(v.positionOS.xyz);

                o.positionCS = TransformWorldToHClip(positionWS);
                o.positionWS = positionWS;
                o.shadowCoord = TransformWorldToShadowCoord(positionWS);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;

                return o;
            }

            half3 ApplyLight(half3 albedo, Light lightData)
            {
                float atten = lightData.distanceAttenuation;
                atten = step(_LightCutoff, atten) * atten;

                return albedo *
                       lightData.color *
                       atten *
                       lightData.shadowAttenuation;
            }

            half4 frag (Varyings i) : SV_Target
            {
                half3 albedo = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv).rgb;
                albedo *= i.color.rgb;
                albedo *= _Color.rgb;

                half3 lighting = 0;

                // =========================
                // Main Light
                // =========================
                Light mainLight = GetMainLight(i.shadowCoord);
                lighting += ApplyLight(albedo, mainLight);

                InputData inputData = (InputData)0;
                inputData.positionWS = i.positionWS;
//                inputData.normalWS = i.normalWS;
                inputData.viewDirectionWS = GetWorldSpaceNormalizeViewDir(i.positionWS);
                inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(i.positionCS);

                 // Get additional lights
                #if defined(_ADDITIONAL_LIGHTS)

                // Additional light loop for non-main directional lights. This block is specific to Forward+.
                #if USE_CLUSTER_LIGHT_LOOP
                UNITY_LOOP for (uint lightIndex = 0; lightIndex < min(URP_FP_DIRECTIONAL_LIGHTS_COUNT, MAX_VISIBLE_LIGHTS); lightIndex++)
                {
                    Light additionalLight = GetAdditionalLight(lightIndex, inputData.positionWS, half4(1,1,1,1));
                    lighting += ApplyLight(albedo, additionalLight);
                }
                #endif
                
                // Additional light loop.
                uint pixelLightCount = GetAdditionalLightsCount();
                LIGHT_LOOP_BEGIN(pixelLightCount)
                    Light additionalLight = GetAdditionalLight(lightIndex, inputData.positionWS, half4(1,1,1,1));
                    lighting += ApplyLight(albedo, additionalLight);
                LIGHT_LOOP_END
                
                #endif

                return half4(lighting, 1);
            }

            ENDHLSL
        }

        // =====================================================
        // Shadow Caster Pass
        // =====================================================
        Pass
        {
            Name "ShadowCaster"
            Tags { "LightMode"="ShadowCaster" }

            ZWrite On
            ZTest LEqual
            ColorMask 0
            Cull Back

            HLSLPROGRAM

            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };

            Varyings ShadowPassVertex(Attributes v)
            {
                Varyings o;

                float3 positionWS = TransformObjectToWorld(v.positionOS.xyz);
                float3 normalWS = float3(0,1,0); // fake normal since we don't use normals

                o.positionCS = TransformWorldToHClip(
                    ApplyShadowBias(positionWS, normalWS, 0)
                );

                return o;
            }

            half4 ShadowPassFragment(Varyings i) : SV_Target
            {
                return 0;
            }

            ENDHLSL
        }
    }
}
