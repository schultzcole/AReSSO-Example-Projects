#nullable enable
using System;
using UnityEngine;

namespace ImageLoader.Scripts.State
{
    /// Represents the collective state of the Image Loader
    /// <remarks>Ideally `Images` would be an `ImmutableArray`</remarks>
    public record ImageLoaderState(ImageBox[] Images);

    /// Represents the state of a specific image in the list
    public abstract record ImageBox
    {
        public Guid ID { get; init; } = Guid.NewGuid();

        // Private constructor prevents extension of ImageBox from outside
        private ImageBox() { }

        /// The image box does not currently contain an image
        public sealed record Empty : ImageBox;

        /// The image box has sent a request for an image, but it is still in progress
        public sealed record Loading(string URL) : ImageBox
        {
            // Only the Loading state can transition to Loaded
            public Loaded TransitionToLoaded(Texture2D texture) => new(texture) { ID = ID };
        }

        /// The image box has an image loaded and ready to display
        public sealed record Loaded(Texture2D Texture) : ImageBox
        {
            public Empty TransitionToEmpty() => new() { ID = ID };
        }

        public Loading TransitionToLoading(string url) => new(url) { ID = ID };
    }
}