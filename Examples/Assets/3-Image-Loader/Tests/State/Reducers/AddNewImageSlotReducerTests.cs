#nullable enable
using ImageLoader.Scripts.State;
using ImageLoader.Scripts.State.Actions;
using ImageLoader.Scripts.State.Reducers;
using NUnit.Framework;

namespace ImageLoader.Tests.State.Reducers
{
    public class AddNewImageSlotReducerTests
    {
        [Test]
        public void AddToEmptyListCreatesNewArray()
        {
            var initialArray = new ImageBox[0];

            var newArray = AddNewImageSlotReducer.Reduce(initialArray, new AddNewImageSlotAction());
            
            Assert.That(!ReferenceEquals(initialArray, newArray), "Reducer did not return a new object");
        }

        [Test]
        public void AddToEmptyListAddsOneNewElement()
        {
            var initialArray = new ImageBox[0];

            var newArray = AddNewImageSlotReducer.Reduce(initialArray, new AddNewImageSlotAction());

            Assert.AreEqual(1, newArray.Length, "Reducer did not produce correct array length");
        }

        [Test]
        public void AddedElementIsEmptyImageBox()
        {
            var initialArray = new ImageBox[0];

            var newArray = AddNewImageSlotReducer.Reduce(initialArray, new AddNewImageSlotAction());
            
            Assert.IsInstanceOf<ImageBox.Empty>(newArray[0], "Reducer produced wrong type of ImageBox in new slot");
        }

        [Test]
        public void AddToFullListAddsNewElementToEnd()
        {
            var initialArray = new ImageBox[] { new ImageBox.Empty(), new ImageBox.Empty() };

            var newArray = AddNewImageSlotReducer.Reduce(initialArray, new AddNewImageSlotAction());
            
            Assert.That(ReferenceEquals(initialArray[0], newArray[0]), "Reducer did not keep position of existing item 0");
            Assert.That(ReferenceEquals(initialArray[1], newArray[1]), "Reducer did not keep position of existing item 1");
            Assert.AreNotEqual(initialArray[0], newArray[2], "New item matches existing item 0");
            Assert.AreNotEqual(initialArray[1], newArray[2], "New item matches existing item 1");
        }
    }
}