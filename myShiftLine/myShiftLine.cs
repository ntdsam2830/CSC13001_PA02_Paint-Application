﻿using myShape;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using myWidthness;
using myStroke;
using myColor;
using System.Windows.Controls;

namespace myShiftLine
{
    public class myShiftLine : IShape
    {
        private Point startPoint;
        private Point endPoint;
        private IWidthness widthness;
        private IStroke strokeStyle;
        private IColor colorValue;
        private bool isFill;

        public string shapeName => "ShiftLine";
        public string shapeImage => "";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }
        public void addWidthness(IWidthness width)
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
        public void setFocus(bool focus) { }
        public void setBold(bool bold) { }
        public void setItalic(bool italic) { }
        public void setBackground(byte r, byte g, byte b) { }
        public Point getStartPoint()
        {
            return startPoint;
        }
        public Point getEndPoint()
        {
            return endPoint;
        }
        public Point getCenterPoint()
        {
            return new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
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

            if (startPoint.X < endPoint.X && startPoint.Y < endPoint.Y)
            {
                if (width > height)
                {
                    endPoint = new Point(startPoint.X + height, startPoint.Y + height);
                }
                else
                {
                    endPoint = new Point(startPoint.X + width, startPoint.Y + width);
                }

                if (width >= height * 2)
                {
                    endPoint = new Point(startPoint.X + width, startPoint.Y);
                }

                if (height >= width * 2)
                {
                    endPoint = new Point(startPoint.X, startPoint.Y + height);
                }
            }
            else if (startPoint.X < endPoint.X && startPoint.Y > endPoint.Y)
            {
                if (width > height)
                {
                    endPoint = new Point(startPoint.X + height, startPoint.Y - height);
                }
                else
                {
                    endPoint = new Point(startPoint.X + width, startPoint.Y - width);
                }

                if (width >= height * 2)
                {
                    endPoint = new Point(startPoint.X + width, startPoint.Y);
                }

                if (height >= width * 2)
                {
                    endPoint = new Point(startPoint.X, startPoint.Y - height);
                }
            }
            else if (startPoint.X > endPoint.X && startPoint.Y < endPoint.Y)
            {
                if (width > height)
                {
                    endPoint = new Point(startPoint.X - height, startPoint.Y + height);
                }
                else
                {
                    endPoint = new Point(startPoint.X - width, startPoint.Y + width);
                }

                if (width >= height * 2)
                {
                    endPoint = new Point(startPoint.X - width, startPoint.Y);
                }

                if (height >= width * 2)
                {
                    endPoint = new Point(startPoint.X, startPoint.Y + height);
                }
            }
            else if (startPoint.X > endPoint.X && startPoint.Y > endPoint.Y)
            {
                if (width > height)
                {
                    endPoint = new Point(startPoint.X - height, startPoint.Y - height);
                }
                else
                {
                    endPoint = new Point(startPoint.X - width, startPoint.Y - width);
                }

                if (width >= height * 2)
                {
                    endPoint = new Point(startPoint.X - width, startPoint.Y);
                }

                if (height >= width * 2)
                {
                    endPoint = new Point(startPoint.X, startPoint.Y - height);
                }
            }

            if (isFill)
            {
                return new Line()
                {
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = endPoint.X,
                    Y2 = endPoint.Y,
                    Stroke = colorValue.colorValue,
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Fill = colorValue.colorValue,
                };
            } else
            {
                return new Line()
                {
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = endPoint.X,
                    Y2 = endPoint.Y,
                    Stroke = colorValue.colorValue,
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                };
            }
        }
    }
}
