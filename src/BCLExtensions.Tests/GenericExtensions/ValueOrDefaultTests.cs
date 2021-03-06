﻿using System;
using System.Collections.Generic;
using System.Reflection;
using BCLExtensions.Tests.TestHelpers;
using Xunit;
using Xunit.Extensions;

namespace BCLExtensions.Tests.GenericExtensions
{
    public class ValueOrDefaultTests
    {
        [Theory]
        [StringData(inputIsNull:true)]
        [ListData(inputIsNull:true)]
        [ObjectData(inputIsNull:true)]
        public void WhenInputNullAndDefaultValueIsNullThrowsException<T>(T input, T defaultValue) where T : class
        {
            Func<T,T,T> valueOrDefault = BCLExtensions.GenericExtensions.GetValueOrDefault<T>;
            Assert.Throws<ArgumentNullException>(valueOrDefault.AsActionUsing(input, null).AsThrowsDelegate());
        }

        [Theory]
        [StringData]
        [ListData]
        [ObjectData]
        public void WhenValidInputAndDefaultValueIsNullThrowsException<T>(T input, T defaultValue) where T : class
        {
            Func<T, T, T> valueOrDefault = BCLExtensions.GenericExtensions.GetValueOrDefault<T>;
            Assert.Throws<ArgumentNullException>(valueOrDefault.AsActionUsing(input, null).AsThrowsDelegate());
        }

        [Theory]
        [StringData]
        [ListData]
        [ObjectData]
        public void WhenInputIsNotNullThenReturnsInputValue<T>(T input, T defaultValue) where T : class
        {
            var result = input.GetValueOrDefault(defaultValue);
            Assert.Equal(input, result);
        }

        [Theory]
        [StringData(inputIsNull:true)]
        [ListData(inputIsNull:true)]
        [ObjectData(inputIsNull: true)]
        public void WhenInputIsNullThenReturnsDefaultValue<T>(T input, T defaultValue) where T : class
        {
            var result = input.GetValueOrDefault(defaultValue);
            Assert.Equal(defaultValue, result);
        }

        public class StringDataAttribute : DataAttribute
        {
            private readonly bool _inputIsNull;

            public StringDataAttribute(bool inputIsNull = false)
            {
                _inputIsNull = inputIsNull;
            }

            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[]
                {
                    _inputIsNull ? null : "Valid Non-null string",
                    "(Default)"
                };
            }
        }

        public class ListDataAttribute : DataAttribute
        {
            private readonly bool _inputIsNull;

            private readonly List<int> _defaultList = new List<int>();

            private readonly List<int> _validList = new List<int>();

            public ListDataAttribute(bool inputIsNull = false)
            {
                _inputIsNull = inputIsNull;
            }

            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[]
                {
                    _inputIsNull ? null : _validList,
                    _defaultList
                };
            }
        }

        public class ObjectDataAttribute : DataAttribute
        {
            private readonly bool _inputIsNull;

            private readonly object _defaultObject = new Object();

            private readonly object _validObject = new Object();

            public ObjectDataAttribute(bool inputIsNull = false)
            {
                _inputIsNull = inputIsNull;
            }

            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new[]
                {
                    _inputIsNull ? null : _validObject,
                    _defaultObject
                };
            }
        }
    }
}