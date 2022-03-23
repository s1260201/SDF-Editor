Shader "SDFE/Template"
{
	Properties
	{

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
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
                                //ローカル→ワールド座標に変換
				o.pos = mul(unity_ObjectToWorld, v.vertex);
				o.uv = v.uv;
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

			// *Torus
			float sdTorus(float3 p, float3 t){
				float2 q = float2(length(p.xz) - t.x, p.y);
				return length(q) - t.y;
			}

			// *Cylinder
			float sdCappedCylinder(float3 p, float3 a, float3 b, float r) //(レイポジ, 下面座標, 上面座標, 太さ)  右手左手系に注意
			{
				float3  ba = b - a;
				float3  pa = p - a;
				float baba = dot(ba,ba);
				float paba = dot(pa,ba);
				float x = length(pa*baba-ba*paba) - r*baba;
				float y = abs(paba-baba*0.5)-baba*0.5;
				float x2 = x*x;
				float y2 = y*y*baba;
				float d = (max(x,y)<0.0)?-min(x2,y2):(((x>0.0)?x2:0.0)+((y>0.0)?y2:0.0));
				return sign(d)*sqrt(abs(d))/baba;
			}

			float mod(float x, float y)
            {
                return x - y * floor(x / y);
            }

            float2 mod(float2 x, float2 y)
            {
                return x - y * floor(x / y);
            }

			float2 opRep(float2 p, float2 interval)
            {
                return mod(p, interval) - interval * 0.5;
            }



			// input obj info there
			
			float getSdf(float3 pos){
				
				//float marchingDist = dTrii(pos,float3(0.75,0.0,0.0),float3(0.75,2.3,0.0) ,0.1);
				//float marchingDist2 = dTrii(pos,float3(-0.75,0.0,0.0),float3(-0.75,2.3,0.0) ,0.1);
				float marchingDist3 = sdBox(float3(0,0,0),float3(0.5,0.5,0.5));
				//float marchingDist4 = sdBox(float3(0,1,0),float3(1,1,1));
				//return min(marchingDist3, marchingDist4);
				return marchingDist3;
				// AddSDF

				// float obj1 = ***;
				// float obj2 = ***;
				// obj2= min(obj1, obj2);
				// float obj3 = ***;
				// obj3 = max(obj2, obj3);
				// return obj3;

				//return 0;
			}
			
			/*
			float calcSoftshadow(float3 ro, float3 rd, float mint, float tmax)
            {
                // bounding volume
                float tp = (0.8 - ro.y) / rd.y;
                if (tp > 0.0) tmax = min(tmax, tp);
                
                float res = 1.0;
                float t = mint;
                for (int i = 0; i < 24; i++)
                {
                    float h = getSdf(ro + rd * t).x;
                    float s = clamp(8.0 * h / t, 0.0, 1.0);
                    res = min(res, s * s * (3.0 - 2.0 * s));
                    t += clamp(h, 0.02, 0.2);
                    if (res < 0.004 || t > tmax) break;
                }
                return clamp(res, 0.0, 1.0);
            }
			*/
			
			float4 rayMarch(float3 pos, float3 rayDir, int StepNum){
				int fase = 0;
				float t = 0;
				float d = getSdf(pos);
				float3 col = 0;

				while(fase < StepNum && abs(d) > 0.001){
					t += d;
					pos += rayDir * d;
					d = getSdf(pos);
					fase++;
				}
				if(step(StepNum,fase)){
					return float4(1,1,1,1);
				}else{
					return float4(col,0);
				}				
			}
			

			fixed4 frag(v2f i) : SV_Target
			{
				float3 col = float3(1,1,1);
				// レイの初期位置(ピクセルのワールド座標)
				float3 pos = i.pos.xyz;
				// レイの進行方向
				float3 rayDir = normalize(pos.xyz - _WorldSpaceCameraPos);

				int StepNum = 30;
				return rayMarch(pos,rayDir,StepNum);
			}
			ENDCG
		}
	}
}
