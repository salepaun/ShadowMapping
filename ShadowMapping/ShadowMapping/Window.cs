﻿using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace ShadowMapping
{
    // This tutorial is split up into multiple different bits, one for each type of light.
    
    // The following is the code for the directional light, a light that has a direction but no position.
    public class Window : GameWindow
    {
        private readonly float[] cubeVerticies =
        {
            // Positions          Normals              Texture coords
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
        
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
        
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
        
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
        
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
        
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
        };

        private readonly float[] planeVerticies =
        {
            -10f,  0f, -10f,  
             10f,  0f, -10f,  
             10f,  0f,  10f,  
            -10f,  0f, -10f,  
             10f,  0f,  10f,
            -10f,  0f,  10f, 
        };
        
        // We draw multiple different cubes and it helps to store all
        // their positions in an array for later when we want to draw them
        private readonly Vector3[] cubePositions =
        {
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(0.0f, 5.0f, 0.0f),
            new Vector3(-1.5f, 2.2f, -2.5f),
//            new Vector3(-3.8f, 2.0f, -12.3f),
//            new Vector3(2.4f, 0.4f, -3.5f),
//            new Vector3(-1.7f, 3.0f, -7.5f),
//            new Vector3(1.3f, 2.0f, -2.5f),
//            new Vector3(1.5f, 2.0f, -2.5f),
//            new Vector3(1.5f, 0.2f, -1.5f),
//            new Vector3(-1.3f, 1.0f, -1.5f)
        };
        
        private readonly Vector3 _lightPos = new Vector3(1.2f, 1.0f, 2.0f);

        private int cubeVertexBufferObject;
        private int planeVertexBufferObject;
        private int _vaoModel;
        private int _vaoLamp;
        private int _vaoPlane;

        private Shader lampShader;
        private Shader lightingShader;
        private Texture diffuseMap;
        private Texture specularMap;
        
        private Camera camera;
        private bool _firstMove = true;
        private Vector2 _lastPos;

        public Window(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            GL.Enable(EnableCap.DepthTest);

            cubeVertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, cubeVertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, cubeVerticies.Length * sizeof(float), cubeVerticies, BufferUsageHint.StaticDraw);

            lightingShader = new Shader("Shaders/shader.vert", "Shaders/lighting.frag");
            lampShader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            diffuseMap = new Texture(@"C:\Git\LearnOpenGL-TK\Chapter 2\5 - Light casters - directional lights\Resources\container2.png");
            specularMap = new Texture(@"C:\Git\LearnOpenGL-TK\Chapter 2\5 - Light casters - directional lights\Resources\container2_specular.png");

            _vaoModel = GL.GenVertexArray();
            GL.BindVertexArray(_vaoModel);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, cubeVertexBufferObject);

            var positionLocation = lightingShader.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

            var normalLocation = lightingShader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            
            var texCoordLocation = lightingShader.GetAttribLocation("aTexCoords");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

            _vaoLamp = GL.GenVertexArray();
            GL.BindVertexArray(_vaoLamp);

            GL.BindBuffer(BufferTarget.ArrayBuffer, cubeVertexBufferObject);

            positionLocation = lampShader.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            
            planeVertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, planeVertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, planeVerticies.Length * sizeof(float), planeVerticies, BufferUsageHint.StaticDraw);
            
            _vaoPlane = GL.GenVertexArray();
            GL.BindVertexArray(_vaoPlane);

            GL.BindBuffer(BufferTarget.ArrayBuffer, planeVertexBufferObject);

            positionLocation = lampShader.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            camera = new Camera(Vector3.UnitY * 3 + Vector3.UnitZ * 10, Width / (float) Height);
            
            CursorVisible = false;
            
            base.OnLoad(e);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(_vaoModel);

            diffuseMap.Use();
            specularMap.Use(TextureUnit.Texture1);
            lightingShader.Use();
            
            lightingShader.SetMatrix4("view", camera.GetViewMatrix());
            lightingShader.SetMatrix4("projection", camera.GetProjectionMatrix());
            
            lightingShader.SetVector3("viewPos", camera.Position);
            
            lightingShader.SetInt("material.diffuse", 0);
            lightingShader.SetInt("material.specular", 1);
            lightingShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            lightingShader.SetFloat("material.shininess", 32.0f);
            
            // Directional light needs a direction, in this example we just use (-0.2, -1.0, -0.3f) as the lights direction
            lightingShader.SetVector3("light.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            lightingShader.SetVector3("light.ambient",  new Vector3(0.2f));
            lightingShader.SetVector3("light.diffuse",  new Vector3(0.5f));
            lightingShader.SetVector3("light.specular", new Vector3(1.0f));

            var axis = new Vector3(0.5f, 0f, 0.5f);
            axis.Normalize();

            // We want to draw all the cubes at their respective positions
            for (int i = 0; i < cubePositions.Length; i++)
            {
                // First we create a model from an identity matrix
                Matrix4 model = Matrix4.Identity;
                // We then calculate the angle and rotate the model around an axis
                float angle = 20.0f * i;
                model *= Matrix4.CreateFromAxisAngle(axis, angle);
                // Then we translate said matrix by the cube position
                model *= Matrix4.CreateTranslation(cubePositions[i]);
                // Remember to set the model at last so it can be used by opentk
                lightingShader.SetMatrix4("model", model);
                
                // At last we draw all our cubes
                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);                
            }

            GL.BindVertexArray(_vaoLamp);
            
            lampShader.Use();

            Matrix4 lampMatrix = Matrix4.Identity;
            lampMatrix *= Matrix4.CreateScale(0.2f);
            lampMatrix *= Matrix4.CreateTranslation(_lightPos);
            
            lampShader.SetMatrix4("model", lampMatrix);
            lampShader.SetMatrix4("view", camera.GetViewMatrix());
            lampShader.SetMatrix4("projection", camera.GetProjectionMatrix());
            
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            GL.BindVertexArray(_vaoPlane);
            
            lightingShader.Use();
            lightingShader.SetMatrix4("model", Matrix4.Identity);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

            SwapBuffers();

            base.OnRenderFrame(e);
        }

        
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Focused)
            {
                return;
            }

            var input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            
            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;
            
            
            if (input.IsKeyDown(Key.W))
                camera.Position += camera.Front * cameraSpeed * (float)e.Time; // Forward 
            if (input.IsKeyDown(Key.S))
                camera.Position -= camera.Front * cameraSpeed * (float)e.Time; // Backwards
            if (input.IsKeyDown(Key.A))
                camera.Position -= camera.Right * cameraSpeed * (float)e.Time; // Left
            if (input.IsKeyDown(Key.D))
                camera.Position += camera.Right * cameraSpeed * (float)e.Time; // Right
            if (input.IsKeyDown(Key.Space))
                camera.Position += camera.Up * cameraSpeed * (float)e.Time; // Up 
            if (input.IsKeyDown(Key.LShift))
                camera.Position -= camera.Up * cameraSpeed * (float)e.Time; // Down

            var mouse = Mouse.GetState();

            if (_firstMove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);
                
                camera.Yaw += deltaX * sensitivity;
                camera.Pitch -= deltaY * sensitivity;
            }
            
            base.OnUpdateFrame(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (Focused)
            {
                Mouse.SetPosition(X + Width/2f, Y + Height/2f);
            }
            
            base.OnMouseMove(e);
        }
        
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            camera.Fov -= e.DeltaPrecise;
            base.OnMouseWheel(e);
        }

        
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            camera.AspectRatio = Width / (float)Height;
            base.OnResize(e);
        }


        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteBuffer(cubeVertexBufferObject);
            GL.DeleteVertexArray(_vaoModel);
            GL.DeleteVertexArray(_vaoLamp);

            GL.DeleteProgram(lampShader.Handle);
            GL.DeleteProgram(lightingShader.Handle);

            base.OnUnload(e);
        }
    }
}