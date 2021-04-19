#nullable enable
using System;
using System.Linq;
using ImageLoader.Scripts.State;
using Playdux.src.Store;
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

        private ImageBoxInstance[]? imageBoxInstances;

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
            
            if (imageBoxInstances is not null)
            {
                foreach (var disposable in imageBoxInstances) { disposable.Dispose(); }
            }

            imageBoxInstances = store.State.Images.Select((_, i) => SetupImageBox(store, i)).ToArray();
        }

        private ImageBoxInstance SetupImageBox(IStore<ImageLoaderState> store, int index)
        {
            var imageBoxInstance = imageBoxAsset!.CloneTree();
            ImageBoxListContainer.Add(imageBoxInstance);
            return new ImageBoxInstance(imageBoxInstance, index, store);
        }
    }
}