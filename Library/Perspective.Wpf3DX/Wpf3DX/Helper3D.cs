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
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Media.Imaging;
using Perspective.Wpf3DX.Textures;
using System.Windows.Graphics;

namespace Perspective.Wpf3DX
{
    /// <summary>
    /// A helper class for 3D operations.
    /// </summary>
    public static class Helper3D
    {
        /// <summary>
        /// Maps a texture to a 3D square surface.
        /// </summary>
        /// <remarks>The cull mode is supposed to be counterclockwise.</remarks>
        /// <param name="vertexPositionTextureNormal">Vertex buffer.</param>
        /// <param name="vertexIndice">Indice of the first vertex.</param>
        /// <param name="minX">The left X coordinate of the 3D square.</param>
        /// <param name="minY">The lower Y coordinate of the 3D square.</param>
        /// <param name="maxX">The right X coordinate of the 3D square.</param>
        /// <param name="maxY">The upper Y coordinate of the 3D square.</param>
        public static void MapSquareTexture(
            VertexPositionTextureNormal[] vertexPositionTextureNormal,
            int vertexIndice,
            float minX,
            float minY,
            float maxX,
            float maxY)
        {
            vertexPositionTextureNormal[vertexIndice++].TextureCoordinates = new Vector2(minX, maxY);
            vertexPositionTextureNormal[vertexIndice++].TextureCoordinates = new Vector2(maxX, maxY);
            vertexPositionTextureNormal[vertexIndice++].TextureCoordinates = new Vector2(maxX, minY);

            vertexPositionTextureNormal[vertexIndice++].TextureCoordinates = new Vector2(minX, maxY);
            vertexPositionTextureNormal[vertexIndice++].TextureCoordinates = new Vector2(maxX, minY);
            vertexPositionTextureNormal[vertexIndice].TextureCoordinates = new Vector2(minX, minY);
        }

        /// <summary>
        /// Calculates the normal vector of a triangle surface.
        /// </summary>
        /// <remarks>
        /// From : 
        /// http://www.kindohm.com/technical/WPF3DTutorial.htm
        /// http://www.limsi.fr/Individu/jacquemi/IG-TR-7-8-9/surf-maillage-vn.html
        /// </remarks>
        /// <param name="p1">First point.</param>
        /// <param name="p2">Second point.</param>
        /// <param name="p3">Third point.</param>
        /// <returns></returns>
        public static Vector3 CalculateNormal(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Vector3 v1 = new Vector3(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            Vector3 v2 = new Vector3(p3.X - p2.X, p3.Y - p2.Y, p3.Z - p2.Z);

            return Vector3.Cross(v1, v2);
        }

        /// <summary>
        /// Creates an XNA texture from an image.
        /// </summary>
        /// <param name="bitmapSource">A BitmapSource object.</param>
        /// <returns>A Texture2D object.</returns>
        public static Texture2D CreateTexture2D(BitmapSource bitmapSource)
        {
            if (bitmapSource == null)
            {
                throw new ArgumentNullException("bitmapSource");
            }
            if (bitmapSource.PixelWidth == 0)
            {
                return null;
            }
            var texture = new Texture2D(
                GraphicsDevice,
                bitmapSource.PixelWidth,
                bitmapSource.PixelHeight,
                false,
                SurfaceFormat.Color);
            bitmapSource.CopyTo(texture);
            return texture;
        }

        /// <summary>
        /// Creates an XNA texture from a color.
        /// </summary>
        /// <param name="color">An XNA Color object.</param>
        /// <returns>A Texture2D object.</returns>
        public static Texture2D CreateTexture2D(Color color)
        {
            if (color == null)
            {
                throw new ArgumentNullException("color");
            }
            var texture = new Texture2D(
                GraphicsDevice,
                1,
                1,
                false,
                SurfaceFormat.Color);
            Color[] colors = new Color[1] { color };
            texture.SetData<Color>(0, null, colors, 0, 1);
            return texture;
        }

        /// <summary>
        /// Gets the default Perspective's graphic device.
        /// </summary>
        public static GraphicsDevice GraphicsDevice
        {
            get { return GraphicsDeviceManager.Current.GraphicsDevice; }
        }

        /// <summary>
        /// Checks the 3D availability.
        /// </summary>
        public static void Check3DAvailability()
        {
            if (GraphicsDeviceManager.Current.RenderMode == RenderMode.Unavailable)
            {
                throw new RenderModeException(GraphicsDeviceManager.Current.RenderModeReason);
            }
        }

        /// <summary>
        /// Gets the default Perspective's RasterizerState object.
        /// </summary>
        public static RasterizerState DefaultRasterizerState
        {
            get { return RasterizerState.CullClockwise; }
        }

        private static ModelTexture _defaultTexture;

        /// <summary>
        /// Gets the default Perspective's texture object.
        /// </summary>
        public static ModelTexture DefaultTexture
        {
            get 
            {
                if (_defaultTexture == null)
                {
                    _defaultTexture = new ColorTexture(1.0f, 0.87f, 0.18f, 1.0f);
                }
                return _defaultTexture; 
            }
        }

        /// <summary>
        /// Gets a default material object.
        /// </summary>
        public static ModelMaterial DefaultMaterial
        {
            get
            {
                return new ModelMaterial(0.0f, 1.0f, 1.0f, 0.0f, 0.5f);
            }
        }

        /// <summary>
        /// Duplicates two collections of Vector3.
        /// </summary>
        /// <param name="from">Original collection.</param>
        /// <param name="to">Recipient collection.</param>
        public static void CloneVectors(Collection<Vector3> from, Collection<Vector3> to)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }
            if (to == null)
            {
                throw new ArgumentNullException("to");
            }
            to.Clear();
            CopyVectors(from, to);
        }

