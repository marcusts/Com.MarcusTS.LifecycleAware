namespace Com.MarcusTS.LifecycleAware.Common.Interfaces
{
   /// <summary>
   ///    Interface IHostAppLifecycleReporter
   /// </summary>
   public interface IHostAppLifecycleReporter
   {
      /// <summary>
      ///    Gets or sets the application lifecycle reporter.
      /// </summary>
      /// <value>The application lifecycle reporter.</value>
      IReportAppLifecycle AppLifecycleReporter { get; set; }

      /// <summary>
      ///    Called when [application going to sleep].
      /// </summary>
      void OnAppGoingToSleep();

      /// <summary>
      ///    Called when [application resuming].
      /// </summary>
      void OnAppResuming();

      /// <summary>
      ///    Called when [application starting].
      /// </summary>
      void OnAppStarting();
   }
}