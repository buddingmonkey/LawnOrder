// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:2,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:2478,x:33139,y:32724,varname:node_2478,prsc:2|emission-2954-OUT,alpha-1877-A;n:type:ShaderForge.SFN_Tex2d,id:1877,x:32602,y:32813,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_1877,prsc:2,tex:dedbc757397d241b5885450ec520c35d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:2890,x:32637,y:32491,ptovrint:False,ptlb:color,ptin:_color,varname:node_2890,prsc:2,glob:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:2954,x:32880,y:32690,varname:node_2954,prsc:2|A-2890-RGB,B-1877-RGB,T-7014-OUT;n:type:ShaderForge.SFN_Slider,id:9540,x:32260,y:32643,ptovrint:False,ptlb:colorOverlay,ptin:_colorOverlay,varname:node_9540,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_OneMinus,id:7014,x:32602,y:32644,varname:node_7014,prsc:2|IN-9540-OUT;proporder:1877-2890-9540;pass:END;sub:END;*/

Shader "Shader Forge/PlayerShader" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _color ("color", Color) = (1,0,0,1)
        _colorOverlay ("colorOverlay", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _color;
            uniform float _colorOverlay;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 emissive = lerp(_color.rgb,_MainTex_var.rgb,(1.0 - _colorOverlay));
                float3 finalColor = emissive;
                return fixed4(finalColor,_MainTex_var.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
