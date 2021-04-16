#nullable enable
using System;
using ImageLoader.Scripts.State;
using ImageLoader.Scripts.State.Actions;
using ImageLoader.Scripts.State.Reducers;
using NUnit.Framework;

namespace ImageLoader.Tests.State.Reducers
{
    public class ImageRequestReducerTests
    {
        [Test]
        public void EmptyArrayThrows()
        {
            var initialArray = new ImageBox[0];

            Assert.Throws<ArgumentException>(
                () => ModifyImageSlotReducer.ReduceImageRequest(initialArray, new ImageRequestAction(0, "")),
                "Reducer did not throw ArgumentException when initial array was empty"
            );
        }

        [Test]
        public void InvalidIndexThrows()
        {
            var initialArray = new ImageBox[] { new ImageBox.Empty() };

            Assert.Throws<IndexOutOfRangeException>(
                () => ModifyImageSlotReducer.ReduceImageRequest(initialArray, new ImageRequestAction(1, "")),
                "Reducer did not throw IndexOutOfRangeException when index was out of range"
            );
        }

        [Test]
        public void ImageRequestDoesntChangeArrayLength()
        {
            var initialArray = new ImageBox[] { new ImageBox.Empty() };

            var newArray = ModifyImageSlotReducer.ReduceImageRequest(initialArray, new ImageRequestAction(0, ""));

            Assert.AreEqual(initialArray.Length, newArray.Length, "Reducer changed array length");
        }

        [Test]
        public void ImageRequestChangesSpecifiedIndexToLoadingState()
        {
            var initialArray = new ImageBox[] { new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty() };

            var newArray = ModifyImageSlotReducer.ReduceImageRequest(initialArray, new ImageRequestAction(2, ""));
            
            Assert.IsInstanceOf<ImageBox.Loading>(newArray[2], "Modified slot was not of type Loading");
        }

        [Test]
        public void LoadingSlotHasCorrectURL()
        {
            const string url = "Test URL";
            var initialArray = new ImageBox[] { new ImageBox.Empty() };

            var newArray = ModifyImageSlotReducer.ReduceImageRequest(initialArray, new ImageRequestAction(0, url));

            var slot = newArray[0] as ImageBox.Loading;
            Assert.AreEqual(url, slot!.URL, "Incorrect URL");
        }

        [Test]
        public void ImageRequestDoesntChangeOtherIndices()
        {
            var initialArray = new ImageBox[] { new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty(), new ImageBox.Empty() };

            var newArray = ModifyImageSlotReducer.ReduceImageRequest(initialArray, new ImageRequestAction(2, ""));

            void CheckIndex(int expectedIndex, int actualIndex) =>
                Assert.AreEqual(initialArray[expectedIndex], newArray[actualIndex], $"Expected initial slot {expectedIndex} to match new slot {expectedIndex}");
            
            CheckIndex(0, 0);
            CheckIndex(1, 1);
            CheckIndex(3, 3);
            CheckIndex(4, 4);
        }
    }
}