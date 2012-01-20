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
using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;

namespace Perspective.Wpf3DX.Sculptors
{
    /// <summary>
    /// A class to handle points and triangles of a box model.
    /// Default size of a side is 1.0.
    /// </summary>
    public class BoxSculptor : Sculptor
    {
        private Vector3 _p000 = new Vector3(0, 0, 0);
        private Vector3 _p100 = new Vector3(1, 0, 0);
        private Vector3 _p110 = new Vector3(1, 1, 0);
        private Vector3 _p010 = new Vector3(0, 1, 0);
        private Vector3 _p001 = new Vector3(0, 0, 1);
        private Vector3 _p011 = new Vector3(0, 1, 1);
        private Vector3 _p101 = new Vector3(1, 0, 1);
        private Vector3 _p111 = new Vector3(1, 1, 1);

        private const int _back = 0;
        private const int _left = 1;
        private const int _front = 2;
        private const int _right = 3;
        private const int _up = 4;
        private const int _down = 5;

        private Collection<Vector3>[] _sidePoints = new Collection<Vector3>[6];

        private BoxSides _sideLayout = BoxSides.All;

        /// <summary>
        /// Initializes a new instance of BoxSculptor.
        /// </summary>
        public BoxSculptor()
            : base()
        {
            this.Points.Add(_p000);
            this.Points.Add(_p100);
            this.Points.Add(_p110);
            this.Points.Add(_p010);
            this.Points.Add(_p001);
            this.Points.Add(_p011);
            this.Points.Add(_p101);
            this.Points.Add(_p111);

            _sidePoints[_back] = new Collection<Vector3>();
            _sidePoints[_back].Add(_p100);
            _sidePoints[_back].Add(_p000);
            _sidePoints[_back].Add(_p010);
            _sidePoints[_back].Add(_p110);

            _sidePoints[_left] = new Collection<Vector3>();
            _sidePoints[_left].Add(_p000);
            _sidePoints[_left].Add(_p001);
            _sidePoints[_left].Add(_p011);
            _sidePoints[_left].Add(_p010);

            _sidePoints[_front] = new Collection<Vector3>();
            _sidePoints[_front].Add(_p001);
            _sidePoints[_front].Add(_p101);
            _sidePoints[_front].Add(_p111);
            _sidePoints[_front].Add(_p011);

            _sidePoints[_right] = new Collection<Vector3>();
            _sidePoints[_right].Add(_p101);
            _sidePoints[_right].Add(_p100);
            _sidePoints[_right].Add(_p110);
            _sidePoints[_right].Add(_p111);

            _sidePoints[_up] = new Collection<Vector3>();
            _sidePoints[_up].Add(_p111);
            _sidePoints[_up].Add(_p110);
            _sidePoints[_up].Add(_p010);
            _sidePoints[_up].Add(_p011);

            _sidePoints[_down] = new Collection<Vector3>();
            _sidePoints[_down].Add(_p000);
            _sidePoints[_down].Add(_p100);
            _sidePoints[_down].Add(_p101);
            _sidePoints[_down].Add(_p001);
        }

        /// <summary>
        /// Initializes a new instance of BoxSculptor.
        /// </summary>
        /// <param name="sideLayout">Set of the visible sides.</param>
        public BoxSculptor(BoxSides sideLayout)
            : this()
        {
            Initialize(sideLayout);
        }

        /// <summary>
        /// Initializes an existing instance of BoxSculptor.
        /// </summary>
        /// <param name="sideLayout">Set of the visible sides.</param>
        public void Initialize(BoxSides sideLayout)
        {
            _sideLayout = sideLayout;
        }

        /// <summary>
        /// Building of the Triangles collections.
        /// </summary>
        protected override void CreateTriangles()
        {
            this.Triangles.Clear();

            if ((_sideLayout & BoxSides.Back) == BoxSides.Back)
            {
                CreateSideTriangles(_sidePoints[_back], TriangleSideKind.Front);
            }
            if ((_sideLayout & BoxSides.Left) == BoxSides.Left)
            {
                CreateSideTriangles(_sidePoints[_left], TriangleSideKind.Front);
            }
            if ((_sideLayout & BoxSides.Front) == BoxSides.Front)
            {
                CreateSideTriangles(_sidePoints[_front], TriangleSideKind.Front);
            }
            if ((_sideLayout & BoxSides.Right) == BoxSides.Right)
            {
                CreateSideTriangles(_sidePoints[_right], TriangleSideKind.Front);
            }
            if ((_sideLayout & BoxSides.Up) == BoxSides.Up)
            {
                CreateSideTriangles(_sidePoints[_up], TriangleSideKind.Front);
            }
            if ((_sideLayout & BoxSides.Down) == BoxSides.Down)
            {
                CreateSideTriangles(_sidePoints[_down], TriangleSideKind.Front);
            }
        }

        /// <summary>
        /// A method for building the texture coordinates of the mesh.
        /// </summary>
        protected override void MapTexture()
        {
            for (int i = 0; i < 6; i++)
            {
                Helper3D.MapSquareTexture(VertexPositionTextureNormals, i * 6, 0.0f, 0.0f, 1.0f, 1.0f);
            }
        }
    }
}
