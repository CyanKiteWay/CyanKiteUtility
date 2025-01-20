using System.Collections.Generic;
using System.Runtime.InteropServices;
using static CyanKiteUtility.Win32API;

namespace CyanKiteUtility
{
    /// <summary>
    /// 移动坐标类型
    /// </summary>
    public enum CoordinatesDataType
    {
        /// <summary>
        /// 绝对位置
        /// </summary>
        Absolute = 1,
        /// <summary>
        /// 相对位置
        /// </summary>
        Relative = 2,
    }

    /// <summary>
    /// 鼠标按键类型
    /// </summary>
    public enum MouseButtonType
    {
        /// <summary>
        /// 鼠标左键
        /// </summary>
        Left = 1,
        /// <summary>
        /// 鼠标右键
        /// </summary>
        Right = 2,
        /// <summary>
        /// 鼠标中键
        /// </summary>
        Middle = 3,
    }

    /// <summary>
    /// 滚轮方向
    /// </summary>
    public enum ScrollingDirection
    {
        /// <summary>
        /// 向上滚动
        /// </summary>
        Up = 1,
        /// <summary>
        /// 向下滚动
        /// </summary>
        Down = 2,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return ("X:" + X + ", Y:" + Y);
        }
    }

    /// <summary>
    /// 模拟鼠标操作
    /// </summary>
    public class MouseHelper
    {
        //移动鼠标 
        private static readonly int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        private static readonly int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        private static readonly int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        private static readonly int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        private static readonly int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        private static readonly int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        private static readonly int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        private static readonly int MOUSEEVENTF_ABSOLUTE = 0x8000;
        //模拟鼠标滚轮滚动操作，必须配合dwData参数
        private static readonly int MOUSEEVENTF_WHEEL = 0x0800;

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="x">绝对位置代表横坐标，相对位置代表位移</param>
        /// <param name="y">绝对位置代表纵坐标，相对位置代表位移</param>
        /// <param name="type">相对位置移动还是绝对位置移动，默认绝对位置</param>
        public static void Move(int x = 0, int y = 0, CoordinatesDataType type = CoordinatesDataType.Absolute)
        {
            switch (type)
            {
                case CoordinatesDataType.Relative:
                    GetCursorPos(out POINT currentPosition);
                    x = currentPosition.X + x;
                    y = currentPosition.Y + y;
                    break;
                default:
                    break;
            }
            SetCursorPos(x, y);
        }

        /// <summary>
        /// 鼠标压下
        /// </summary>
        /// <param name="button">鼠标对应按键</param>
        public static void Down(MouseButtonType button)
        {
            int operate = 0;
            switch (button)
            {
                case MouseButtonType.Left:
                    operate = MOUSEEVENTF_LEFTDOWN;
                    break;
                case MouseButtonType.Right:
                    operate = MOUSEEVENTF_RIGHTDOWN;
                    break;
                case MouseButtonType.Middle:
                    operate = MOUSEEVENTF_MIDDLEDOWN;
                    break;
                default:
                    break;
            }
            mouse_event(operate, 0, 0, 0, 0);
        }

        /// <summary>
        /// 鼠标弹起
        /// </summary>
        /// <param name="button">鼠标对应按键</param>
        public static void Up(MouseButtonType button)
        {
            int operate = 0;
            switch (button)
            {
                case MouseButtonType.Left:
                    operate = MOUSEEVENTF_LEFTUP;
                    break;
                case MouseButtonType.Right:
                    operate = MOUSEEVENTF_RIGHTUP;
                    break;
                case MouseButtonType.Middle:
                    operate = MOUSEEVENTF_MIDDLEUP;
                    break;
                default:
                    break;
            }
            mouse_event(operate, 0, 0, 0, 0);
        }

        /// <summary>
        /// 鼠标点击
        /// </summary>
        /// <param name="button">鼠标对应按键</param>
        /// <param name="num">鼠标点击次数</param>
        public static void Click(MouseButtonType button, int num = 1)
        {
            for (int i = 0; i < num; i++)
            {
                Down(button);
                Up(button);
            }
        }

        /// <summary>
        /// 鼠标滚轮操作
        /// </summary>
        /// <param name="direction">方向</param>
        /// <param name="distance">距离(自动转为正数)</param>
        public static void RollWheel(ScrollingDirection direction, int distance)
        {
            if (distance < 0)
            {
                distance = 0 - distance;
            }
            switch (direction)
            {
                case ScrollingDirection.Up:
                    break;
                case ScrollingDirection.Down:
                    distance = 0 - distance;
                    break;
                default:
                    break;
            }
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, distance, 0);
        }
    }

    /// <summary>
    /// 键盘按键值
    /// </summary>
    public enum KeyboardValue
    {
        A = 65,
        B = 66,
        C = 67,
        D = 68,
        E = 69,
        F = 70,
        G = 71,
        H = 72,
        I = 73,
        J = 74,
        K = 75,
        L = 76,
        M = 77,
        N = 78,
        O = 79,
        P = 80,
        Q = 81,
        R = 82,
        S = 83,
        T = 84,
        U = 85,
        V = 86,
        W = 87,
        X = 88,
        Y = 89,
        Z = 90,
        Num0 = 48,
        Num1 = 49,
        Num2 = 50,
        Num3 = 51,
        Num4 = 52,
        Num5 = 53,
        Num6 = 54,
        Num7 = 55,
        Num8 = 56,
        Num9 = 57,

        /// <summary>
        /// 回车
        /// </summary>
        Enter = 13,
        /// <summary>
        /// 删除
        /// </summary>
        Backspace = 8,
        /// <summary>
        /// Tab
        /// </summary>
        Tab = 9,
        /// <summary>
        /// Shift
        /// </summary>
        Shift = 16,
        Control = 17,
        Alt = 18,
        CapsLock = 20,
        Esc = 27,
        Spacebar = 32,
        LeftArrow = 37,
        UpArrow = 38,
        RightArrow = 39,
        DownArrow = 40,
        Delete = 46,
        NumLock = 144,
    }

    /// <summary>
    /// 模拟全局键盘操作
    /// </summary>
    public class KeyboardHelper
    {
        //键盘按键落下
        private static readonly uint KEYEVENTF_KEYDOWN = 0;
        //键盘按键抬起
        private static readonly uint KEYEVENTF_KEYUP = 2;

        /// <summary>
        /// 键盘按键压下
        /// </summary>
        /// <param name="value">键盘按键值</param>
        public static void Down(KeyboardValue value)
        {
            keybd_event((byte)value, 0, KEYEVENTF_KEYDOWN, 0);
        }

        /// <summary>
        /// 键盘按键松开
        /// </summary>
        /// <param name="value">键盘按键值</param>
        public static void Up(KeyboardValue value)
        {
            keybd_event((byte)value, 0, KEYEVENTF_KEYUP, 0);
        }

        /// <summary>
        /// 键盘按键敲击一次
        /// </summary>
        /// <param name="value">键盘按键值</param>
        public static void Pressed(KeyboardValue value)
        {
            Down(value);
            Up(value);
        }

        /// <summary>
        /// 键盘组合按键敲击
        /// </summary>
        /// <param name="values">组合按键</param>
        public static void PressedTogether(params KeyboardValue[] values)
        {
            var valuesList = new List<KeyboardValue>(values);
            foreach (var item in valuesList)
            {
                Down(item);
            }

            valuesList.Reverse();
            foreach (var item in valuesList)
            {
                Up(item);
            }
        }
    }
}
