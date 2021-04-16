using System;
using ImageLoader.Scripts.State;
using ImageLoader.Scripts.State.Actions;
using ImageLoader.Scripts.State.Reducers;
using NUnit.Framework;
using UnityEngine;

namespace ImageLoader.Tests.State.Reducers
{
    public class ClearSlotReducerTests
    {
        [Test]
        public void EmptyArrayThrows()
        {
            var initialArray = new ImageBox[0];

            Assert.Throws<ArgumentException>(
                () => ModifyImageSlotReducer.ReduceClearSlot(initialArray, new ClearImageSlotAction(0)),
                "Reducer did not throw ArgumentException when initial array was empty"
            );
        }

        [Test]
        public void InvalidIndexThrows()
        {
            var initialArray = new ImageBox[] { new ImageBox.Empty() };

            Assert.Throws<IndexOutOfRangeException>(
                () => ModifyImageSlotReducer.ReduceClearSlot(initialArray, new ClearImageSlotAction(1)),
                "Reducer did not throw IndexOutOfRangeException when index was out of range"
            );
        }

        [Test]
        public void ImageResultDoesntChangeArrayLength()
        {
            var initialArray = new ImageBox[] { new ImageBox.Loaded(Texture2D.whiteTexture) };

            var newArray = ModifyImageSlotReducer.ReduceClearSlot(initialArray, new ClearImageSlotAction(0));

            Assert.AreEqual(initialArray.Length, newArray.Length, "Reducer changed array length");
        }

        [Test]
        public void ImageResultForNonLoadingSlotThrows()
        {
            var initialArray = new ImageBox[] { new ImageBox.Empty() };

            Assert.Throws<InvalidOperationException>(
                () => ModifyImageSlotReducer.ReduceClearSlot(initialArray, new ClearImageSlotAction(0)),
                "Reducer did not throw Argument exception when targeting non Loading slot"
            );
        }

        [Test]
        public void ImageResultChangesSpecifiedIndexToEmptyState()
        {
            var initialArray = new ImageBox[]
            {
                new ImageBox.Loaded(Texture2D.whiteTexture), new ImageBox.Loaded(Texture2D.whiteTexture), new ImageBox.Loaded(Texture2D.whiteTexture), new ImageBox.Loaded(Texture2D.whiteTexture),
                new ImageBox.Loaded(Texture2D.whiteTexture)
            };

            var newArray = ModifyImageSlotReducer.ReduceClearSlot(initialArray, new ClearImageSlotAction(2));

            Assert.IsInstanceOf<ImageBox.Empty>(newArray[2], "Modified slot was not of type Empty");
        }

        [Test]
        public void ImageResultDoesntChangeOtherIndices()
        {
            var initialArray = new ImageBox[]
            {
                new ImageBox.Loaded(Texture2D.whiteTexture), new ImageBox.Loaded(Texture2D.whiteTexture), new ImageBox.Loaded(Texture2D.whiteTexture), new ImageBox.Loaded(Texture2D.whiteTexture),
                new ImageBox.Loaded(Texture2D.whiteTexture)
            };

            var newArray = ModifyImageSlotReducer.ReduceClearSlot(initialArray, new ClearImageSlotAction(2));

            void CheckIndex(int expectedIndex, int actualIndex) =>
                Assert.AreEqual(initialArray[expectedIndex], newArray[actualIndex], $"Expected initial slot {expectedIndex} to match new slot {expectedIndex}");

            CheckIndex(0, 0);
            CheckIndex(1, 1);
            CheckIndex(3, 3);
            CheckIndex(4, 4);
        }
    }
}