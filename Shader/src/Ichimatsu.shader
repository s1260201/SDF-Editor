Shader "JPShader/Ichimatsu"
{
    Properties
    {
        [HideInInspector] _MainTex ("Texture", 2D) = "white" {}
        _Rep ("Repetition", Float) = 10.0
        col1 ("Color1", Color) = (1.0, 1.0, 1.0, 1.0)
        col2 ("Color2", Color) = (0.0, 0.0, 0.0, 1.0)
        //_Shading ("Shading", Range(0, 1)) = 0.1
        xm ("X-axis scroll", Float) = 0.1
        ym ("Y-axis scroll", Float) = 0.1

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
            float4 col1;
            float4 col2;
            float _Rep;
            float xm;
            float ym;
            //float _Shading;


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
                //fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                int2 uv_int;
                float2 uv_float = modf((i.uv += float2(_Time.x * xm, _Time.x * ym)) * _Rep, uv_int);
   

                int j = fmod(uv_int.x + uv_int.y ,2);
                /*
                step(uv_float.x, 0.5) * (1.0 - (_Shading * (1.0-uv_float.x)/(1.0-_Shading)));
                +  (1 - step(uv_float.x, 0.5) * 0.5 * (uv_float.x / _Shading))
                */

                //float sha = step( abs(uv_float.x - round(uv_float.x)), _Shading) * (1 - abs(round(uv_float.x) - uv_float.x)/_Shading) * 0.5 * (2 * fmod(round(uv_float.x - 0.25),2) - 1) + 0.5;
                // step(abs(frac(x)-round(frac(x))),0.2) * ((1 - abs(round(x) - x) / 0.2) * 0.5 * (2 * mod(round(frac(x/2-0.25)), 2) -1) )  
                //uv_float.x *= 2;
                //float sha = step(abs(uv_float.x-round(uv_float.x)),_Shading) * ((1 - abs(round(uv_float.x) - uv_float.x) / _Shading) * 0.5 * (2 * fmod(round(frac(uv_float.x/2-0.25)), 2) -1) )  +0.5;

                //sha = step(_Shading,  abs(uv_float.x - round(uv_float.x))) * j;

             
                return lerp(col1,  col2, j);
            }
            ENDCG
        }
    }
}
