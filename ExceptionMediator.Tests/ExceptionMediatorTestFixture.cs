using NUnit.Framework;
using System;
using Mediator = ExceptionMediatorLibrary.ExceptionMediator;

namespace ExceptionMediator.Tests
{
    [TestFixture]
    public class ExceptionMediatorTestFixture
    {
        [Test]
        public void ExceptionMediator_OnException_ResponseTupleExceptionMustNotBeNull()
        { 
            var result = Mediator.Intercept<object>(() => throw new Exception());

            Assert.That(result.exception, Is.Not.Null);
            Assert.That(result.routineResult, Is.Null);

            Console.WriteLine(result);
        }

        [Test]
        public void ExceptionMediator_OnFormatException_ResponseTupleFormatExceptionMustNotBeNull()
        {
            var invalidFormatInput = "A";

            var result = Mediator.Intercept<object>(() =>
            {
                return int.Parse(invalidFormatInput);
            });

            Assert.Multiple(() =>
            {
                Assert.That(result.exception, Is.Not.Null);
                Assert.That(result.exception.GetType(), Is.EqualTo(typeof(FormatException)));
                Assert.That(result.routineResult, Is.Null);
            });

            Console.WriteLine(result);
        }

        [Test]
        public void ExceptionMediator_OnExplicitReturnTypeCallWithNoException_PassOnValidResultModel()
        {
            var invalidFormatInput = "1";

            var result = Mediator.Intercept<int>(() =>
            {
                return int.Parse(invalidFormatInput);
            });

            Assert.Multiple(() =>
            {
                Assert.That(result.GetType(), Is.EqualTo(typeof((Exception, int))));
                Assert.That(result.exception, Is.Null);

                Assert.That(result.routineResult, Is.Not.Null);
                Assert.That(result.routineResult.GetType(), Is.EqualTo(typeof(int)));
                Assert.That(result.routineResult, Is.Not.EqualTo(default));
                Assert.That(result.routineResult, Is.EqualTo(1));
            });

            Console.WriteLine(result);
        }

        [Test]
        public void ExceptionMediator_OnImpliedReturnTypeCallWithNoException_PassOnValidResultModel()
        {
            var invalidFormatInput = "1";

            var result = Mediator.Intercept(() =>
            {
                return int.Parse(invalidFormatInput);
            });

            Assert.Multiple(() =>
            {
                Assert.That(result.exception, Is.Null);
                Assert.That(result.routineResult.GetType(), Is.EqualTo(typeof(int)));
                Assert.That(result.routineResult, Is.EqualTo(1));
                Assert.That(result.routineResult, Is.Not.EqualTo(default));
            });

            Console.WriteLine(result);
        }
    }
}
