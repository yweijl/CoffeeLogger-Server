using Moq;
using System;
using System.Linq.Expressions;

namespace Shared.UnitTests
{
    public class ExpressionCaptor<TFrom, TTo>
    {
        public Expression<Func<TFrom, TTo>> Value { get; private set; }

        public Expression<Func<TFrom, TTo>> Capture()
        {
            return It.Is<Expression<Func<TFrom, TTo>>>((value) => Save(value));
        }

        public Func<TFrom, TTo> Compile()
        {
            if (Value == null) throw new ArgumentNullException(nameof(Value));

            return Value.Compile();
        }

        public TTo Invoke(TFrom arg)
        {
            return Compile().Invoke(arg);
        }

        private bool Save(Expression<Func<TFrom, TTo>> value)
        {
            Value = value;
            return true;
        }
    }
}