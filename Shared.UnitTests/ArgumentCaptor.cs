using Moq;

namespace Shared.UnitTests
{
    public class ArgumentCaptor<TArgument>
    {
        public TArgument Value { get; private set; }

        public TArgument Capture()
        {
            return It.Is<TArgument>((value) => Save(value));
        }

        private bool Save(TArgument value)
        {
            Value = value;
            return true;
        }
    }
}
