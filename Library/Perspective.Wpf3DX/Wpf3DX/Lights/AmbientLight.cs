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
namespace Perspective.Wpf3DX.Lights
{
    /// <summary>
    /// Light object that applies light to objects uniformly, regardless of their shape.
    /// </summary>
    public class AmbientLight : Light
    {
        public AmbientLight()
        {
            Intensity = 0.25f;
        }
    }
}
