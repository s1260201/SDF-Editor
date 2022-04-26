Shader "SDFE/Sample"
{
	Properties
	{
		_Color("Albedo Color", COLOR) = (1,1,1,1)

		_BlurShadow("BlurShadow", Range(0.0,50.0)) = 16.0
		_Metalmess("Metalness", Range(0.0,1.0))=0.5
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
			float _BlurShadow,_Metalmess;
			
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
float dist0 = sdBox(float3(pos.x - 0, pos.y -  0, pos.z - 0), float3(1,1,1));
float dist1 = sdBox(float3(pos.x - 1, pos.y -  1, pos.z - 0), float3(1,1,1));
float dist2 = sdRoundBox(float3(pos.x - -1, pos.y -  -1, pos.z - 0), float3(1,1,1),0.5);
dist = min(min(dist0,dist1),dist2);
				return dist;
			}
			float3 getNormal(float3 pos) {
				float d = 0.001;
				return normalize(float3(
					getSdf(pos + float3(d, 0, 0)) - getSdf(pos + float3(-d, 0, 0)), // x partial differentiation 偏微分
					getSdf(pos + float3(0, d, 0)) - getSdf(pos + float3(0, -d, 0)), // y
					getSdf(pos + float3(0, 0, d)) - getSdf(pos + float3(0, 0, -d))  // z
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

			float calcAO(float3 pos, float3 nor)
            {
                float occ = 0.0;
                float sca = 1.0;
                for (int i = 0; i < 5; i++)
                {
                    float h = 0.01 + 0.12 * float(i) / 4.0;
                    float d = getSdf(pos + h * nor).x;
                    occ += (h - d) * sca;
                    sca *= 0.95;
                    if (occ > 0.35) break;
                }
                return clamp(1.0 - 3.0 * occ, 0.0, 1.0) * (0.5 + 0.5 * nor.y);
            }

			float4 rayMarch(float3 pos, float3 rayDir, int StepNum){
				int fase = 0;
				float t = 0;
				float d = getSdf(pos);
				float3 col = float3(0.0,0.0,0.0);
				float3 lightCol = float3(1,1,1);

				while(fase < StepNum && abs(d) > 0.001){
					t += d;
					pos += rayDir * d;
					d = getSdf(pos);
					fase++;
				}

				if(step(StepNum,fase)){
					return float4(col,0);
				}else{
					// ライティングのパラメーター
                    float3 normal = getNormal(pos);// 法線
                    //float3 light = normalize(float3(1, 1, -1));// 平行光源の方向ベクトル
					float3 light = normalize(_WorldSpaceLightPos0);
                    
                    // マテリアルのパラメーター
                    float3 albedo = float3(_Color.r, _Color.g, _Color.b);// アルベド
                    float metalness = _Metalmess;// メタルネス（金属の度合い）

                    
                    // ライティング計算
                    float diffuse = saturate(dot(normal, light));// 拡散反射
                    float specular = pow(saturate(dot(reflect(light, normal), rayDir)), 10.0);// 鏡面反射 Phong
                    float ao = calcAO(pos, normal);// AO : Ambient occlusion
                    //float shadow = calcSoftshadow(pos, light, 0.25, 5);// シャドウ
                    float shadow = genShadow(pos, light);

                    // ライティング結果の合成
                    col += albedo * diffuse * shadow * (1 - metalness);// 直接光の拡散反射
                    col += albedo * specular * shadow * metalness;// 直接光の鏡面反射
                    col += albedo * ao * lerp(float4(0,0,0,0), float4(1,1,1,0), 0.3);// 環境光
					return float4(col,1);
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
