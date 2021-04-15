#nullable enable
using System;
using ImageLoader.Scripts.State;
using ImageLoader.Scripts.State.Actions;
using ImageLoader.Scripts.State.Reducers;
using NUnit.Framework;

namespace ImageLoader.Tests.State.Reducers
{
    public class DeleteImageSlotReducerTests
    {
        private static void CheckElements(ImageBox[] initialArray, ImageBox[] newArray, int expectedIndex, int actualIndex) =>
            Assert.AreEqual(
                initialArray[expectedIndex],
                newArray[actualIndex],
                $"Element {actualIndex} of new array does not match element {expectedIndex} of initial array"
            );

        [Test]
        public void InvalidIndexThrows()
        {
            var initialArray = new ImageBox[] { new ImageBox.Empty() };

            Assert.Throws<IndexOutOfRangeException>(() => DeleteImageSlotReducer.Reduce(initialArray, new DeleteImageSlotAction(2)), "Reducer did not throw when index was out of range");
        }

        [Test]
        public void EmptyInitialArrayThrows()
        {
            var initialArray = new ImageBox[0];

            Assert.Throws<ArgumentException>(() => DeleteImageSlotReducer.Reduce(initialArray, new DeleteImageSlotAction(0)), "Reducer did not throw when attempting to delete slot from empty array");
        }

        [Test]
        public void ReducerCopiesArray()
        {
            var initialArray = new ImageBox[] { new ImageBox.Empty() };

            var newArray = DeleteImageSlotReducer.Reduce(initialArray, new DeleteImageSlotAction(0));

            Assert.That(!ReferenceEquals(initialArray, newArray), "Reducer did not produce new array object");
        }

        [Test]
        public void ReducerDeletesCorrectElementMidArray()
        {
            var initialArray = new ImageBox[] { new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty() };

            var newArray = DeleteImageSlotReducer.Reduce(initialArray, new DeleteImageSlotAction(1));

            CheckElements(initialArray, newArray, 0, 0);
            CheckElements(initialArray, newArray, 2, 1);
            CheckElements(initialArray, newArray, 3, 2);
            CheckElements(initialArray, newArray, 4, 3);
        }

        [Test]
        public void ReducerDeletesCorrectElementStartOfArray()
        {
            var initialArray = new ImageBox[] { new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty() };

            var newArray = DeleteImageSlotReducer.Reduce(initialArray, new DeleteImageSlotAction(0));

            CheckElements(initialArray, newArray, 1, 0);
            CheckElements(initialArray, newArray, 2, 1);
            CheckElements(initialArray, newArray, 3, 2);
            CheckElements(initialArray, newArray, 4, 3);
        }

        [Test]
        public void ReducerDeletesCorrectElementEndOfArray()
        {
            var initialArray = new ImageBox[] { new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty() };

            var newArray = DeleteImageSlotReducer.Reduce(initialArray, new DeleteImageSlotAction(4));

            CheckElements(initialArray, newArray, 0, 0);
            CheckElements(initialArray, newArray, 1, 1);
            CheckElements(initialArray, newArray, 2, 2);
            CheckElements(initialArray, newArray, 3, 3);
        }
    }
}