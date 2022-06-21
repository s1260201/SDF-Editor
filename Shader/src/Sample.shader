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

			float mod(float x, float y)
            {
                return x - y * floor(x / y);
            }

			float2 mod(float2 x, float2 y)
            {
                return x - y * floor(x / y);
            }

			float3 mod(float3 x, float3 y)
            {
                return x - y * floor(x / y);
            }

			float repeat(float p, float interval){
				return mod(p, interval) - interval * 0.5;
			}

			float2 repeat(float2 p, float2 interval){
				return mod(p, interval) - interval * 0.5;
			}

			float3 repeat(float3 p, float3 interval){
				return mod(p, interval) - interval * 0.5;
			}

			float smin(float a, float b) {
				float k = 0.2;
				float h = clamp(0.5 + 0.5 * (b - a) / k, 0,1);
				//return mix(b, a, h) - k * h * (1 - h); 
				return lerp(b,a,h) - k * h * (1 - h);
			}

			// *Sphere
			float sdSphere(float3 p, float r)
			{
				return length(p) - r;
			}

			float sdCone(float3 p, float2 c, float h )
			{
				float q = length(p.xz);
				return max(dot(c.xy,float2(q,p.y)),-h-p.y);
			}

			float sdVerticalCapsule( float3 p, float h, float r )
			{
				p.y -= clamp( p.y, 0.0, h );
				return length( p ) - r;
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

			float sdTorus( float3 p, float2 t )
			{
				float2 q = float2(length(p.xz)-t.x,p.y);
				return length(q)-t.y;
			}

			float getSdf(float3 pos){
				
				float dist = 0;
				// SDF
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

			float3 acesFilm(float3 x){
				const float a = 2.51;
				const float b = 0.03;
				const float c = 2.43;
				const float d = 0.59;
				const float e = 0.14;
				return saturate((x * (a * x + b)) / (x * ( c * x + d) + e));
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
                    float specular = pow(saturate(dot(reflect(light, normal), -rayDir)), 10.0);// 鏡面反射 Phong
                    float ao = calcAO(pos, normal);// AO : Ambient occlusion
                    //float ao = 1.0;
					//float shadow = calcSoftshadow(pos, light, 0.25, 5);// シャドウ
                    //float shadow = genShadow(pos, light);
					float shadow = 1.0;

                    // ライティング結果の合成
                    col += albedo * diffuse * shadow * (1 - metalness);// 直接光の拡散反射
                    col += albedo * specular * shadow * metalness;// 直接光の鏡面反射
                    col += albedo * ao * lerp(float4(0,0,0,0), float4(1,1,1,0), 0.3);// 環境光
					//col = acesFilm(col * 0.8);
					//col = pow(col,1/2.2);
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