#nullable enable
using System;
using ImageLoader.Scripts.State.Actions;
using ImageLoader.Scripts.State.Reducers;
using Playdux.src.Store;

namespace ImageLoader.Scripts.State
{
    public class ImageLoaderStore : StoreBehaviour<ImageLoaderState>
    {
        protected override Store<ImageLoaderState> InitializeStore()
        {
            return new(new ImageLoaderState(new ImageBox[0]), RootReducer);
        }

        private static ImageLoaderState RootReducer(ImageLoaderState state, IAction action)
        {
            return action switch
            {
                AddNewImageSlotAction a => state with { Images = AddNewImageSlotReducer.Reduce(state.Images, a) },
                DeleteImageSlotAction a => state with { Images = DeleteImageSlotReducer.Reduce(state.Images, a) },
                ImageRequestAction a => state with { Images = ModifyImageSlotReducer.ReduceImageRequest(state.Images, a) },
                ImageResultAction a => state with { Images = ModifyImageSlotReducer.ReduceImageResult(state.Images, a) },
                _ => throw new ArgumentException($"Unrecognized action type: {action.GetType().Name}", nameof(action))
            };
        }
    }
}
