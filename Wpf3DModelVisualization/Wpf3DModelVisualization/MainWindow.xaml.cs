using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Wpf3DModelVisualization
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawModel(true); 
        }

        private void DrawModel(bool perspective)
        {
            Model3DGroup modelGroup = new Model3DGroup();

            if (perspective)
            {
                modelGroup.Children.Add(new AmbientLight(Colors.DarkGray));
                modelGroup.Children.Add(new DirectionalLight(Colors.DarkGray, new Vector3D(-1, -1, -1)));
                modelGroup.Children.Add(new DirectionalLight(Colors.Gray, new Vector3D(1, 1, 1)));
            }
            else
            {
                modelGroup.Children.Add(new AmbientLight(Colors.DarkGray));
                modelGroup.Children.Add(new DirectionalLight(Colors.DarkGray, new Vector3D(-1, -1, -1)));
                modelGroup.Children.Add(new DirectionalLight(Colors.Gray, new Vector3D(1, 1, 1)));
            }


            GeometryModel3D geometryModel = new GeometryModel3D
            {
                Geometry = new MeshGeometry3D
                {
                    Positions = new Point3DCollection(new[]
                    {
                        new Point3D(-0.5, -0.5, -0.5),
                        new Point3D(0.5, -0.5, -0.5),
                        new Point3D(0.5, 0.5, -0.5),
                        new Point3D(-0.5, 0.5, -0.5),
                        new Point3D(-0.5, -0.5, 0.5),
                        new Point3D(0.5, -0.5, 0.5),
                        new Point3D(0.5, 0.5, 0.5),
                        new Point3D(-0.5, 0.5, 0.5)
                    }),
                    TriangleIndices = new Int32Collection(new[]
                    {
                        // Передня сторона
                        0, 1, 2, 2, 3, 0,
                        // Задня сторона
                        4, 5, 6, 6, 7, 4,

                        // Ліва сторона
                        0, 3, 7, 7, 4, 0,
                        // Права сторона
                        1, 5, 6, 6, 2, 1,

                        // Верхня сторона
                        2, 6, 7, 7, 3, 2,
                        // Нижня сторона
                        0, 4, 5, 5, 1, 0
                    })
                },
                Material = new MaterialGroup
                {
                    Children =
                    {
                        new DiffuseMaterial(new SolidColorBrush(Colors.DarkBlue)),
                        new DiffuseMaterial(new SolidColorBrush(Colors.DarkBlue)),
                        new DiffuseMaterial(new SolidColorBrush(Colors.DarkBlue)),
                        new DiffuseMaterial(new SolidColorBrush(Colors.DarkBlue)),
                        new DiffuseMaterial(new SolidColorBrush(Colors.DarkBlue)),
                        new DiffuseMaterial(new SolidColorBrush(Colors.DarkBlue))
                    }
                }
            };

            modelGroup.Children.Add(geometryModel);

            if (Content is Viewport3D viewport3D)
            {
                ModelVisual3D modelVisual = new ModelVisual3D { Content = modelGroup };
                viewport3D.Children.Add(modelVisual);

                if (!perspective)
                {

                    OrthographicCamera orthographicCamera = new OrthographicCamera
                    {
                        Position = new Point3D(2, 2, 2),
                        LookDirection = new Vector3D(-1, -1, -1),
                        UpDirection = new Vector3D(0, 1, 0)
                    };
                    viewport3D.Camera = orthographicCamera;
                }
            }
        }
    }
}
