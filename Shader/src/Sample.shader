Shader "SDFE/Sample"
{
	Properties
	{
		_Color("Color", COLOR) = (1,1,1,1)

		_Radius("Radius", Range(0.0,1.0)) = 0.3
		_BlurShadow("BlurShadow", Range(0.0,50.0)) = 16.0
		_Speed("Speed", Range(0.0,10.0)) = 2.0
		_LightValue("LightLevel",Range(0.0,10.0))=1.25
	}
	SubShader
	{
                //衝突しないピクセルは透明
		Tags{ "Queue" = "Transparent" "LightMode"="ForwardBase"}
		LOD 100



		Pass
		{
			ZWrite On
                        //アルファ値が機能するために必要
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			//#include "AutoLight.cginc"

			fixed4 _Color;
			float _Radius,_BlurShadow,_Speed;
			float4 _LightValue;
			
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 pos : POSITION1;
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
			};
			

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
                                //ローカル→ワールド座標に変換
				o.pos = mul(unity_ObjectToWorld, v.vertex);
				o.uv = v.uv;
				//o.normal = UnityObjectToWorldNormal(v.normal);
				return o;
			}

			// *Sphere
			float sdSphere(float3 p, float r)
			{
				return length(p) - r;
			}

			// *Box
			float sdBox(float3 p, float3 b){
				float3 q = abs(p) - b;
				return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0);
			}

			// *RoundBox
			float sdRoundBox(float3 p, float3 b, float r){
				float3 q = abs(p) - b;
				return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0) - r;
			}

			float getSdf(float3 pos){
				
				float dist = 0;
				// SDF
				return dist;
			}
			float3 getNormal(float3 pos) {
				float d = 0.001;
				return normalize(float3(
					getSdf(pos + float3(d, 0, 0)) - getSdf(pos + float3(-d, 0, 0)),
					getSdf(pos + float3(0, d, 0)) - getSdf(pos + float3(0, -d, 0)),
					getSdf(pos + float3(0, 0, d)) - getSdf(pos + float3(0, 0, -d))
				));
			}

			float genShadow(float3 pos, float3 lightDir){
				float marchingDist = 0.0;
				float c = 0.001;
				float r = 1.0;
				float shadowCoef = 0.5;
				for(float t = 0.0; t < 50.0; t++){
					marchingDist = getSdf(pos + lightDir * c);
					if(marchingDist < 0.001){
						return shadowCoef;
					}
					r = min(r, marchingDist * _BlurShadow / c);
					c += marchingDist;
				}
				return 1.0 - shadowCoef + r * shadowCoef;
			}
			/*
			float4 lighting(float3 pos)
			{	
				float3 mpos =pos;
				float3 normal =getnormal(mpos);
				
				pos =  mul(unity_ObjectToWorld,float4(pos,1)).xyz;
				normal =  normalize(mul(unity_ObjectToWorld,float4(normal,0)).xyz);
					
				float3 viewdir = normalize(pos-_WorldSpaceCameraPos);
				half3 lightdir = normalize(UnityWorldSpaceLightDir(pos));				
				float sha = softray(mpos,lightdir,3.3);
				float4 color = material(mpos);
				
				float NdotL = max(0,dot(normal,lightdir));
				float3 R = -normalize(reflect(lightdir,normal));
				float3 spec =pow(max(dot(R,-viewdir),0),10);

				float4 col =  sha*(color* NdotL+float4(spec,0));
				return col;
			}*/

			float4 rayMarch(float3 pos, float3 rayDir, int StepNum){
				int fase = 0;
				float t = 0;
				float d = getSdf(pos);
				float3 lightCol = float3(1,1,1);

				while(fase < StepNum && abs(d) > 0.001){
					t += d;
					pos += rayDir * d;
					d = getSdf(pos);
					fase++;
				}

				if(step(StepNum,fase)){
					return float4(1,1,1,0);
				}else{
					//ライティング
					float3 lightDir = _WorldSpaceLightPos0.xyz;
					float3 normal = getNormal(rayDir);
					float lightValue = _LightValue;
					float posonray = mul(unity_WorldToObject,float4(_WorldSpaceCameraPos,1.0));
					//float3 lightColor = _LightColor0;
					float4 col = float4(0.0,0.0,0.0,0.0);
					float diff = clamp(dot(lightDir,normal),0.1,1.0);
					col = _Color * diff * lightValue;
					//ソフトシャドウ
					//float shadow = genShadow(pos + normal * 0.001, lightDir);

					//fixed4 col = fixed4(lightColor * max(dot(normal, lightDir), 0) * max(0.5, shadow), 1.0);
					//col.rgb += fixed3(0.2f, 0.2f, 0.2f);
					return col;
					//色を乗算
				}				
			}		

			fixed4 frag(v2f i) : SV_Target
			{
				// レイの初期位置(ピクセルのワールド座標)
				float3 pos = i.pos.xyz;
				// レイの進行方向
				float3 rayDir = normalize(pos.xyz - _WorldSpaceCameraPos);
				
				int StepNum = 50;
				float4 col = rayMarch(pos,rayDir,StepNum);
				return col;
			}
			ENDCG
		}
	}
}