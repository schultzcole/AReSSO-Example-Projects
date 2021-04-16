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

            ReducerValidators.ValidateLength(images);
            ReducerValidators.ValidateIndex(images, index);

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

            ReducerValidators.ValidateLength(images);
            ReducerValidators.ValidateIndex(images, index);

            var newImageBox = images[index].TransitionToLoading(url);
            return SetItem(images, index, newImageBox);
        }

        public static ImageBox[] ReduceImageResult(ImageBox[] images, ImageResultAction action)
        {
            var (index, texture) = action;

            ReducerValidators.ValidateLength(images);
            ReducerValidators.ValidateIndex(images, index);
            var slot = EnforceSlotType<ImageBox.Loading>(images[index]);

            var newImageBox = slot.TransitionToLoaded(texture);
            return SetItem(images, index, newImageBox);
        }

        public static ImageBox[] ReduceClearSlot(ImageBox[] images, ClearImageSlotAction action)
        {
            var index = action.Slot;
            
            ReducerValidators.ValidateLength(images);
            ReducerValidators.ValidateIndex(images, index);
            var slot = EnforceSlotType<ImageBox.Loaded>(images[index]);

            var newImageBox = slot.TransitionToEmpty();
            return SetItem(images, index, newImageBox);
        }

        private static T EnforceSlotType<T>(ImageBox slot) where T : ImageBox
        {
            if (slot is T castSlot) return castSlot;
            throw new InvalidOperationException($"Slot is not expected type {nameof(ImageBox)}.{typeof(T).Name} | actual: {nameof(ImageBox)}.{slot.GetType().Name}");
        }
    }

    internal static class ReducerValidators
    {
        internal static void ValidateLength(ImageBox[] images)
        {
            if (images.Length < 1) throw new ArgumentException("Can't modify slot in empty array", nameof(images));
        }

        internal static void ValidateIndex<T>(T[] images, int index)
        {
            // Attempts to get the element at the given index, throwing if it is an invalid index
            var _ = images[index];
        }
    }
}