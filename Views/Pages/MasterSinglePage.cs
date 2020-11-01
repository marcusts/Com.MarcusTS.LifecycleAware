namespace Com.MarcusTS.LifecycleAware.Views.Pages
{
   using Com.MarcusTS.SharedForms.Common.Utils;
   using Com.MarcusTS.SharedUtils.Utils;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;
   using System.Threading.Tasks;
   using Xamarin.Forms;

   public interface IMasterSinglePage
   {
      bool AllowInputWhileIsBusy { get; set; }

      View CurrentStage { get; set; }
      bool IsBusyShowing { get; set; }
      Task[] StagedAddedAnimation { get; set; }

      Task[] StagedRemovedAnimation { get; set; }

      Task AddStage(View overlay, bool doNotFocus, bool keepOthers);

      Task RemoveStage(View overlay);
   }

   public class MasterSinglePage : ContentPageWithLifecycle, IMasterSinglePage
   {
      private const double INDICATOR_WIDTH_HEIGHT = 50;
      private static readonly Color OVERLAY_BACKGROUND_COLOR = Color.FromRgba(250, 250, 250, 50);
      private readonly RelativeLayout _backgroundOverlay;

      private readonly RelativeLayout _baseLayout;
      private readonly ActivityIndicator _isBusyIndicator = new ActivityIndicator();
      private View _currentStage;
      private bool _isBusyShowing;

      public MasterSinglePage()
      {
         BackgroundColor = Color.Transparent;

         _baseLayout = FormsUtils.GetExpandingRelativeLayout();
         _backgroundOverlay = FormsUtils.GetExpandingRelativeLayout();
         _backgroundOverlay.BackgroundColor = OVERLAY_BACKGROUND_COLOR;

         _isBusyIndicator.Color = Color.White;
         _isBusyIndicator.HeightRequest = INDICATOR_WIDTH_HEIGHT;
         WidthRequest = INDICATOR_WIDTH_HEIGHT;

         _backgroundOverlay.Children.Add
         (
            _isBusyIndicator,
            Constraint.RelativeToParent
            (layout => layout.Width / 2 - INDICATOR_WIDTH_HEIGHT / 2),
            Constraint.RelativeToParent(layout => layout.Height / 2 - INDICATOR_WIDTH_HEIGHT / 2)
         );

         _baseLayout.CreateRelativeOverlay(_backgroundOverlay);

         Content = _baseLayout;
      }

      public bool AllowInputWhileIsBusy { get; set; }

      public View CurrentStage
      {
         get => _currentStage;
         set
         {
            // Can't set these as the CurrentStage
            if (value.IsAnEqualReferenceTo(_backgroundOverlay) && value.IsAnEqualReferenceTo(_baseLayout) &&
               value.IsAnEqualReferenceTo(_isBusyIndicator))
            {
               return;
            }

            // The value must be a child of the _baseLayout
            if (!_baseLayout.Children.Contains(value))
            {
               return;
            }

            _currentStage = value;
         }
      }

      public bool IsBusyShowing
      {
         get => _isBusyShowing;
         set
         {
            _isBusyShowing = value;

            if (_isBusyShowing)
            {
               _baseLayout.RaiseChild(_backgroundOverlay);
               _backgroundOverlay.InputTransparent = AllowInputWhileIsBusy;
            }
            else
            {
               _baseLayout.LowerChild(_backgroundOverlay);
               _backgroundOverlay.InputTransparent = false;
            }
         }
      }

      public Task[] StagedAddedAnimation { get; set; }

      public Task[] StagedRemovedAnimation { get; set; }

      public async Task AddStage(View overlay, bool doNotFocus, bool keepOthers)
      {
         Debug.Assert(overlay.IsNotNullOrDefault(), "New canvas required at ->" + nameof(AddStage) + ",<-");

         _baseLayout.CreateRelativeOverlay(overlay);

         if (!doNotFocus)
         {
            var removeAnimations = new List<Task>();

            if (CurrentStage.IsNotNullOrDefault() && StagedRemovedAnimation.IsNotEmpty())
            {
               removeAnimations.Add(Task.WhenAll(StagedRemovedAnimation));

               if (!keepOthers)
                  using (var task = new Task(() => _baseLayout.LowerChild(CurrentStage)))
                  {
                     removeAnimations.Add(task);
                  }
            }

            var addAnimations = new List<Task>();

            // Run the incoming canvas transitions
            if (StagedAddedAnimation.IsNotEmpty())
            {
               addAnimations.Add(Task.WhenAll(StagedAddedAnimation));

               using (var task = new Task(() => _baseLayout.RaiseChild(overlay)))
               {
                  addAnimations.Add(task);
               }
            }

            // Combine the two task sets and run them concurrently
            var allAnimations = new List<Task>();
            allAnimations.AddRange(removeAnimations);
            allAnimations.AddRange(addAnimations);

            await Task.WhenAll(allAnimations);

            CurrentStage = overlay;
         }

         if (!keepOthers)
         {
            var subViews = _baseLayout.Children.Where(C =>
               C.IsNotAnEqualReferenceTo(_backgroundOverlay) && C.IsNotAnEqualReferenceTo(_isBusyIndicator)).ToArray();

            for (var thisViewIndex = 0; thisViewIndex < subViews.Length; thisViewIndex++)
               _baseLayout.Children.RemoveAt(thisViewIndex);
         }
      }

      /// <remarks>
      ///    This is  utility to remove a stage; it does not animate or alter the Current Stage.
      /// </remarks>
      /// <param name="overlay"></param>
      /// <returns></returns>
      public Task RemoveStage(View overlay)
      {
         var foundIndex = _baseLayout.Children.IndexOf(overlay);

         if (foundIndex >= 0) _baseLayout.Children.RemoveAt(foundIndex);

         return Task.FromResult(true);
      }
   }
}
