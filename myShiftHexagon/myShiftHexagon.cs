﻿using myShape;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace myShiftHexagon
{
    public class myShiftHexagon : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "ShiftHexagon";
        public string shapeImage => "";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType()
        {
            var start = startPoint;
            var end = endPoint;

            var width = Math.Abs(end.X - start.X);
            var height = Math.Abs(end.Y - start.Y);

            var center = new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);
            var sideLength = Math.Min(width / 2, height / 2);

            var hexagon = new Polygon
            {
                Fill = Brushes.Green,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Points = CreateHexagonPoints(center, sideLength)
            };

            return hexagon;
        }

        private PointCollection CreateHexagonPoints(Point center, double sideLength)
        {
            var points = new PointCollection();

            for (int i = 0; i < 6; i++)
            {
                double angle = Math.PI / 3 * i;
                points.Add(new Point(center.X + sideLength * Math.Cos(angle), center.Y + sideLength * Math.Sin(angle)));
            }

            return points;
        }
    }
}