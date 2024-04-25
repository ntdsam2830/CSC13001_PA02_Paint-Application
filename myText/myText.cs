﻿using myColor;
using myShape;
using myStroke;
using myWidthness;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace myText
{
    public class myText : IShape
    {
        TextBox myTextBox;
        string myTextString = "";

        Point startPoint;
        Point endPoint;

        string fontFamily;
        int fontSize;

        private IColor colorValue;
        private bool isFill;

        public string shapeName => "Text";
        public string shapeImage => "";

        public void addStartPoint(Point point) 
        { 
            startPoint = point;
            endPoint = new Point(startPoint.X + fontSize * 10, startPoint.Y + fontSize * 2);
        }
        public void addEndPoint(Point point) 
        {
            endPoint = point;
        }
        public void addWidthness(IWidthness width) {}
        public void addStrokeStyle(IStroke stroke) {}
        public void addPointList(List<Point> pointList) { }
        public void addFontSize(int size) { fontSize = size; }
        public void addFontFamily(string family) { fontFamily = family; }
        public void addColor(IColor color)
        {
            colorValue = color;
        }
        public TextBox getTextBox() { return myTextBox; }
        public void setTextString(string text) 
        { 
            myTextString = text;
        }
        public void setShapeFill(bool isShapeFill)
        {
            isFill = isShapeFill;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType()
        {
            var left = Math.Min(startPoint.X, endPoint.X);
            var right = Math.Max(startPoint.X, endPoint.X);

            var top = Math.Min(startPoint.Y, endPoint.Y);
            var bottom = Math.Max(startPoint.Y, endPoint.Y);

            var width = right - left;
            var height = bottom - top;

            Canvas canvas = new Canvas();
            Rectangle rectangle = new Rectangle()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection() { 10, 2 },
                Width = width,
                Height = height,
            };

            Canvas.SetLeft(rectangle, left);
            Canvas.SetTop(rectangle, top);

            myTextBox = new TextBox()
            {
                FontFamily = new FontFamily(fontFamily),
                FontSize = fontSize,
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                Foreground = colorValue.colorValue,
                Text = myTextString,
                Width = width,
                Height = height,
            };

            Canvas.SetLeft(myTextBox, left);
            Canvas.SetTop(myTextBox, top);

            canvas.Children.Add(rectangle);
            canvas.Children.Add(myTextBox);

            return canvas;
        }
    }
}
