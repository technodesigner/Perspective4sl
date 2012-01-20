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
using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;
using Perspective.Wpf3DX.Transforms;

namespace Perspective.Wpf3DX.Sculptors
{
    /// <summary>
    /// A class to handle points and triangles of a 3D model.
    /// </summary>
    public abstract class Sculptor
    {
        /// <summary>
        /// Initializes a new instance of Sculptor.
        /// </summary>
        public Sculptor()
        {
        }

        private Collection<Vector3> _points = new Collection<Vector3>();

        /// <summary>
        /// Gets the sculptor's points collection.
        /// </summary>
        public Collection<Vector3> Points
        {
            get { return _points; }
        }

        /// <summary>
        /// Gets or sets the sculptor's triangles collection.
        /// </summary>
        public Collection<Vector3Triplet> Triangles = new Collection<Vector3Triplet>(); // No getter for performance reason

        private VertexPositionTextureNormal[] _vertexPositionTextureNormal;

        /// <summary>
        /// Gets an array of vertex positions.
        /// </summary>
        public VertexPositionTextureNormal[] VertexPositionTextureNormals
        {
            get { return _vertexPositionTextureNormal; }
        }

        /// <summary>
        /// Clears the triangles and points collections.
        /// </summary>
        public void Clear()
        {
            Triangles.Clear();
            Points.Clear();
        }

        /// <summary>
        /// Virtual method to override for building the Triangles collections.
        /// </summary>
        protected virtual void CreateTriangles()
        {
        }

        /// <summary>
        /// Creates the triangles of a side.
        /// </summary>
        /// <remarks>The cull mode is supposed to be clockwise (front faces are counter-clockwise).</remarks>
        /// <param name="points">Collection of 3D points - may be an other collection than points (i.e. a side collection).</param>
        /// <param name="tsk">TriangleSideKind Value.</param>
        protected void CreateSideTriangles(
            Collection<Vector3> points,
            TriangleSideKind tsk)
        {
            for (int i = 0; i < points.Count - 2; i++)
            {
                if (tsk == TriangleSideKind.Front)
                {
                    this.Triangles.Add(new Vector3Triplet(points[0], points[i + 1], points[i + 2]));
                }
                else
                {
                    this.Triangles.Add(new Vector3Triplet(points[0], points[i + 2], points[i + 1]));
                }
            }
        }

        /// <summary>
        /// Virtual method to override for building the texture coordinates of the mesh.
        /// </summary>
        protected virtual void MapTexture()
        {
        }

        /// <summary>
        /// Build a 3D mesh geometry from the Sculptor object.
        /// Points should be initialized before the call.
        /// </summary>
        public void BuildMesh()
        {
            CreateTriangles();
            _vertexPositionTextureNormal = new VertexPositionTextureNormal[this.Triangles.Count * 3];
            for (int i = 0; i < Triangles.Count; i++)
            {
                Vector3 normal = Helper3D.CalculateNormal(
                    Triangles[i].Points[0],
                    Triangles[i].Points[1],
                    Triangles[i].Points[2]);
                normal.Normalize();
                for (int j = 0; j < 3; j++)
                {
                    _vertexPositionTextureNormal[i * 3 + j] = new VertexPositionTextureNormal(Triangles[i].Points[j], normal);
                }
            }
            MapTexture();
        }

        /// <summary>
        /// Copies the points and triangles from a Sculptor object
        /// </summary>
        /// <param name="s"></param>
        protected void CopyFrom(Sculptor s)
        {
            foreach (Vector3 p in s.Points)
            {
                this.Points.Add(p);
            }
            foreach (Vector3Triplet tpl in s.Triangles)
            {
                this.Triangles.Add(tpl);
            }
        }

        /// <summary>
        /// Applies a transformation to the points.
        /// </summary>
        /// <param name="t">ModelTransform object.</param>
        public virtual void Transform(ModelTransform t)
        {
            for (int i = 0; i < _points.Count; i++)
            {
                _points[i] = t.Transform(_points[i]);
            }

            for (int j = 0; j < Triangles.Count; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    Triangles[j].Points[k] = t.Transform(Triangles[j].Points[k]);
                }
            }
        }
    }
}
