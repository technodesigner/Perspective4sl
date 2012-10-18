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
float4x4 WorldViewProj;
float4x4 ModelTransform;

struct VertexData
{
	float3 Position : POSITION;
	float2 TextureCoordinates : TEXCOORD0;
	float3 Normal : NORMAL;
};

struct VertexShaderOutput
{
	float4 Position : POSITION;
	float2 TextureCoordinates : TEXCOORD0;
	float3 Normal : TEXCOORD1;
};

VertexShaderOutput main(VertexData vertex)
{
	VertexShaderOutput output;
	float4 newPos = mul(float4(vertex.Position, 1), ModelTransform);
	output.Position = mul(newPos, WorldViewProj);
	output.Normal.xyz = mul(float4(vertex.Normal.xyz, 0), ModelTransform).xyz;
	// output.Normal.xyz = vertex.Normal.xyz;
	output.TextureCoordinates = vertex.TextureCoordinates;
	return output;
}