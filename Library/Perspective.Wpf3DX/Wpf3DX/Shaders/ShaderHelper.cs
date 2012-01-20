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
using System;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Windows;
using Microsoft.Xna.Framework;

namespace Perspective.Wpf3DX.Shaders
{
    /// <summary>
    /// A helper class for shader operations.
    /// </summary>
    public static class ShaderHelper
    {
        private static int _materialRegister = -1;
        private static int _pixelRegister;
        private static int _vertexRegister;

        /// <summary>
        /// Associates the specified VertexShader with the specified GraphicsDevice
        /// </summary>
        /// <remarks>This method manages automatically the vertex registers.</remarks>
        /// <param name="graphicsDevice">The GraphicsDevice object.</param>
        /// <param name="vertexShader">The VertexShader object.</param>
        public static void SetVertexShader(GraphicsDevice graphicsDevice, VertexShader vertexShader)
        {
            graphicsDevice.SetVertexShader(vertexShader);
            _vertexRegister = 0;
        }

        /// <summary>
        /// Sets constant registers to a matrix which is passed to the vertex shader on the graphics device.
        /// </summary>
        /// <remarks>This method manages automatically the vertex registers and must be used after a ShaderHelper.SetVertexShader call.</remarks>
        /// <param name="graphicsDevice">The GraphicsDevice object.</param>
        /// <param name="matrix">A matrix object.</param>
        public static void SetVertexShaderConstantMatrix(GraphicsDevice graphicsDevice, ref Matrix matrix)
        {
            Vector4 v1 = new Vector4(matrix.M11, matrix.M12, matrix.M13, matrix.M14);
            graphicsDevice.SetVertexShaderConstantFloat4(_vertexRegister++, ref v1);

            Vector4 v2 = new Vector4(matrix.M21, matrix.M22, matrix.M23, matrix.M24);
            graphicsDevice.SetVertexShaderConstantFloat4(_vertexRegister++, ref v2);

            Vector4 v3 = new Vector4(matrix.M31, matrix.M32, matrix.M33, matrix.M34);
            graphicsDevice.SetVertexShaderConstantFloat4(_vertexRegister++, ref v3);

            Vector4 v4 = new Vector4(matrix.M41, matrix.M42, matrix.M43, matrix.M44);
            graphicsDevice.SetVertexShaderConstantFloat4(_vertexRegister++, ref v4);
        }

        /// <summary>
        /// Associates the specified PixelShader with the specified GraphicsDevice
        /// </summary>
        /// <remarks>This method manages automatically the pixel registers.</remarks>
        /// <param name="graphicsDevice">The GraphicsDevice object.</param>
        /// <param name="pixelShader">The PixelShader object.</</param>
        public static void SetPixelShader(GraphicsDevice graphicsDevice, PixelShader pixelShader)
        {
            graphicsDevice.SetPixelShader(pixelShader);
            _pixelRegister = 0;
        }

        /// <summary>
        /// Sets constant registers to a Vector4 which is passed to the pixel shader on the graphics device.
        /// </summary>
        /// <remarks>This method manages automatically the pixel registers and must be used after a ShaderHelper.SetPixelShader call.</remarks>
        /// <param name="graphicsDevice">The GraphicsDevice object.</param>
        /// <param name="vector4">A Vector4 object.</param>
        public static void SetPixelShaderConstantFloat4(GraphicsDevice graphicsDevice, ref Vector4 vector4)
        {
            graphicsDevice.SetPixelShaderConstantFloat4(_pixelRegister++, ref vector4);
        }

        /// <summary>
        /// Sets constant registers to a material which is passed to the pixel shader on the graphics device.
        /// </summary>
        /// <remarks>This method manages automatically the pixel registers and must be used after a ShaderHelper.SetPixelShader call.</remarks>
        /// <param name="graphicsDevice">The GraphicsDevice object.</param>
        /// <param name="material">A ModelMaterial object.</param>
        public static void SetPixelShaderConstantMaterial(GraphicsDevice graphicsDevice, ref ModelMaterial material)
        {
            int register = _pixelRegister;
            bool incrementPixelShaderRegister = false;
            if (_materialRegister == -1)
            {
                _materialRegister = _pixelRegister;
                incrementPixelShaderRegister = true;
            }
            else
            {
                register = _materialRegister;
            }

            Vector4 v1 = new Vector4(material.Ambientness, material.Diffuseness, material.Specularness, material.Emissiveness); // x,y,z,w
            graphicsDevice.SetPixelShaderConstantFloat4(register++, ref v1);
            
            Vector4 v2 = new Vector4(0.0f, 0.0f, 0.0f, material.Shininess);
            graphicsDevice.SetPixelShaderConstantFloat4(register++, ref v2);

            if (incrementPixelShaderRegister)
            {
                _pixelRegister = register;
            }
        }
    }
}
