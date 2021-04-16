#nullable enable
using Playdux.src.Store;
using UnityEngine;

namespace ImageLoader.Scripts.State.Actions
{
    /// Adds a new image slot to the list
    public record AddNewImageSlotAction : IAction;

    /// Deletes a specific slot from the list
    public record DeleteImageSlotAction(int Slot) : IAction;

    /// Begins an image request for a slot
    public record ImageRequestAction(int Slot, string URL) : IAction;

    /// Indicates that an image request has completed
    public record ImageResultAction(int Slot, Texture2D Image) : IAction;
}