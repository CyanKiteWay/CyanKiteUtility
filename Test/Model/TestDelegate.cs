namespace Test.Model
{
    public class TestDelegate
    {
        public GetValue testGetValue;

        private GetValue testGetValuePro;

        public GetValue TestGetValuePro
        {
            get { return testGetValuePro; }
            set { testGetValuePro = value; }
        }

        public int Value1 { get; set; }

        public event EventHandler<IntEventArgs> TestEventGetValue;


        public TestDelegate()
        {
            testGetValue += getA;
            testGetValue += getB;
            testGetValue += getC;

            testGetValuePro += getA1;
            testGetValuePro += getB1;
            testGetValuePro += getC1;

            TestEventGetValue += getD;
            TestEventGetValue += getE;
        }

        public int getA(int a1)
        {
            Console.WriteLine($"getA:{a1 + 1}");
            return a1;
        }

        public int getB(int b1)
        {
            Console.WriteLine($"getB:{b1 * 2}");
            return b1;
        }

        public int getC(int c1)
        {
            Console.WriteLine($"getC:{c1 / 3}");
            return c1;
        }

        public void getD(object? d1, IntEventArgs a)
        {
            var obj = d1 as TestDelegate;
            var param = obj?.Value1 ?? 0;
            Console.WriteLine($"getD:{param + a.ValueA}");
        }

        public void getE(object? d1, IntEventArgs a)
        {
            var obj = d1 as TestDelegate;
            var param = obj?.Value1 ?? 0;
            Console.WriteLine($"getE:{param - a.ValueA}");
        }

        public int getA1(int a1)
        {
            Console.WriteLine($"getA1:{a1 + 1}");
            return a1;
        }

        public int getB1(int b1)
        {
            Console.WriteLine($"getB1:{b1 * 2}");
            return b1;
        }

        public int getC1(int c1)
        {
            Console.WriteLine($"getC1:{c1 / 3}");
            return c1;
        }
    }

    public delegate int GetValue(int a);

    public class IntEventArgs
    {
        public int ValueA;
    }   
}
