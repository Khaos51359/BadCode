namespace BadCode
{
    public class BadAppleFrame
    {
        public const int WIDTH = 18;
        public const int HEIGHT = 18;

        private short[,] _frame = new short[WIDTH,HEIGHT];

        public BadAppleFrame(short[,] frame)
        {
            _frame = frame;
        }


        public short[,] GetFrame()
        {
            return _frame;
        }

    }
}
