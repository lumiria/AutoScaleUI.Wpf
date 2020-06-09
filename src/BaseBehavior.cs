#nullable enable

using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace AutoScaleUI
{
    /// <summary>
    /// Provides the behavior base class that handles a safe unsubscribes.
    /// </summary>
    /// <typeparam name="T">The dependency object this behavior should attach to.</typeparam>
    /// <remarks>
    /// Encapsulates state information and zero or more ICommands into an attachable object.
    /// </remarks>
    public abstract class BaseBehavior<T> : Behavior<T> where T : FrameworkElement
    {
        private bool _isLoaded;

        /// <summary>
        /// Initializes the behabior.
        /// Called after an AssociatedObject which bahavior is attached to is loaded.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// UnInitializes the behabior.
        /// Called when an AssociatedObject which bahavior is attached to is being unloaded.
        /// </summary>
        protected abstract void Uninitialize();

        #region Behavior Pattern

        /// <summary>
        /// Called when the element is laid outm rendered, and ready for interaction.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">A RoutedEventArgs that contains the event data.</param>
        protected virtual void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            _isLoaded = true;
            AssociatedObject.Loaded -= OnAssociatedObjectLoaded;
            AssociatedObject.Unloaded += OnAssociatedObjectUnloaded;

            Initialize();
        }

        /// <summary>
        /// Occurs when the element is removed from within an element tree of loaded
        /// elements.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">A RoutedEventArgs that contains the event data.</param>
        protected virtual void OnAssociatedObjectUnloaded(object sender, RoutedEventArgs e)
        {
            if (!_isLoaded) return;

            if (AssociatedObject != null)
            {
                AssociatedObject.Unloaded -= OnAssociatedObjectUnloaded;
                AssociatedObject.Loaded += OnAssociatedObjectLoaded;
            }

            Uninitialize();
            _isLoaded = false;
        }

        #region Behavior<T> Members

        /// <summary>
        /// Called after the bahavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += OnAssociatedObjectLoaded;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            OnAssociatedObjectUnloaded(this, new RoutedEventArgs());
            AssociatedObject.Loaded -= OnAssociatedObjectLoaded;

            base.OnDetaching();
        }

        #endregion Behavior<T> Members

        #endregion Behavior Pattern
    }
}