        /// <summary>
        /// Copy the vector3 objects of a collection in an other one.
        /// </summary>
        /// <param name="from">Original collection.</param>
        /// <param name="to">Recipient collection.</param>
        public static void CopyVectors(Collection<Vector3> from, Collection<Vector3> to)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }
            if (to == null)
            {
                throw new ArgumentNullException("to");
            }
            foreach (Vector3 p in from)
            {
                to.Add(p);
            }
        }

        /// <summary>
        /// Rounds a vertex of a triangle.
        /// </summary>
        /// <param name="pA">First point.</param>
        /// <param name="pB">Second point. Vertex to round</param>
        /// <param name="pC">Third point.</param>
        /// <param name="roundingRate">Vertex rounding rate. The value must be comprized between 0.0 and 0.5.</param>
        /// <returns></returns>
        public static Collection<Vector3> RoundCorner(Vector3 pA, Vector3 pB, Vector3 pC, float roundingRate)
        {
            if (!((pA.Z == pB.Z) && (pB.Z == pC.Z))
                )
            {
                throw new ArgumentOutOfRangeException("pA");
            }

            if ((roundingRate < 0.0)
                || (roundingRate > 0.5)
                )
            {
                throw new ArgumentOutOfRangeException("roundingRate");
            }

            Collection<Vector3> points = new Collection<Vector3>();

            int roundingDefinition = (int)(roundingRate * 40.0);

            Vector3 v1 = new Vector3();
            v1 = pA - pB;
            v1.X = (float)Math.Round(v1.X, 3);
            v1.Y = (float)Math.Round(v1.Y, 3);
            v1.Z = (float)Math.Round(v1.Z, 3);
            Vector3 p1 = Vector3.Add(pB, Vector3.Multiply(v1, roundingRate));

            Vector3 v2 = new Vector3();
            v2 = pC - pB;
            v2.X = (float)Math.Round(v2.X, 3);
            v2.Y = (float)Math.Round(v2.Y, 3);
            v2.Z = (float)Math.Round(v2.Z, 3);
            Vector3 p2 = Vector3.Add(pB, Vector3.Multiply(v2, roundingRate));

            // v1 is the normal vector for the linear curve
            // v1.X*x + v1.Y*y + c1 = 0;
            // p1 is owned by this curve so
            float c1 = -(v1.X * p1.X) - (v1.Y * p1.Y);

            // same for v2 and p2
            float c2 = -(v2.X * p2.X) - (v2.Y * p2.Y);

            // center for the arc that owns p1 and p2
            Vector3 center = new Vector3();

            if (v1.Y == 0.0)
            {
                if (v1.X == 0.0)
                {
                    throw new InvalidOperationException();
                }
                center.X = -c1 / v1.X;
                if (v2.Y == 0.0)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    center.Y = (-c2 - v2.X * center.X) / v2.Y;
                }
            }
            else
            {
                if (v2.Y == 0.0)
                {
                    if (v2.X == 0.0)
                    {
                        throw new InvalidOperationException();
                    }
                    center.X = -c2 / v2.X;
                }
                else
                {
                    center.X = (c1 / v1.Y - c2 / v2.Y) / (v2.X / v2.Y - v1.X / v1.Y);
                }
                center.Y = (-c1 - v1.X * center.X) / v1.Y;
            }
            center.Z = pB.Z;

            // angle of the arc between p1 and p2
            // 360 - 180 - Vector3.AngleBetween(v1, v2)
            float angleArc = MathHelper.ToRadians(180 - AngleBetween(v1, v2));
            
            // angle of each part
            float angleStep = angleArc / roundingDefinition;

            Vector3 vRadius = p1 - center;

            float angleBaseDeg = AngleBetween(new Vector3(1, 0, 0), vRadius);
            // necessar adjustment because of Vector3.AngleBetween() - see documentation
            if (p1.Y < 0.0)
            {
                angleBaseDeg = 360.0f - angleBaseDeg;
            }
            float angleBase = MathHelper.ToRadians(angleBaseDeg);

            points.Add(p1);
            // points of the arc
            for (int j = 1; j <= roundingDefinition - 1; j++)
            {
                float angle = angleBase + (angleStep * j);
                Vector3 p = new Vector3();
                p.X = center.X + (float)Math.Cos(angle) * vRadius.Length();
                p.Y = center.Y + (float)Math.Sin(angle) * vRadius.Length();
                p.Z = pB.Z;
                points.Add(p);
            }
            points.Add(p2);

            return points;
        }

        /// <summary>
        /// Returns the angle between 2 vectors.
        /// </summary>
        /// <remarks>Thanks to http://www.xnawiki.com/index.php?title=Vector_Math </remarks>
        /// <param name="v1">The first Vector3 object.</param>
        /// <param name="v2">The second Vector3 object.</param>
        /// <returns>An angle value (in degrees).</returns>
        public static float AngleBetween(Vector3 v1, Vector3 v2)
        {
            v1.Normalize();
            v2.Normalize();
            return MathHelper.ToDegrees((float)Math.Acos(Vector3.Dot(v1, v2)));
        }

        /// <summary>
        /// Rotation of a vector around one of the 3 axes according to a given angle.
        /// </summary>
        /// <param name="source">Vector to rotate</param>
        /// <param name="angle">Angle (in radians)</param>
        /// <param name="rotationAxis">Rotation axis : X, Y ou Z</param>
        /// <returns>A new Vector3 object corresponding to the rotation</returns>
        public static Vector3 RotateVector(Vector3 source, float angle, AxisDirection rotationAxis)
        {
            Vector3 result;
            switch (rotationAxis)
            {
                case AxisDirection.X:
                    result = Vector3.Transform(source, Matrix.CreateFromAxisAngle(Vector3.UnitX, angle));
                    break;
                case AxisDirection.Y:
                    result = Vector3.Transform(source, Matrix.CreateFromAxisAngle(Vector3.UnitY, angle));
                    break;
                default:
                    result = Vector3.Transform(source, Matrix.CreateFromAxisAngle(Vector3.UnitZ, angle));
                    break;
            }
            return result;
        }

        /// <summary>
        /// Rotation of a vector around an axis according to a given angle.
        /// </summary>
        /// <param name="source">Source point.</param>
        /// <param name="angle">Rotation angle (in degree).</param>
        /// <param name="axis">Axis vector.</param>
        /// <returns>A new Vector3 object corresponding to the rotation.</returns>
        public static Vector3 RotateVector(Vector3 source, float angle, Vector3 axis)
        {
            return Vector3.Transform(source, Matrix.CreateFromAxisAngle(axis, MathHelper.ToRadians(angle)));
        }
    }
}
