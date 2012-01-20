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
using System.ComponentModel;

namespace Perspective.Wpf3DX
{
    /// <summary>
    /// A structure to manage material properties.
    /// </summary>
    public struct ModelMaterial
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="emissiveness">The emissiveness factor.</param>
        /// <param name="ambientness">The ambiantness factor.</param>
        /// <param name="diffuseness">The diffuseness factor.</param>
        /// <param name="specularness">The specularness factor.</param>
        /// <param name="shininess">The shininess factor.</param>
        public ModelMaterial(float emissiveness, float ambientness, float diffuseness, float specularness, float shininess) : this()
        {
            Emissiveness = emissiveness;
            Ambientness = ambientness;
            Diffuseness = diffuseness;
            Specularness = specularness;
            Shininess = shininess;
        }

        /// <summary>
        /// Gets or sets the emissiveness factor.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float Emissiveness { get; set; }

        /// <summary>
        /// Gets or sets the ambiantness factor.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float Ambientness { get; set; }
        
        /// <summary>
        /// Gets or sets the diffuseness factor.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float Diffuseness { get; set; }
        
        /// <summary>
        /// Gets or sets the specularness factor.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float Specularness { get; set; }

        /// <summary>
        /// Gets or sets the shininess factor.
        /// <remarks>Best values are between 0 and 1. Specularness shoud be > 0.</remarks>
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float Shininess { get; set; }
        
    }
}
