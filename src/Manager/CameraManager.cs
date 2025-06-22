using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace minijam.src.Manager
{
    public static class CameraManager
    {
        public static Camera camera;

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            camera = new(graphicsDevice);
        }
        
        public static void UpdateCameraPosition(Vector2 newPos)
        {
            camera.Position = newPos;
        }   
    }
}