#nullable enable
using System;
using ImageLoader.Scripts.State.Actions;

namespace ImageLoader.Scripts.State.Reducers
{
    public static class AddNewImageSlotReducer
    {
        public static ImageBox[] Reduce(ImageBox[] images, AddNewImageSlotAction action)
        {
            // discard action. We want to make sure the caller *has* an AddNewImageSlotAction to pass in, but the action itself doesn't have any information we need to do the reduce
            var _ = action;

            var newArray = new ImageBox[images.Length + 1];
            Array.Copy(images, 0, newArray, 0, images.Length);
            newArray[newArray.Length - 1] = new ImageBox.Empty();

            return newArray;
        }
    }

    public static class DeleteImageSlotReducer
    {
        public static ImageBox[] Reduce(ImageBox[] images, DeleteImageSlotAction action)
        {
            var index = action.Slot;

            if (images.Length < 1) throw new ArgumentException("Can't delete image slot from empty array", nameof(images));

            // Throw IndexOutOfRangeException if provided index is not valid.
            var _ = images[index];

            var newArray = new ImageBox[images.Length - 1];

            Array.Copy(images, 0, newArray, 0, index);
            Array.Copy(images, index + 1, newArray, index, images.Length - (index + 1));

            return newArray;
        }
    }

    public static class ModifyImageSlotReducer
    {
        private static ImageBox[] SetItem(ImageBox[] images, int index, ImageBox value)
        {
            var newArray = new ImageBox[images.Length];

            Array.Copy(images, 0, newArray, 0, index);
            newArray[index] = value;
            Array.Copy(images, index + 1, newArray, index + 1, images.Length - (index + 1));

            return newArray;
        }

        public static ImageBox[] ReduceImageRequest(ImageBox[] images, ImageRequestAction action)
        {
            var (index, url) = action;

            Validate(images);

            var newImageBox = images[index].TransitionToLoading(url);
            return SetItem(images, index, newImageBox);
        }

        public static ImageBox[] ReduceImageResult(ImageBox[] images, ImageResultAction action)
        {
            var (index, texture) = action;

            Validate(images);

            var slot = images[index] as ImageBox.Loading;
            if (slot is null) throw new ArgumentException($"Specified slot is not {nameof(ImageBox)}.{nameof(ImageBox.Loading)}", $"{nameof(action)}.{nameof(action.Slot)}");

            var newImageBox = slot.TransitionToLoaded(texture);
            return SetItem(images, index, newImageBox);
        }

        private static void Validate(ImageBox[] images)
        {
            if (images.Length < 1) throw new ArgumentException("Can't modify slot in empty array", nameof(images));
        }
    }
}