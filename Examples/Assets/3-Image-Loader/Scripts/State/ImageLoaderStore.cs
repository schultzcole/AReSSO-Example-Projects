#nullable enable
using System;
using ImageLoader.Scripts.State.Actions;
using ImageLoader.Scripts.State.Reducers;
using Playdux.src.Store;
using UnityEngine;

namespace ImageLoader.Scripts.State
{
    [DefaultExecutionOrder(-1)]
    public class ImageLoaderStore : StoreBehaviour<ImageLoaderState>
    {
        [SerializeField] private ImageBoxSpawner? spawnerSideEffector;

        protected override Store<ImageLoaderState> InitializeStore()
        {
            return new(
                new ImageLoaderState(new ImageBox[] { new ImageBox.Empty(), new ImageBox.Loading("blah"), new ImageBox.Loaded(Texture2D.normalTexture) }),
                RootReducer,
                new[] { spawnerSideEffector! }
            );
        }

        private static ImageLoaderState RootReducer(ImageLoaderState state, IAction action)
        {
            return action switch
            {
                AddNewImageSlotAction a => state with { Images = AddNewImageSlotReducer.Reduce(state.Images, a) },
                DeleteImageSlotAction a => state with { Images = DeleteImageSlotReducer.Reduce(state.Images, a) },
                ImageRequestAction a => state with { Images = ModifyImageSlotReducer.ReduceImageRequest(state.Images, a) },
                ImageResultAction a => state with { Images = ModifyImageSlotReducer.ReduceImageResult(state.Images, a) },
                InitializeAction<ImageLoaderState> => state,
                _ => throw new ArgumentException($"Unrecognized action type: {action.GetType().Name}", nameof(action))
            };
        }
    }
}