﻿using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using myShape;
using myWidthness;

namespace myFivePointStar
{
    public class myFivePointStar : IShape
    {
        private Point startPoint;
        private Point endPoint;
        private IWidthness widthness;

        public string shapeName => "FivePointStar";
        public string shapeImage => "images/shape5Star.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }
        public void addWidthness(IWidthness width) 
        {
            widthness = width;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() {

            var start = startPoint;
            var end = endPoint;

            var center = new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);
            var radius = Math.Min(Math.Abs(start.X - end.X), Math.Abs(start.Y - end.Y)) / 2;

            var element = new Path
            {
                Fill = Brushes.AliceBlue,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Data = CreateStarGeometry(center, radius)
            };

            return element;
        }

        private Geometry CreateStarGeometry(Point center, double radius)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure
            {
                StartPoint = new Point(center.X + radius * Math.Cos(-Math.PI / 2), center.Y + radius * Math.Sin(-Math.PI / 2)),
                IsClosed = true
            };

            for (int i = 1; i <= 5; i++)
            {
                double angle = i * 4 * Math.PI / 5 - Math.PI / 2;
                figure.Segments.Add(new LineSegment(new Point(center.X + radius * Math.Cos(angle), center.Y + radius * Math.Sin(angle)), true));
            }

            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}