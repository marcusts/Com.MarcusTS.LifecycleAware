namespace Com.MarcusTS.LifecycleAware.Common.Interfaces
{
   /// <summary>
   ///    Interface IHostPageLifecycleReporter
   /// </summary>
   public interface IHostStageLifecycleReporter
   {
      /// <summary>
      ///    Gets or sets the stage lifecycle reporter.
      /// </summary>
      /// <value>The stage lifecycle reporter.</value>
      IReportStageLifecycle StageLifecycleReporter { get; set; }

      /// <summary>
      ///    Called when [stage appearing].
      /// </summary>
      void OnStageAppearing();

      /// <summary>
      ///    Called when [stage disappearing].
      /// </summary>
      void OnStageDisappearing();
   }
}
