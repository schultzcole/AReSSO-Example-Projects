#nullable enable
using System;
using ImageLoader.Scripts.State;
using ImageLoader.Scripts.State.Actions;
using Playdux.src.Store;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

namespace ImageLoader.Scripts
{
    public record ImageBoxInstance : IDisposable
    {
        private readonly IStore<ImageLoaderState> store;
        private readonly IDisposable? stateSubscription;

        private readonly int slot;

        private readonly Image imgElement;
        private readonly Button btnElement;
        private readonly Label btnLabelElement;

        public ImageBoxInstance(VisualElement ui, int slot, IStore<ImageLoaderState> store)
        {
            this.slot = slot;
            this.store = store;

            imgElement = ui.Query<Image>().First();
            btnElement = ui.Query<Button>().First();
            btnLabelElement = ui.Query<Button>().Children<Label>().First();

            stateSubscription = store.ObservableFor(state => state.Images[slot], true)
                .ObserveOnMainThread()
                .Subscribe(imageBox =>
                {
                    switch (imageBox)
                    {
                        case ImageBox.Empty:
                            OnEmpty();
                            break;
                        case ImageBox.Loading:
                            OnLoading();
                            break;
                        case ImageBox.Loaded loaded:
                            OnLoaded(loaded);
                            break;
                        default: throw new InvalidOperationException($"Unknown image box type: {imageBox}");
                    }
                }, Debug.LogError);
        }

        private void OnEmpty()
        {
            imgElement.image = null;
            btnLabelElement.text = "Load Image!";
            btnElement.SetEnabled(true);
            btnElement.clicked -= OnClearClicked;
            btnElement.clicked += OnLoadClicked;
        }

        private void OnLoading()
        {
            imgElement.image = Texture2D.whiteTexture;
            btnLabelElement.text = "Loading...";
            btnElement.SetEnabled(false);
            btnElement.clicked -= OnClearClicked;
            btnElement.clicked -= OnLoadClicked;
        }

        private void OnLoaded(ImageBox.Loaded loaded)
        {
            imgElement.image = loaded.Texture;
            btnLabelElement.text = "Clear!";
            btnElement.SetEnabled(true);
            btnElement.clicked -= OnLoadClicked;
            btnElement.clicked += OnClearClicked;
        }

        private void OnLoadClicked() => store.Dispatch(new ImageRequestAction(slot, "https://picsum.photos/200"));

        private void OnClearClicked() => store.Dispatch(new ClearImageSlotAction(slot));

        public void Dispose() { stateSubscription?.Dispose(); }
    }
}