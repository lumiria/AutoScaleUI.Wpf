#nullable enable

using System;
using System.Windows;
using System.Windows.Media;

namespace AutoScaleUI
{
    public sealed class AutoScaleBehavior : BaseBehavior<FrameworkElement>
    {
        private ScaleTransform? _scaleTransform;
        private double _baseWidth;
        private double _baseHeight;


        /// <summary>
        /// Gets or sets a value that indicates whether to maintain aspect ratio.
        /// </summary>
        public bool MaintainsAspectRaito { get; set; }


        /// <summary>
        /// Gets or sets the base size.
        /// </summary>
        public Size BaseSize { get; set; } = new Size(double.NaN, double.NaN);

        public FrameworkElement? TargetParent
        {
            get => (FrameworkElement?)GetValue(TargetParentProperty);
            set => SetValue(TargetParentProperty, value); 
        }

        /// <summary>
        /// Gets or sets the parent element that is the basis of the scale.
        /// </summary>
        public static readonly DependencyProperty TargetParentProperty =
            DependencyProperty.Register("TargetParent", typeof(FrameworkElement), typeof(AutoScaleBehavior),
                new PropertyMetadata(null, (d, e) =>
                {
                    var self = (AutoScaleBehavior)d;
                    if (e.OldValue is FrameworkElement oldTarget)
                    {
                        oldTarget.SizeChanged -= self.TargetWindow_SizeChanged;
                    }
                    if (e.NewValue is FrameworkElement newTarget)
                    {
                        if (newTarget.IsLoaded)
                        {
                            self.SetBase(newTarget);
                        }
                        else
                        {
                            void NewTarget_OnLoaded(object sender, RoutedEventArgs e)
                            {
                                var target = (FrameworkElement)sender;
                                target.Loaded -= NewTarget_OnLoaded;
                                self.SetBase(target);
                            }
                            newTarget.Loaded += NewTarget_OnLoaded;
                        }
                        newTarget.SizeChanged += self.TargetWindow_SizeChanged;
                    }
                }));

        private double BaseWidth =>
            !double.IsNaN(BaseSize.Width) ? BaseSize.Width : _baseWidth;

        private double BaseHeight =>
            !double.IsNaN(BaseSize.Height) ? BaseSize.Height : _baseHeight;


        protected override void Initialize()
        {
            var element = AssociatedObject;
            var transform = element.LayoutTransform;

            _baseWidth = element.ActualWidth;
            _baseHeight = element.ActualHeight;
            _scaleTransform = new ScaleTransform(1, 1);

            if (transform == null)
            {
                element.LayoutTransform = _scaleTransform;
            }
            else if (transform is TransformGroup group)
            {
                group.Children.Add(_scaleTransform);
            }
            else
            {
                var grp = new TransformGroup();
                grp.Children.Add(transform);
                grp.Children.Add(_scaleTransform);
                element.LayoutTransform = grp;
            }

            Resize(TargetParent);
        }

        protected override void Uninitialize()
        {
            if (TargetParent != null)
            {
                TargetParent.SizeChanged -= TargetWindow_SizeChanged;
            }
        }

        private void SetBase(FrameworkElement targetParent)
        {
            _baseWidth = targetParent.ActualWidth;
            _baseHeight = targetParent.ActualHeight;
            Resize(targetParent);
        }

        private void Resize(FrameworkElement? targetParent)
        {
            if (_scaleTransform == null
                || targetParent == null) return;

            var scaleX = targetParent.ActualWidth / BaseWidth;
            var scaleY = targetParent.ActualHeight / BaseHeight;

            if (MaintainsAspectRaito)
            {
                scaleX = scaleY = Math.Min(scaleX, scaleY);
            }
            _scaleTransform.ScaleX = scaleX;
            _scaleTransform.ScaleY = scaleY;
        }

        private void TargetWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Resize((FrameworkElement)sender);
        }
    }
}
