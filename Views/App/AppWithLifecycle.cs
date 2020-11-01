namespace Com.MarcusTS.LifecycleAware.Views.App
{
   using Com.MarcusTS.LifecycleAware.Common.Interfaces;
   using Com.MarcusTS.LifecycleAware.Common.Utils;
   using Com.MarcusTS.SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>
   ///    Class AppWithLifecycle.
   ///    Implements the <see cref="Xamarin.Forms.Application" />
   ///    Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IReportAppLifecycle" />
   ///    Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IManageMainPage" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Application" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IReportAppLifecycle" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IManageMainPage" />
   public class AppWithLifecycle : Application, IReportAppLifecycle, IManageMainPage
   {
      /// <summary>
      ///    Occurs when [application is going to sleep].
      /// </summary>
      public event EventUtils.NoParamsDelegate AppIsGoingToSleep;

      /// <summary>
      ///    Occurs when [application is resuming].
      /// </summary>
      public event EventUtils.NoParamsDelegate AppIsResuming;

      /// <summary>
      ///    Occurs when [application is starting].
      /// </summary>
      public event EventUtils.NoParamsDelegate AppIsStarting;

      /// <summary>
      ///    Gets or sets the root page of the application.
      /// </summary>
      /// <value>The root page of the application.</value>
      /// <remarks>This property throws an exception if the application developer attempts to set it to <see langword="null" />.</remarks>
      public new Page MainPage
      {
         get => base.MainPage;
         set
         {
            base.MainPage = value;

            if (base.MainPage != null)
            {
               base.MainPage.SetLifecycleReporters(this);
            }
         }
      }

      /// <summary>
      ///    Application developers override this method to perform actions when the application resumes from a sleeping state.
      /// </summary>
      /// <remarks>To be added.</remarks>
      protected override void OnResume()
      {
         AppIsResuming?.Invoke();
      }

      /// <summary>
      ///    Application developers override this method to perform actions when the application enters the sleeping state.
      /// </summary>
      /// <remarks>To be added.</remarks>
      protected override void OnSleep()
      {
         AppIsGoingToSleep?.Invoke();
      }

      /// <summary>
      ///    Called when [start].
      /// </summary>
      protected override void OnStart()
      {
         AppIsStarting?.Invoke();
      }
   }
}
