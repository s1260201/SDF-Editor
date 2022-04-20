Shader "RayMarching/Base"
{
	Properties
	{
		_Colors("Color",Color)=(1.0,1.0,1.0,0.0)
		_LightValue("LightLevel",Range(0.0,10.0))=1.25
	}
	SubShader
	{
		Cull Off
		Tags { 
			"RenderType"="Transparent" 
			"Queue" = "Transparent"
		}
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			uniform float4 _Colors;
			uniform float _LightValue;

			struct VertInput
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Ray{
				float3 org;
				float3 dir;
			};

			struct v2f
			{	
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float2 screenPos : TEXCOORD0;
				float3 worldPos : TEXCOORD1;
				float4 pos : POSITION1;
			};

			struct frag_out{
				//�F�Ɛ[�x����Ԃ�
				half4 color : SV_Target;
				float depth : SV_Depth;
			};

			//�J��Ԃ��֐�
			float3 mod(float3 a, float3 b)
			{
				return frac(abs(a / b)) * abs(b);
			}

			float3 repeat(float3 pos, float3 span)
			{
				return mod(pos, span) - span * 0.5;
			}
			
			//�[�x�����v�Z���܂��B
			float computeDepth(float3 pos)
            {
                float4 vpPos = UnityObjectToClipPos(float4(pos, 1.0));
                #if UNITY_UV_STARTS_AT_TOP
                    return vpPos.z / vpPos.w;
                #else
                    return (vpPos.z / vpPos.w) * 0.5 + 0.5;
                #endif
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
			
			float2 distFunc(float3 pos){
				float dist = 0; 
				// SDF
				return dist;
			}
			//���C�e�B���O�Ƃ��ɕK�v�ɂȂ�@���x�N�g���𐶐�����֐�
			//�ɔ����Ȑ��l�̂����x,y,z����擾���A�����normalize�i�x�N�g���̒�����1�Ɂj���邱�Ƃœ���
			float3 generateNormal(float3 p){
				//���C�}�[�`���O�ŕK�v�Ȗ@���x�N�g�������
				//float d = 0.0001;
				const float3 d = float3(0.0001,0.0,0.0);
				float x = distFunc(p+d.xyy)-distFunc(p-d.xyy);
				float y = distFunc(p+d.yxy)-distFunc(p-d.yxy);
				float z = distFunc(p+d.yyx)-distFunc(p-d.yyx);
				return normalize(float3(x,y,z));
			}
			
			v2f vert (VertInput v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);		//�����W(640,360)etc
				o.pos = mul(unity_ObjectToWorld, v.vertex);
				o.uv = v.uv;
                o.screenPos = o.vertex;							//UV���W(0-1)
                o.worldPos = v.vertex.xyz;						//���Ƃ��Ƃ̒��_���W
                return o;
            }
			
			frag_out frag (v2f i)
			{
				// Put the results and return this.
				frag_out o;
				//���C�}�[�`���O�p�̃��C�̒�`
				Ray ray;
				//���C�̌��_�����݂̃J�����ʒu�����[���h���W�ɒu�����������̂ɒ�`
				//ray.org = mul(unity_WorldToObject,float4(_WorldSpaceCameraPos,1.0));
				ray.org = i.pos.xyz;
				ray.dir = normalize(ray.org.xyz - _WorldSpaceCameraPos);
				//ray.dir = normalize(i.worldPos - ray.org);

				float3 posOnRay = ray.org;
				float maxStep = 32;
				float lightValue = _LightValue; //���C�g�̖��邳����
				float threshold = 0.001;		//�Փ˂����Ɣ��肷��臒l
				float maxDistance = 10.0;		//���ƂŃJ��������Max�������쐬
				float tmp;
				float2 dist;
				tmp = 0.0;

				//Marching Loop
				for(int i=0;i<maxStep;i++){
					dist = distFunc(posOnRay);
					tmp += dist.x;
					posOnRay = ray.org + tmp*ray.dir;
					if(dist.x<threshold||dist.x>maxDistance)
						break;
				}

				//���C���I�u�W�F�N�g�ɂԂ�����������
				float4 color = float4(0.0,0.0,0.0,0.0);
				//�Ԃ����Ă���Έ���菬�����͂��A�傫�������炻�̃s�N�Z����discard(�������Ȃ�)
				if(abs(dist.x)>threshold)
					discard;
				else{
					//�@���x�N�g�����쐬
					float3 normal = generateNormal(posOnRay);
					//_WorldSpaceLightPos0�ŃV�[�����̃��C�g�̈ʒu���擾�ł���
					float diff = clamp(dot(_WorldSpaceLightPos0,normal),0.1,1.0);
					color = _Colors * diff * lightValue;
				}
				o.color = color;
				o.depth = computeDepth(posOnRay);
				return o;
			}
			ENDCG
		}
	}
}