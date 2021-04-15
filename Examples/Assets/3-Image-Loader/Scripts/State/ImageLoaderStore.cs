#nullable enable
using Playdux.src.Store;

namespace ImageLoader.Scripts.State
{
    public class ImageLoaderStore : StoreBehaviour<ImageLoaderState>
    {
        protected override Store<ImageLoaderState> InitializeStore()
        {
            return new(new ImageLoaderState(new ImageBox[0]), RootReducer);
        }

        private static ImageLoaderState RootReducer(ImageLoaderState state, IAction action) => state;
    }
}
