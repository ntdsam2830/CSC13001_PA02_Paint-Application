﻿using myColor;
using myShape;
using myStroke;
using myWidthness;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace myArrow
{
    public class myArrow : IShape
    {
        private Point startPoint;
        private Point endPoint;
        private IWidthness widthness;
        private IStroke strokeStyle;
        private IColor colorValue;
        private bool isFill;

        public string shapeName => "Arrow";
        public string shapeImage => "images/shapeArrow.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }
        public void addWidthness (IWidthness width)
        {
            widthness = width;
        }
        public void addStrokeStyle(IStroke stroke) 
        {
            strokeStyle = stroke;
        }
        public void addColor(IColor color)
        {
            colorValue = color;
        }
        public void addPointList(List<Point> pointList) { }
        public void addFontSize(int fontSize) { }
        public void addFontFamily(string fontFamily) { }
        public TextBox getTextBox() { return null; }
        public void setTextString(string text) { }
        public void setShapeFill(bool isShapeFill)
        {
            isFill = isShapeFill;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType() {
            Point center = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);

            var left = Math.Min(startPoint.X, endPoint.X);
            var right = Math.Max(startPoint.X, endPoint.X);

            var top = Math.Min(startPoint.Y, endPoint.Y);
            var bottom = Math.Max(startPoint.Y, endPoint.Y);

            var width = right - left;
            var height = bottom - top;

            string status = "";

            if (startPoint.X < endPoint.X && startPoint.Y < endPoint.Y) 
            {
                status = "normal";
            } else if (startPoint.X < endPoint.X && startPoint.Y > endPoint.Y)
            {
                status = "upside";
            } else if (startPoint.X > endPoint.X && startPoint.Y < endPoint.Y)
            {
                status = "reverse";
            } else if (startPoint.X > endPoint.X && startPoint.Y > endPoint.Y)
            {
                status = "upside-reverse";
            }

            Path element;

            if (isFill)
            {
                element = new Path
                {
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Stroke = colorValue.colorValue,
                    Fill = colorValue.colorValue,
                    Data = CreateArrowGeometry(center, width, height, status)
                };
            } else
            {
                element = new Path
                {
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Stroke = colorValue.colorValue,
                    Data = CreateArrowGeometry(center, width, height, status)
                };
            }

            return element;
        }

        private Geometry CreateArrowGeometry(Point center, double width, double height, string status)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure();

            if (status == "normal")
            {
                figure.StartPoint = new Point(center.X, startPoint.Y);
                figure.IsClosed = true;

                figure.Segments.Add(new LineSegment(new Point(endPoint.X, center.Y - height / 6), true));
                figure.Segments.Add(new LineSegment(new Point(center.X + width / 6, center.Y - height / 6), true));
                figure.Segments.Add(new LineSegment(new Point(center.X + width / 6, endPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 6, endPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 6, center.Y - height / 6), true));
                figure.Segments.Add(new LineSegment(new Point(startPoint.X, center.Y - height / 6), true));
            } else if (status == "upside")
            {
                figure.StartPoint = new Point(center.X, endPoint.Y);
                figure.IsClosed = true;

                figure.Segments.Add(new LineSegment(new Point(endPoint.X, center.Y - height / 6), true));
                figure.Segments.Add(new LineSegment(new Point(center.X + width / 6, center.Y - height / 6), true));
                figure.Segments.Add(new LineSegment(new Point(center.X + width / 6, startPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 6, startPoint.Y), true)); 
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 6, center.Y - height / 6), true));
                figure.Segments.Add(new LineSegment(new Point(startPoint.X, center.Y - height / 6), true));
            } else if (status == "reverse")
            {
                figure.StartPoint = new Point(center.X, startPoint.Y);
                figure.IsClosed = true;

                figure.Segments.Add(new LineSegment(new Point(endPoint.X, center.Y - height / 6), true));
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 6, center.Y - height / 6), true));
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 6, endPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X + width / 6, endPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X + width / 6, center.Y - height / 6), true));
                figure.Segments.Add(new LineSegment(new Point(startPoint.X, center.Y - height / 6), true));
            } else  if (status == "upside-reverse")
            {
                figure.StartPoint = new Point(center.X, endPoint.Y);
                figure.IsClosed = true;

                figure.Segments.Add(new LineSegment(new Point(endPoint.X, center.Y - height / 6), true));

                figure.Segments.Add(new LineSegment(new Point(center.X - width / 6, center.Y - height / 6), true));
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 6, startPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X + width / 6, startPoint.Y), true));

                figure.Segments.Add(new LineSegment(new Point(center.X + width / 6, center.Y - height / 6), true));
                figure.Segments.Add(new LineSegment(new Point(startPoint.X, center.Y - height / 6), true));
            }

            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}
