#nullable enable
using System;
using UnityEngine;

namespace ImageLoader.Scripts.State
{
    /// Represents the collective state of the Image Loader
    /// <remarks>Ideally `Images` would be an `ImmutableArray`</remarks>
    public record ImageLoaderState(ImageBox[] Images);

    /// Represents the state of a specific image in the list
    public abstract record ImageBox(Guid ID)
    {
        private ImageBox() : this(Guid.NewGuid()) { }

        /// The image box does not currently contain an image
        public sealed record Empty : ImageBox;

        /// The image box has sent a request for an image, but it is still in progress
        public sealed record Loading(string URL) : ImageBox;

        /// The image box has an image loaded and ready to display
        public sealed record Loaded(Texture2D Texture) : ImageBox;

        public Empty TransitionTo() => new() { ID = ID };
        public Loading TransitionTo(string url) => new(url) { ID = ID };
        public Loaded TransitionTo(Texture2D texture) => new(texture) { ID = ID };
    }
}