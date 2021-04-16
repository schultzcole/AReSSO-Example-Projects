using System;
using ImageLoader.Scripts.State;
using ImageLoader.Scripts.State.Actions;
using ImageLoader.Scripts.State.Reducers;
using NUnit.Framework;
using UnityEngine;

namespace ImageLoader.Tests.State.Reducers
{
    public class ImageResultReducerTests
    {
        [Test]
        public void EmptyArrayThrows()
        {
            var initialArray = new ImageBox[0];

            Assert.Throws<ArgumentException>(
                () => ModifyImageSlotReducer.ReduceImageResult(initialArray, new ImageResultAction(0, Texture2D.whiteTexture)),
                "Reducer did not throw ArgumentException when initial array was empty"
            );
        }

        [Test]
        public void InvalidIndexThrows()
        {
            var initialArray = new ImageBox[] { new ImageBox.Empty() };

            Assert.Throws<IndexOutOfRangeException>(
                () => ModifyImageSlotReducer.ReduceImageResult(initialArray, new ImageResultAction(1, Texture2D.whiteTexture)),
                "Reducer did not throw IndexOutOfRangeException when index was out of range"
            );
        }

        [Test]
        public void ImageResultDoesntChangeArrayLength()
        {
            var initialArray = new ImageBox[] { new ImageBox.Loading("") };

            var newArray = ModifyImageSlotReducer.ReduceImageResult(initialArray, new ImageResultAction(0, Texture2D.whiteTexture));

            Assert.AreEqual(initialArray.Length, newArray.Length, "Reducer changed array length");
        }
        
        
        [Test]
        public void ImageRequestChangesSpecifiedIndexToLoadedState()
        {
            var initialArray = new ImageBox[] { new ImageBox.Loading(""), new ImageBox.Loading(""), new ImageBox.Loading(""), new ImageBox.Loading(""), new ImageBox.Loading("") };

            var newArray = ModifyImageSlotReducer.ReduceImageResult(initialArray, new ImageResultAction(2, Texture2D.whiteTexture));
            
            Assert.IsInstanceOf<ImageBox.Loaded>(newArray[2], "Modified slot was not of type Loaded");
        }

        [Test]
        public void LoadingSlotHasCorrectTexture()
        {
            var initialArray = new ImageBox[] { new ImageBox.Loading("") };

            var newArray = ModifyImageSlotReducer.ReduceImageResult(initialArray, new ImageResultAction(0, Texture2D.whiteTexture));

            var slot = newArray[0] as ImageBox.Loaded;
            Assert.AreEqual(Texture2D.whiteTexture, slot!.Texture, "Incorrect Texture");
        }

        [Test]
        public void ImageRequestDoesntChangeOtherIndices()
        {
            var initialArray = new ImageBox[] { new ImageBox.Loading(""), new ImageBox.Loading(""), new ImageBox.Loading(""), new ImageBox.Loading(""), new ImageBox.Loading("") };

            var newArray = ModifyImageSlotReducer.ReduceImageResult(initialArray, new ImageResultAction(2, Texture2D.whiteTexture));

            void CheckIndex(int expectedIndex, int actualIndex) =>
                Assert.AreEqual(initialArray[expectedIndex], newArray[actualIndex], $"Expected initial slot {expectedIndex} to match new slot {expectedIndex}");
            
            CheckIndex(0, 0);
            CheckIndex(1, 1);
            CheckIndex(3, 3);
            CheckIndex(4, 4);
        }
    }
}