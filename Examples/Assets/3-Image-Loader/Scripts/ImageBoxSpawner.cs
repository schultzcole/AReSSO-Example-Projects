#nullable enable
using System;
using System.Linq;
using ImageLoader.Scripts.State;
using Playdux.src.Store;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

namespace ImageLoader.Scripts
{
    public class ImageBoxSpawner : MonoBehaviour, ISideEffector<ImageLoaderState>
    {
        /// implement <see cref="ISideEffector{TRootState}.Priority"/>
        public int Priority => 0;

        [SerializeField] private UIDocument? imageBoxListDocument;
        [SerializeField] private VisualTreeAsset? imageBoxAsset;

        private VisualElement ImageBoxListContainer => imageBoxListDocument!.rootVisualElement.Query("image-box-container").First();

        private IDisposable[]? previousImageBoxInstanceDisposables;

        public bool PreEffect(DispatchedAction dispatchedAction, IStore<ImageLoaderState> store) => true;

        public void PostEffect(DispatchedAction dispatchedAction, IStore<ImageLoaderState> store)
        {
            switch (dispatchedAction.Action)
            {
                case InitializeAction<ImageLoaderState>:
                    OnInitialized(store);
                    break;
            }
        }

        private void OnInitialized(IStore<ImageLoaderState> store)
        {
            ImageBoxListContainer.Clear();
            
            if (previousImageBoxInstanceDisposables is not null)
            {
                foreach (var disposable in previousImageBoxInstanceDisposables) { disposable.Dispose(); }
            }

            previousImageBoxInstanceDisposables = store.State.Images.Select((_, i) => SetupImageBox(store, i)).ToArray();
        }

        private IDisposable SetupImageBox(IStore<ImageLoaderState> store, int index)
        {
            Debug.Log($"Spawning image box for index {index}");
            
            var imageBoxInstance = imageBoxAsset!.CloneTree();
            ImageBoxListContainer.Add(imageBoxInstance);
            var imgElement = imageBoxInstance.Query<Image>().First();
            var btnElement = imageBoxInstance.Query<Button>().First();
            var btnLabelElement = imageBoxInstance.Query<Button>().Children<Label>().First();

            return store.ObservableFor(s => s.Images[index], true).Subscribe(imageBox =>
            {
                switch (imageBox)
                {
                    case ImageBox.Empty:
                        imgElement.image = Texture2D.whiteTexture;
                        btnLabelElement.text = "Load Image!";
                        btnElement.SetEnabled(true);
                        break;
                    case ImageBox.Loading:
                        imgElement.image = Texture2D.whiteTexture;
                        btnLabelElement.text = "Loading...";
                        btnElement.SetEnabled(false);
                        break;
                    case ImageBox.Loaded loaded:
                        imgElement.image = loaded.Texture;
                        btnLabelElement.text = "Clear!";
                        btnElement.SetEnabled(true);
                        break;
                    default: throw new InvalidOperationException($"Unknown image box type: {imageBox}");
                }
            });
        }
    }
}