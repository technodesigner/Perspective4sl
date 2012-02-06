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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Perspective.Wpf3DX.Transforms;
using Perspective.Wpf3DX.Sculptors;
using Perspective.Wpf3DX.Shaders;
using Perspective.Wpf3DX.Textures;

namespace Perspective.Wpf3DX.Models
{
    /// <summary>
    /// Basic abstract class for Perspective 3D geometrical models.
    /// </summary>
    public abstract class Sculpture : Model
    {
        private Scene _scene;
        private Sculptor _sculptor;
        private VertexBuffer _vertexBuffer;
        private Texture2D _texture;
        private Texture2D _backTexture;
        private Matrix _matrix;

        private ModelMaterial _material = Helper3D.DefaultMaterial;

        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        public ModelMaterial Material
        {
            get { return _material; }
            set { _material = value; }
        }

        private ModelMaterial _backMaterial = Helper3D.DefaultMaterial;

        /// <summary>
        /// Gets or sets the back face's material.
        /// </summary>
        public ModelMaterial BackMaterial
        {
            get { return _backMaterial; }
            set { _backMaterial = value; }
        }

        ///// <summary>
        ///// Gets or sets the texture.
        ///// </summary>
        public ModelTexture Texture { get; set; }

        /// <summary>
        /// Gets or sets the back face's texture.
        /// </summary>
        public ModelTexture BackTexture { get; set; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public Sculpture()
        {
            Texture = Helper3D.DefaultTexture;
        }

        /// <summary>
        /// Gets the associated Sculptor object.
        /// </summary>
        protected Sculptor Sculptor
        {
            get { return _sculptor; }
        }

        private SamplerState _samplerState;

        /// <summary>
        /// Initializes the visual.
        /// </summary>
        /// <param name="scene">The Scene object.</param>
        /// <param name="ContainerMatrix">The container matrix.</param>
        public override void Initialize(Scene scene, Matrix containerMatrix)
        {
            _samplerState = new SamplerState
            {
                AddressU = TextureAddressMode.Clamp,
                AddressV = TextureAddressMode.Clamp
            };  

            ContainerMatrix = containerMatrix;
            _scene = scene; // to avoid getter access in the OnRender method
            Invalidate();
            _matrix = Matrix; //  to avoid getter access in the OnRender method
            base.Initialize(scene, containerMatrix);
        }

        /// <summary>
        /// Invalidates the mesh of the visual.
        /// </summary>
        public void InvalidateMesh()
        {
            GraphicsDevice graphicsDevice = Helper3D.GraphicsDevice;
            _sculptor = CreateSculptor();
            _sculptor.BuildMesh();
            _vertexBuffer = new VertexBuffer(
                graphicsDevice,
                VertexPositionTextureNormal.VertexDeclaration,
                _sculptor.VertexPositionTextureNormals.Length,
                BufferUsage.WriteOnly);
            _vertexBuffer.SetData(0, _sculptor.VertexPositionTextureNormals, 0, _sculptor.VertexPositionTextureNormals.Length, 0);
            // graphicsDevice.SetVertexBuffer(_vertexBuffer);
        }

        /// <summary>
        /// Invalidates the transform of the visual.
        /// </summary>
        /// <remarks>This initializes the Matrix property.</remarks>
        public override void InvalidateTransform()
        {
            base.InvalidateTransform();
            _matrix = Matrix;
        }

        /// <summary>
        /// Invalidates the texture of the visual.
        /// </summary>
        public void InvalidateTexture()
        {
            if (Texture != null)
            {
                _texture = Texture.GetTexture2D();
            }
            if (BackTexture != null)
            {
                _backTexture = BackTexture.GetTexture2D();
            }
        }

        /// <summary>
        /// Invalidates the mesh, the transform and the texture of the visual.
        /// </summary>
        public void Invalidate()
        {
            InvalidateMesh();
            InvalidateTransform();
            InvalidateTexture();
        }

        /// <summary>
        /// Initializes a new specific instance.
        /// </summary>
        /// <returns>A Sculptor object.</returns>
        protected abstract Sculptor CreateSculptor();

        /// <summary>
        /// Throws the Render event.
        /// </summary>
        /// <param name="e">The event data.</param>
        public override void OnRender(RenderEventArgs e)
        {
            base.OnRender(e);

            if (_vertexBuffer != null)
            {
                // For texture rendering
                e.GraphicsDevice.SamplerStates[0] = _samplerState; 

                // Vertex pipeline
                e.GraphicsDevice.SetVertexBuffer(_vertexBuffer);
                ShaderHelper.SetVertexShader(e.GraphicsDevice, _scene._vertexShader);
                ShaderHelper.SetVertexShaderConstantMatrix(e.GraphicsDevice, ref e.ViewProjection);
                ShaderHelper.SetVertexShaderConstantMatrix(e.GraphicsDevice, ref _matrix);

                // Pixel pipeline
                ShaderHelper.SetPixelShader(e.GraphicsDevice, _scene._pixelShader);
                Vector4 lookDirection = new Vector4(_scene.Camera.LookTarget, 0.0f);
                ShaderHelper.SetPixelShaderConstantFloat4(e.GraphicsDevice, ref lookDirection);
                ShaderHelper.SetPixelShaderConstantFloat4(e.GraphicsDevice, ref _scene._ambientLightVector);
                ShaderHelper.SetPixelShaderConstantFloat4(e.GraphicsDevice, ref _scene._directionalLightVector1);
                ShaderHelper.SetPixelShaderConstantFloat4(e.GraphicsDevice, ref _scene._directionalLightVector2);
                ShaderHelper.SetPixelShaderConstantFloat4(e.GraphicsDevice, ref _scene._directionalLightVector3);
                ShaderHelper.SetPixelShaderConstantFloat4(e.GraphicsDevice, ref _scene._directionalLightVector4);
                ShaderHelper.SetPixelShaderConstantMaterial(e.GraphicsDevice, ref _material);

                if (_texture != null)
                {
                    e.GraphicsDevice.Textures[0] = _texture;
                }

                e.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, _sculptor.Triangles.Count);

                if (_backTexture != null)
                {
                    e.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
                    e.GraphicsDevice.Textures[0] = _backTexture;
                    ShaderHelper.SetPixelShaderConstantMaterial(e.GraphicsDevice, ref _backMaterial);
                    e.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, _sculptor.Triangles.Count);
                    e.GraphicsDevice.RasterizerState = Helper3D.DefaultRasterizerState;
                }
            }
        }
    }
}
