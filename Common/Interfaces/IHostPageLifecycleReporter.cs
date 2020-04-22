namespace Com.MarcusTS.LifecycleAware.Common.Interfaces
{
   /// <summary>
   ///    Interface IHostPageLifecycleReporter
   /// </summary>
   public interface IHostPageLifecycleReporter
   {
      /// <summary>
      ///    Gets or sets the page lifecycle reporter.
      /// </summary>
      /// <value>The page lifecycle reporter.</value>
      IReportPageLifecycle PageLifecycleReporter { get; set; }

      /// <summary>
      ///    Called when [page appearing].
      /// </summary>
      void OnPageAppearing();

      /// <summary>
      ///    Called when [page disappearing].
      /// </summary>
      void OnPageDisappearing();
   }
}