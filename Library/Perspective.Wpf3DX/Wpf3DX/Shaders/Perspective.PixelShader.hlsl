//------------------------------------------------------------------
//
//  For licensing information and to get the latest version go to:
//  http://www.codeplex.com/perspective4sl
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY
//  OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//  LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
//  FITNESS FOR A PARTICULAR PURPOSE.
//
//------------------------------------------------------------------
float4 LookDirection : register(c0);
float4 AmbientLight : register(c1);
float4 DirectionalLight1 : register(c2);
float4 DirectionalLight2 : register(c3);
float4 DirectionalLight3 : register(c4);
float4 DirectionalLight4 : register(c5);
float2x4 Material : register(c6);

texture visualTexture : register(t0);
sampler visualSampler = sampler_state
{
	texture = <visualTexture>;    
};

struct VertexShaderOutput
{
	float4 Position : POSITION;
	float2 TextureCoordinates : TEXCOORD0;
	float3 Normal : TEXCOORD1;
};

float4 main(VertexShaderOutput vertex) : COLOR
{
	float4 color = tex2D(visualSampler, vertex.TextureCoordinates).rgba;
	float alpha = color.a;
	float diffuseFactor1 = DirectionalLight1.w * max(dot(-DirectionalLight1.xyz, vertex.Normal), 0);
	float diffuseFactor2 = DirectionalLight2.w * max(dot(-DirectionalLight2.xyz, vertex.Normal), 0);
	float diffuseFactor3 = DirectionalLight3.w * max(dot(-DirectionalLight3.xyz, vertex.Normal), 0);
	float diffuseFactor4 = DirectionalLight4.w * max(dot(-DirectionalLight4.xyz, vertex.Normal), 0);
	float diffuseFactor = saturate(diffuseFactor1 + diffuseFactor2 + diffuseFactor3 + diffuseFactor4);
	float3 reflection1 = normalize(2 * diffuseFactor1 * vertex.Normal + DirectionalLight1.xyz); 
	float3 reflection2 = normalize(2 * diffuseFactor2 * vertex.Normal + DirectionalLight2.xyz); 
	float3 reflection3 = normalize(2 * diffuseFactor3 * vertex.Normal + DirectionalLight3.xyz); 
	float3 reflection4 = normalize(2 * diffuseFactor4 * vertex.Normal + DirectionalLight4.xyz); 
	float shininess = Material._m13 == 0 ? 1000 : 10 / Material._m13;
	float4 result =
		saturate(
			(color *
				saturate(											// returns 0 < x < 1
					Material._m03									// emissive factor
					+ AmbientLight.w * Material._m00				// ambient factor
					+ diffuseFactor * Material._m01))				// diffuse factor
			+ Material._m02 * DirectionalLight1.w * pow(saturate(dot(reflection1, -DirectionalLight1.xyz)), shininess)
			+ Material._m02 * DirectionalLight2.w * pow(saturate(dot(reflection2, -DirectionalLight2.xyz)), shininess)
			+ Material._m02 * DirectionalLight3.w * pow(saturate(dot(reflection3, -DirectionalLight3.xyz)), shininess)
			+ Material._m02 * DirectionalLight4.w * pow(saturate(dot(reflection3, -DirectionalLight4.xyz)), shininess)
			);
	result.a = alpha;
	return result;
}